using System.ComponentModel.DataAnnotations.Schema;

namespace JWTAuthentication.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Locations { get; set; }
        //public int BookId { get; set; }
        //public Book? Book { get; set; }
        public string Description { get; set; }
        public ICollection<Book>? Book { get; set; }
    }
}
