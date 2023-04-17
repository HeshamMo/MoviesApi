using System.ComponentModel.DataAnnotations;

namespace MoviesApi.dtos
{
    public class GenreDto
    {
        [StringLength(100)]
        public string name { get; set; }
    }
}
