using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {

        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.Superheroes.ToListAsync());
        }

        [HttpGet("id")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await _context.Superheroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found");
            return Ok(hero);
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> Get(SuperHero request)
        {
            var hero = await _context.Superheroes.FindAsync(request.Id);
            if (hero == null)
                return BadRequest("Hero not found");
            hero.SuperHeroName = request.SuperHeroName;
            hero.RealName = request.RealName;
            hero.LastName = request.LastName;

            await _context.SaveChangesAsync();
            return Ok(await _context.Superheroes.ToListAsync());
        } 

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.Superheroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.Superheroes.ToListAsync());
        }

        [HttpDelete("id")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var hero = await _context.Superheroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero Not found");
             _context.Superheroes.Remove(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.Superheroes.ToListAsync());
        }


    }
}
