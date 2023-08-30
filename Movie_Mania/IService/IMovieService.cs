using Movie_Mania.Models;
using Movie_Mania.Resources;
using System.Collections.Generic;

namespace Movie_Mania.IService
{
    public interface IMovieService
    {
        bool AddMovie(MovieResources movie);
        IEnumerable<MovieResources> GetAllMovies();
        IEnumerable<MovieResources> MovieSearch();
        IEnumerable<MovieResources> GetMovieByCountry(string countryName);
        IEnumerable<string> GetCountry();

        MovieResources Getgenredetails();
        MovieResources GetMovieById(int id);
        MovieResources GetMovieByCode(string Slug);
        void UpdateMovie(MovieResources movie);
        void DeleteMovie(int id);

        void AddComment(int movieId, string user, string post,string slug);
        void AddOrUpdateRating(int movieid, int ratingvalue, string user, string slug);

        decimal GetRatingAverage(string slug);
        IEnumerable<Movie> GetMovieWithHighestRating();
        IEnumerable<string> GetGenres();
        IEnumerable<MovieResources> GetMovieByGenre(string genreName);




    }
}
