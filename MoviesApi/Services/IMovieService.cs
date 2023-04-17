using MoviesApi.Models;
using System.Collections.Generic;

namespace MoviesApi.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAll(int genreId = 0);
        Task<Movie> GetById(int id);
        Task<Movie> CreateMovie(Movie movie);
        Movie UpdateMovie(Movie movie);
        Movie Delete(Movie movie);


    }
}
