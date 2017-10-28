using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicShop.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconFilename { get; set; }

        public ICollection<Album> Albums { get; set; }
    }
}