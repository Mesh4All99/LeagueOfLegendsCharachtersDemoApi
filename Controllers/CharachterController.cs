using LeagueOfLegendsCharachters.Data;
using LeagueOfLegendsCharachters.DTO.CharachterDTO;
using LeagueOfLegendsCharachters.Mapper;
using LeagueOfLegendsCharachters.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace LeagueOfLegendsCharachters.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class CharachterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CharachterController(ApplicationDbContext context)
        {
            _context = context;
        }
        // Get All Async //////////

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.Charachters.Include(x=>x.Status).Select(o => o.DisplayCharachterDTO()).ToListAsync();
            return Ok(data);
        }
        //
        //
        [HttpGet("{name}")]
        public async Task<IActionResult> GetById(string name)
        {
            var data = await _context.Charachters.Include(o => o.Status).FirstOrDefaultAsync(x => x.Name == name);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data.DisplayCharachterDTO());    
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostCharachterDTO model)
        {
            if (ModelState.IsValid)
            {
                await _context.Charachters.AddAsync(model.PostCharachterDTO());
                await _context.SaveChangesAsync();
            }
            return CreatedAtAction(nameof(GetById), new { name = model.Name}, model);
        }
        //
        //
        [HttpPut("{name}")]
        public async Task<IActionResult> Put([FromBody] PutCharachterDTO model, string name)
        {
            if (ModelState.IsValid)
            {
                var data =await _context.Charachters.Include(o => o.Status).FirstOrDefaultAsync(n => n.Name == name);
                if (data == null)
                {
                    return NotFound();
                }
                data.Name = model.Name;
                data.RolePosition = model.RolePosition;
                data.BlueEssence = model.BlueEssence;
                data.Status = new Status
                {
                    Armor = model.Status.Armor,
                    AttackRange = model.Status.AttackRange,
                    Damage = model.Status.Damage,
                    Health = model.Status.Health,
                    MagicResist = model.Status.MagicResist,
                    MovementSpeed = model.Status.MovementSpeed,
                };
                _context.Charachters.Update(data);
                await _context.SaveChangesAsync();
            }
            return NoContent();
        }
    }
}
