using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.dtos;
using MoviesApi.Models;
using MoviesApi.Services;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController:ControllerBase
    {
        private readonly IMovieService movieService;
        private readonly IGenreService genreService;
        //private
        private readonly List<string> extensions = new List<string>() { ".jpg", ".png" };
        private readonly long maxSize = 1048576;
        public MoviesController(IMovieService movieService, IGenreService genreService)
        {
            this.movieService = movieService;
            this.genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies = await movieService.GetAll(); 
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var movie =  await movieService.GetById(id);


            if(movie is null)
            {
                return NotFound($"No Movie with the specified ID : {id}");
            }

            return Ok(movie);
        }

        [HttpGet("GetMoviesByGenreId")]
        public async Task<IActionResult> GetMoviesByGenreIdAsync(byte genreId)
        {
            var movies =await 
              movieService.GetAll(genreId); 

              
            return Ok(movies);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovieAsync([FromForm] MovieDto dto)
        {
            if(!extensions.Contains(Path.GetExtension(dto.Poster.FileName.ToLower())))
            {
                return BadRequest("File Extension must be Jpg or Png ");
            }
            if(dto.Poster.Length > maxSize)
            {
                return BadRequest("Poster cannot be bigger than 1 Mb");
            }

            var genre = await genreService.CheckGenre(dto.GenreId); 
            if(!genre)
            {
                return BadRequest($"No Genre with the id {dto.GenreId}");
            }

            var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);

            var movie = new Movie()
            {
                GenreId = dto.GenreId,
                Title = dto.Title,
                Poster = dataStream.ToArray(),
                Rate = dto.Rate,
                Storyline = dto.Storyline,
                Year = dto.Year,
            };

            await movieService.CreateMovie(movie);
            return Ok(movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovieAsync(int id, [FromForm] MovieDto dto)
        {
            var movie = await movieService.GetById(id);
            movie.Title = dto.Title;
            movie.Year = dto.Year;
            movie.Storyline = dto.Storyline;
            movie.Rate = dto.Rate;
            movie.GenreId= dto.GenreId;

            var genre =await genreService.CheckGenre(dto.GenreId); 
            if(!genre)
            {
                return BadRequest($"NO genre with ID :{id}");

            };

            if(dto.Poster != null)
            {
                
                if(!extensions.Contains(Path.GetExtension(dto.Poster.FileName.ToLower())))
                {
                    return BadRequest("File Extension must be Jpg or Png ");
                }
                if(dto.Poster.Length > maxSize)
                {
                    return BadRequest("Poster cannot be bigger than 1 Mb");
                }

                MemoryStream memoryStream = new MemoryStream();
                await dto.Poster.CopyToAsync(memoryStream);
                movie.Poster = memoryStream.ToArray();
            }


            movieService.UpdateMovie(movie);
            return Ok(movie); 


        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieAsync(int id)
        {
            var movie = await movieService.GetById(id);
            if(movie is null)
            {
                return BadRequest($"No movie with is ID : {id}");

            }
            movieService.Delete(movie); 
            return Ok(movie);
        }
    }
}
