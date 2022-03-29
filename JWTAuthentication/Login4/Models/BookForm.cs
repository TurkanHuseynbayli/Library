using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login4.Models
{
    public class BookDataSource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Locations { get; set; }
        public string Authors { get; set; }
        public string Description { get; set; }
    }
    public class BookForm
    {
        public int Id { get; set; }
        public string Name { get; set; }
       // public string Author { get; set; }
        public Location Location { get; set; }
        public Author Author { get; set; }

    }

    public class Location
    {
        public int Id { get; set; }
        public string Locations { get; set; }
        public string Description { get; set; }
    }

    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
