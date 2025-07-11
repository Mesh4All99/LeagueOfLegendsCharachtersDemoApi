using LeagueOfLegendsCharachters.Data;
using LeagueOfLegendsCharachters.Mapper;
using LeagueOfLegendsCharachters.Models;
using LeagueOfLegendsCharachters.Models.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connection = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("ConnectionString returned null");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connection);
});

builder.Services.AddIdentityCore<AppUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        var secret = builder.Configuration["JwtConfig:Secret"];
        var issuer = builder.Configuration["JwtConfig:ValidIssuer"];
        var audience = builder.Configuration["JwtConfig:ValidAudiences"];
        if (secret is null || issuer is null || audience is null)
        {
            throw new ApplicationException("JWT is not configured in settings");
        }
        // up is optional
        option.SaveToken = true;
        option.RequireHttpsMetadata = false;
        option.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtConfig:ValidIssuer"],
            ValidAudience = builder.Configuration["JwtConfig:ValidAudiences"],
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secret))
        };
    });

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var roleManager =  service.GetRequiredService<RoleManager<IdentityRole>>();

    if (! await roleManager.RoleExistsAsync(AppRoles.Administrator))
    {
        await roleManager.CreateAsync(new IdentityRole(AppRoles.Administrator));
    }

    if (! await roleManager.RoleExistsAsync(AppRoles.VipUser))
    {
        await roleManager.CreateAsync(new IdentityRole(AppRoles.VipUser));
    }

    if (! await roleManager.RoleExistsAsync(AppRoles.User))
    {
        await roleManager.CreateAsync(new IdentityRole(AppRoles.User));
    }
}

using (var _scope = app.Services.CreateScope())
{
    var _context = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    if (_context.Database.GetPendingMigrations().Any())
    {
        _context.Database.Migrate();
    }
    //
    if (!_context.Charachters.Any())
    {
        _context.Charachters.AddRange(
        new Charachter()
        {
            Name = "Jhin",
            BlueEssence = 6200,
            RolePosition = RolePositionEnum.Bottom,
            Status = new Status
            {
                Damage = 59,
                Armor = 24,
                Health = 665,
                AttackRange = 550,
                MagicResist = 30,
                MovementSpeed = 330,
            }
        },
        new Charachter()
        {
            Name = "Urgot",
            BlueEssence = 1575,
            RolePosition = RolePositionEnum.Top,
            Status = new Status
            {
                Damage = 63,
                Armor = 36,
                Health = 655,
                AttackRange = 350,
                MagicResist = 32,
                MovementSpeed = 330
            }
        },
        new Charachter()
        {
            Name = "Daruis",
            BlueEssence = 225,
            RolePosition = RolePositionEnum.Top,
            Status = new Status
            {
                Damage = 64,
                Armor = 37,
                Health = 652,
                AttackRange = 175,
                MagicResist = 32,
                MovementSpeed = 340
            }
        }
        );
        _context.SaveChanges();
    }
}

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
