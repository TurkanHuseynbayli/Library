using JWTAuthentication.DAL;
using JWTAuthentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWTAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.Admin)]
    public class LocationController : ControllerBase
    {
        private readonly AppDbContext _context;
        public LocationController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/<LocationController>
        [HttpGet]
      [AllowAnonymous]
        public ActionResult<List<Location>> Get()
        {
            var locations = _context.Location.Include(l=>l.Book).ToList();
            return locations;
        }

        // GET api/<LocationController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LocationController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Location location)
        {
            //Location location = _context.Location.Where(x => x.Locations == bookDto.LocationName).FirstOrDefault();


            //Book book = new Book
            //{
            //    Name = bookDto.Name,
            //    Location = location
            //};


            await _context.AddAsync(location);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success", Message = "Location created successfully!" });
        }

        // PUT api/<LocationController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Location location)
        {
            Location dbLocation = await _context.Location.FindAsync(id);

            // Book dbbook = _context.Books.Include(blg => blg.Location).FirstOrDefault(blg => blg.Id == id);
            if (dbLocation == null) return NotFound();
            dbLocation.Locations = location.Locations;
            dbLocation.Description = location.Description;
           
            //Book book = new Book
            //{
            //    Name = bookDto.Name,
            //    Location = l
            //};
            // dbbook.Author = book.Author;

            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success", Message = "Updated!" });
        }

        // DELETE api/<LocationController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Location location = await _context.Location.FindAsync(id);
            // Location location = await _context.Location.FindAsync(id);

            //   if (book2 == null) return NotFound();


            _context.Location.Remove(location);

            // _context.Location.Remove(location);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success", Message = "Deleted!" });
        }
    }
}
