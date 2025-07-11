﻿using LeagueOfLegendsCharachters.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LeagueOfLegendsCharachters.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        public AccountController(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AddOrUpdateAppUserModel model)
        {
            // Check if the model is valid 
            if (ModelState.IsValid)
            {
                var existedUser = await _userManager.FindByNameAsync(model.UserName);
                if (existedUser != null)
                {
                    ModelState.AddModelError("", "User name is already taken");
                    return BadRequest(ModelState);
                }
                // Create a new user object 
                var user = new AppUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                // Try to save the user
                var result = await _userManager.CreateAsync(user, model.Password);
                // If the user is successfully created, return Ok 
                var RoleResult = await _userManager.AddToRoleAsync(user,AppRoles.User);
                if (result.Succeeded && RoleResult.Succeeded)
                {
                    var token = GenerateToken(model.UserName);
                    return Ok(new { token });
                }
                // If there are any errors, add them to the ModelState object and return the error to the client 
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            // If we got this far, something failed, redisplay form 
            return BadRequest(ModelState);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            // Get the secret in the configuration
            // Check if the model is valid 
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    if (await _userManager.CheckPasswordAsync(user, model.Password))
                    {
                        var token = GenerateToken(model.UserName);
                        return Ok(new { token });
                    }
                }
                // If the user is not found, display an error message 
                ModelState.AddModelError("", "Invalid username or password");
            }
            return BadRequest(ModelState);
        }
        private string? GenerateToken(string userName)
        {
            var secret = _configuration["JwtConfig:Secret"];
            var issuer = _configuration["JwtConfig:ValidIssuer"];
            var audience = _configuration["JwtConfig:ValidAudiences"];
            if (secret is null || issuer is null || audience is null)
            {
                throw new ApplicationException("Jwt is not set in the  configuration");
            }
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userName)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }
    }
}
