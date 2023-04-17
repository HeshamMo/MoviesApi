using MoviesApi.Models;

namespace MoviesApi.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetAll();
        Task<Genre> GetById(byte id); 
        Task<Genre> CreateGenre(Genre genre);
        Genre UpdateGenre(Genre genre);
        Genre DeleteGenre(Genre genre);

        Task<bool> CheckGenre(byte id); 
    }
}
