namespace JWTAuthentication.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public Author Author { get; set; }

    }

    public class BookDTO
    {       
        public string Name { get; set; }
        public string LocationName { get; set; }
        public string AuthorName { get; set; }
    }

}

