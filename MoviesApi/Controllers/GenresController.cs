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
    public class GenresController:ControllerBase
    {
        private readonly IGenreService genreService; 
        public GenresController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var genres = await genreService.GetAll();
            return Ok(genres);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenreAsync(GenreDto dto)
        {
            Genre genre = new Genre() { Name = dto.name };
            await genreService.CreateGenre(genre);

            return Ok(genre); 
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdataGenreAsync (byte id , [FromBody] GenreDto dto)
        {
            Genre genre = await genreService.GetById(id); 
            if (genre == null)
            {
                return NotFound($"No Genre with the ID :{id}"); 
            }

            genre.Name = dto.name;
            genreService.UpdateGenre(genre);
            return Ok(genre);   
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenreAsync ( byte id)
        {
            var genre = await genreService.GetById(id); 
            if(genre == null)
            {
                return NotFound($"No Genre with the ID :{id}");
            }
            genreService.DeleteGenre(genre);
            return Ok(genre);   
        }
    }
}
