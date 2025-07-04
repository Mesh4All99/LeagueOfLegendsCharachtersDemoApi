using LeagueOfLegendsCharachters.Data;
using LeagueOfLegendsCharachters.DTO.CharachterDTO;
using LeagueOfLegendsCharachters.Mapper;
using LeagueOfLegendsCharachters.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [HttpGet]
        public async Task<ActionResult<DisplayCharachterDTO>> GetAll()
        {
            var data = await _context.Charachters.Include(b => b.Status).ToListAsync();
            var fdata = data.Select(s => s.DisplayCharachterDTO()); 
            return Ok(fdata);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string name)
        {
            var Data = await _context.Charachters.Include(o => o.Status).FirstOrDefaultAsync(x => x.Name == name);
            return Ok(Data);
        }
        [HttpPost]
        public async Task<IActionResult> Post(PostCharachterDTO PostModel)
        {
            if (ModelState.IsValid)
            {
                if (PostModel == null)
                {
                    return BadRequest();
                }
                await _context.Charachters.AddAsync(PostModel.PostCharachterDTO());
                await _context.SaveChangesAsync();
            }
            return Ok(PostModel);
        }
        [HttpPut("{name}")]
        public async Task<IActionResult> Put(PostCharachterDTO PostModel, [FromRoute] string name)
        {
            var Data = await _context.Charachters.FirstOrDefaultAsync(x => x.Name == name);
            if (Data == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                Data.Name = PostModel.Name;
                Data.RolePosition = PostModel.RolePosition;
                Data.BlueEssence = PostModel.BlueEssence;
                Data.Status = new Status
                {
                    Armor = PostModel.Status!.Armor,
                    Health = PostModel.Status.Health,
                    AttackRange = PostModel.Status.AttackRange,
                    Damage = PostModel.Status.Damage,
                    MagicResist = PostModel.Status.MagicResist,
                    MovementSpeed = PostModel.Status.MovementSpeed,
                };
                _context.Charachters.Update(Data);
                await _context.SaveChangesAsync();
            }
            return Ok(PostModel);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string name)
        {
            var Data = await _context.Charachters.FirstOrDefaultAsync(x => x.Name == name);
            if (Data == null)
            {
                return NotFound();
            }
            else
            {
                _context.Charachters.Remove(Data);
                await _context.SaveChangesAsync();
            }
            return NoContent();
        }
    }
}
