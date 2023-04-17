using Microsoft.EntityFrameworkCore;
using MoviesApi.Models;

namespace MoviesApi.Services
{
    public class GenreService:IGenreService
    {
        private readonly MoviesContext context;
        public GenreService(MoviesContext context)
        {
            this.context = context;
        }

        public async Task<bool> CheckGenre(byte id)
        {
            return await context.Genres.AnyAsync(g => g.Id == id);
        }

        public async Task<Genre> CreateGenre(Genre genre)
        {
            await context.Genres.AddAsync(genre);
            context.SaveChanges();
            return genre; 
        }

        public Genre DeleteGenre(Genre genre)
        {
            context.Remove(genre);
            context.SaveChanges(); 
            return genre;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
           
            return await context.Genres.OrderBy(gen => gen.Id).ToListAsync();
        }

        public async Task<Genre> GetById(byte id)
        {
            return await context.Genres.SingleOrDefaultAsync(g => g.Id == id);

        }

        public Genre UpdateGenre(Genre genre)
        {
            context.Update(genre);
            context.SaveChanges(); 
            return genre;
        }
    }
}
