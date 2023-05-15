using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heroes = new List<SuperHero> {
                new SuperHero
                {
                    Id = 1,
                    Name = "Spider Man",
                    FirstName="Peter",
                    LastName ="Parker",
                    Place = "New york City"
                },
                    new SuperHero
                {
                    Id = 2,
                    Name = "Iron Man",
                    FirstName="Tony",
                    LastName ="Stark",
                    Place = "Long Island"
                }
            };

        private readonly DataContext _context;
        public SuperHeroController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public  async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<SuperHero>> Get(int Id)
        {
            var hero = await _context.SuperHeroes.Where(h=>h.Id==Id).FirstOrDefaultAsync();
            if(hero==null)
            {
                return BadRequest("Hero not found");
            }
            return Ok(hero);
        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHeroes(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHeroes(SuperHero request)
        {
            var hero = await _context.SuperHeroes.FindAsync(request.Id);
            if (hero == null)
            {
                return BadRequest("Hero not found");
            }
            hero.Name = request.Name;
            hero.FirstName = request.FirstName;
            hero.LastName = request.LastName;
            hero.Place = request.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult<SuperHero>> DeleteHero(int Id)
        {
            var dbhero = await _context.SuperHeroes.FindAsync(Id);
            if (dbhero == null)
            {
                return BadRequest("Hero not found");
            }
            _context.SuperHeroes.Remove(dbhero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}
