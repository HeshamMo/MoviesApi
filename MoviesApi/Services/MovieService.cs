using Microsoft.EntityFrameworkCore;
using MoviesApi.Models;

namespace MoviesApi.Services
{
    public class MovieService:IMovieService
    {
        private readonly MoviesContext context;
        public MovieService(MoviesContext context)
        {
            this.context = context;
        }
        public async Task<Movie> CreateMovie(Movie movie)
        {
            await context.Movies.AddAsync(movie);
            return movie;
        }

        public Movie Delete(Movie movie)
        {
            context.Movies.Remove(movie);
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAll(int genreId = 0)
        {
            return await context.Movies.
                Where(m => m.GenreId == genreId || genreId == 0)
                .Include(m => m.Genre)
                .OrderByDescending(m => m.Rate)
                .ToListAsync();



        }

        public async Task<Movie> GetById(int id)
        {
           return await context.Movies.Include(m=>m.Genre).SingleOrDefaultAsync(Genre=> Genre.Id == id);  
        }

        public Movie UpdateMovie(Movie movie)
        {
            context.Update(movie); 
            return movie;
        }
    }
}
