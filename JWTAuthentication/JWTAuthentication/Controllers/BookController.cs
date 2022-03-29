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
    //[AllowAnonymous]
  
     [Authorize(Roles = UserRoles.Admin)]
  //  [CustomAuthorize(UserRoles.Admin, UserRoles.User)]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _context;
        public BookController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/<BookController>
        
        [HttpGet]
        [AllowAnonymous]
        //[Authorize(Roles = UserRoles.Admin)]
        //[Authorize(Roles = UserRoles.User)]
        public  ActionResult<List<Book>> Get()
        {
            var books = _context.Books.Include(b => b.Location).Include(a=>a.Author).ToList();
           
            return books;
        }

        // GET api/<BookController>/5
     
        [HttpGet("{id}")]
     //  [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<Book>> Get(int id)
        {
            Book book = await _context.Books.FindAsync(id);
            if (book == null) return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Book NotFound!" });
            return book;
        }

        // POST api/<BookController>
        
        [HttpPost]
       // [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult> Post(BookDTO bookDto)
        {
           Location location = _context.Location.Where(x => x.Locations == bookDto.LocationName).FirstOrDefault();
           Author author=_context.Authors.Where(a=>a.Name==bookDto.AuthorName).FirstOrDefault();  
            //if (location != null)
            //{
            //    return BadRequest("Bu LocationDa Kitab Var!");
            //}

            //location = new Location
            //{
            //    Locations = "A1"
            //};

            // await _context.AddAsync(location);
            // await _context.SaveChangesAsync();

            Book book = new Book
            {
                Name = bookDto.Name,
                Location = location,
                Author = author,
                
            };


            await _context.AddAsync(book);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success", Message = "Book created successfully!" });
        }

        // PUT api/<BookController>/5
      
        [HttpPut("{id}")]
     //   [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult> Put(int id, [FromBody] BookDTO bookDto)
        {
            
         // if (id != bookDto.Id) return BadRequest();
         
            Location l= _context.Location.Where(x => x.Locations == bookDto.LocationName).FirstOrDefault();
        //    Author author= _context.Authors.Where(x => x.Name == bookDto.AuthorName).FirstOrDefault();
           
            Book dbbook = await _context.Books.FindAsync(id);
            Author author = _context.Authors.Where(x => x.Name == bookDto.AuthorName).FirstOrDefault();
            // Book dbbook = _context.Books.Include(blg => blg.Location).FirstOrDefault(blg => blg.Id == id);
            if (dbbook == null) return NotFound();
            dbbook.Name = bookDto.Name;
            dbbook.Location = l ;
            dbbook.Author = author;
            //Book book = new Book
            //{
            //    Name = bookDto.Name,
            //    Location = l
            //};
            // dbbook.Author = book.Author;

            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success", Message = "Updated!" });
        }

        // DELETE api/<BookController>/5
       
        [HttpDelete("{id}")]
       // [Authorize(Roles = UserRoles.Manager)]
        public async Task<ActionResult> Delete(int id)
        {
            Book book = await _context.Books.FindAsync(id);
         // Location location = await _context.Location.FindAsync(id);
        
          //   if (book2 == null) return NotFound();

          
            _context.Books.Remove(book);
           
           // _context.Location.Remove(location);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success", Message = "Deleted!" });
        }
    }
}
