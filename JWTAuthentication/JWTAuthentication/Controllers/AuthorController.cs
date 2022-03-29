using JWTAuthentication.DAL;
using JWTAuthentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWTAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.Manager)]
    public class AuthorController : ControllerBase
    {
        private readonly AppDbContext _context;
        public AuthorController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/<AuthorController>
        [HttpGet]
    [AllowAnonymous]
        public ActionResult<List<Author>> Get()
        {
            return _context.Authors.ToList();
        }

        // GET api/<AuthorController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> Get(int id)
        {
            Author author = await _context.Authors.FindAsync(id);
            if (author == null) return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Author NotFound!" });
            return author;
        }

        // POST api/<AuthorController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Author author)
        {
            
            if (!ModelState.IsValid) return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Name field required!" });
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success", Message = "Author created successfully!" });
        }

        // PUT api/<AuthorController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Author author)
        {
            if (id != author.Id) return BadRequest();
            Author dbAuthor = await _context.Authors.FindAsync(id);
            if (dbAuthor == null) return NotFound();
            dbAuthor.Name = author.Name;
           // dbAuthor.Surname = author.Surname;
            dbAuthor.Age = author.Age;

            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success", Message = "Updated!" });
        }

        // DELETE api/<AuthorController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Author author = await _context.Authors.FindAsync(id);
            if (author == null) return NotFound();
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success", Message = "Deleted!" });
        }
    }
}
