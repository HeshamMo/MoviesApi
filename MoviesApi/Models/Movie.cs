using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesApi.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        [MaxLength(2500)]
        public string Storyline { get; set; }
        public byte[] Poster { get; set; }


        public byte GenreId { get; set; }
        public virtual Genre Genre { get; set; } 
    }

}
