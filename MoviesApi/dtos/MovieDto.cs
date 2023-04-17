using MoviesApi.Models;
using System.ComponentModel.DataAnnotations;

namespace MoviesApi.dtos
{
    public class MovieDto
    {
       
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        [MaxLength(2500)]
        public string Storyline { get; set; }
        public IFormFile? Poster { get; set; }

        public byte GenreId { get; set; }
       
    }
}
