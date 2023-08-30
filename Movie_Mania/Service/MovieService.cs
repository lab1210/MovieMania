using Microsoft.Ajax.Utilities;
using Movie_Mania.IService;
using Movie_Mania.Models;
using Movie_Mania.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using Movie_Mania.Migrations;
using System.Runtime.CompilerServices;

namespace Movie_Mania.Service
{
    public class MovieService : IMovieService
    {
        private readonly MovieContext movieContext;
        public MovieService()
        {
            movieContext = new MovieContext();
        }
        public IEnumerable<MovieResources> GetAllMovies()
        {
            var movies = movieContext.Movies.ToList();
            var MovieResourcelist = new List<MovieResources>();
            foreach (var movie in movies)
            {
                var movieResource = new MovieResources
                {
                    Code = movie.Code,
                    Name = movie.Name,
                    TicketPrice = movie.TicketPrice,
                    Description = movie.Description,
                    Country = movie.Country,
                    ReleaseDate = movie.ReleaseDate,
                    Id = movie.Id,
                    ImagePath = movie.ImagePath,
                    VideoPath = movie.VideoPath,
                    Slug = movie.Slug,
                };
                MovieResourcelist.Add(movieResource);
            }
            return MovieResourcelist;
        }

        public IEnumerable<Movie> GetMovieWithHighestRating()
        {
            var highRatedMovieIds = movieContext.rating
                .Where(rating => rating.Value == 5)
                .Select(rating => rating.MovieID)
                .Distinct();

            var highRatedMovies = movieContext.Movies
                .Include(movie => movie.SelectedGenres)
                .Where(movie => highRatedMovieIds.Contains(movie.Id));

            return highRatedMovies.ToList();
        }
        public IEnumerable<string> GetGenres()
        {
           var genre=movieContext.pickedGenres
                .OrderBy(c=>c.name)
                .Select (c => c.name)
                .Distinct();
            return genre;
        }

        public IEnumerable<MovieResources> GetMovieByGenre(string genreName)
        {
            var movies = from movie in movieContext.Movies
                         where movie.SelectedGenres.Any(genre => genre.name == genreName)
                         select movie;

            var movieList = new List<MovieResources>();

            foreach (var movie in movies)
            {
                var movieResource = new MovieResources
                {
                    Code = movie.Code,
                    Name = movie.Name,
                    TicketPrice = movie.TicketPrice,
                    Description = movie.Description,
                    Country = movie.Country,
                    ReleaseDate = movie.ReleaseDate,
                    ImagePath = movie.ImagePath,
                    Id = movie.Id,
                    Slug = movie.Slug,
                    VideoPath = movie.VideoPath,
                };

                movieList.Add(movieResource);
            }

            return movieList;
        }


        public IEnumerable<string> GetCountry()
        {
            var country = movieContext.Movies
                .OrderBy(c => c.Country)
                .Select(c => c.Country)
                .Distinct();
            return country;
        }
        public IEnumerable<MovieResources> GetMovieByCountry(string countryName)
        {
            var movies = from movie in movieContext.Movies
                         where movie.Country == countryName
                         select movie;

            var movieList = new List<MovieResources>();

            foreach (var movie in movies)
            {
                var movieResource = new MovieResources
                {
                    Code = movie.Code,
                    Name = movie.Name,
                    TicketPrice = movie.TicketPrice,
                    Description = movie.Description,
                    Country = movie.Country,
                    ReleaseDate = movie.ReleaseDate,
                    ImagePath = movie.ImagePath,
                    Id = movie.Id,
                    Slug = movie.Slug,
                    VideoPath = movie.VideoPath,
                };

                movieList.Add(movieResource);
            }

            return movieList;
        }




        public MovieResources GetMovieById(int id)
        {
            var movie = movieContext.Movies
                .Include(x => x.SelectedGenres)
                .SingleOrDefault(p => p.Id == id);
            var movieresource = new MovieResources
            {
                Code = movie.Code,
                Name = movie.Name,
                TicketPrice = movie.TicketPrice,
                Description = movie.Description,
                Country = movie.Country,
                ReleaseDate = movie.ReleaseDate,
                Id = movie.Id,
                Slug = movie.Slug,
                ImagePath = movie.ImagePath,
                VideoPath = movie.VideoPath,
                SelectedGenres = movie.SelectedGenres.Select(p => new PickedGenreresource
                {
                    name = p.name,
                    genreid = p.genreid,
                    Selected = p.Selected,
                }).ToList()
            };
            return movieresource;
        }



        public MovieResources Getgenredetails()
        {
            var list = new MovieResources();
            var genres = new List<PickedGenreresource>();
            var data = movieContext.genres.OrderBy(o => o.Name).ToList();
            foreach (var item in data)
            {
                var genre = new PickedGenreresource
                {
                    name = item.Name,
                    genreid = item.Id,
                };
                genres.Add(genre);
            }
            list.SelectedGenres = genres;

            return list;
        }


        public void UpdateMovie(MovieResources movie)
        {
            var existingMovie = movieContext.Movies.FirstOrDefault(p => p.Id == movie.Id);

            if (existingMovie != null)
            {
                existingMovie.TicketPrice = movie.TicketPrice;
                existingMovie.Country = movie.Country;
                existingMovie.Description = movie.Description;
                existingMovie.Name = movie.Name;
                existingMovie.ReleaseDate = movie.ReleaseDate;
                existingMovie.ImagePath = movie.ImagePath;
                existingMovie.VideoPath = movie.VideoPath;

                existingMovie.Slug = movie.Slug;


                movieContext.SaveChanges();

            }
        }
        public void DeleteMovie(int id)
        {
            var movie = movieContext.Movies.Include(c => c.SelectedGenres).FirstOrDefault(p => p.Id == id);
            movieContext.pickedGenres.RemoveRange(movie.SelectedGenres);
            movieContext.MoviesComment.RemoveRange(movie.MovieComments);
            movieContext.Movies.Remove(movie);
            movieContext.SaveChanges();

        }
        public IEnumerable<MovieResources> MovieSearch()
        {
            var movie = from c in movieContext.Movies
                        select c;
            var movielist = new List<MovieResources>();
            foreach (var movies in movie)
            {
                var movieresc = new MovieResources
                {
                    Code = movies.Code,
                    Name = movies.Name,
                    TicketPrice = movies.TicketPrice,
                    Description = movies.Description,
                    Country = movies.Country,
                    ReleaseDate = movies.ReleaseDate,
                    ImagePath = movies.ImagePath,
                    Id = movies.Id,
                    Slug = movies.Slug,
                    VideoPath = movies.VideoPath,

                };

                movielist.Add(movieresc);
            }
            return movielist;
        }

        public MovieResources GetMovieByCode(string Slug)
        {

            var movie = movieContext.Movies
                .Include(x => x.SelectedGenres)
                .FirstOrDefault(p => p.Slug == Slug);
            var movieresource = new MovieResources
            {
                Code = movie.Code,
                Name = movie.Name,
                TicketPrice = movie.TicketPrice,
                Description = movie.Description,
                Country = movie.Country,
                ReleaseDate = movie.ReleaseDate,
                Slug = movie.Slug,
                Id = movie.Id,
                ImagePath = movie.ImagePath,
                VideoPath = movie.VideoPath,


                Ratings = movie.Ratings.Select(p => new RatingResource
                {
                    Id = p.Id,
                    Value = p.Value,
                    Slug = p.Slug,
                    UserName = p.UserName,
                }).ToList(),
                MovieCommentsresource = movie.MovieComments.Select(p => new MovieCommentresource
                {
                    Id = p.Id,
                    User = p.User,
                    Post = p.Post,
                    PostDate = p.PostDate,
                    Slug = p.Slug,


                }).ToList(),

                SelectedGenres = movie.SelectedGenres.Select(p => new PickedGenreresource
                {
                    name = p.name,
                    genreid = p.genreid,
                    Selected = p.Selected,
                }).ToList(),


            };
            return movieresource;
        }




        public bool AddMovie(MovieResources movie)
        {

            var genres = movie.SelectedGenres;
            genres = genres.Where(p => p.Selected).ToList();
            var slug = $"{movie.Name}-{movie.Code}".ToLower().Replace(" ", "-");
            var NewMovie = new Movie
            {
                Code = movie.Code,
                Name = movie.Name,
                TicketPrice = movie.TicketPrice,
                Description = movie.Description,
                Country = movie.Country,
                ReleaseDate = movie.ReleaseDate,
                ImagePath = movie.ImagePath,
                VideoPath = movie.VideoPath,
                Id = movie.Id,
                Slug = slug,
            };
            var moviegenre = new List<PickedGenre>();
            foreach (var item in genres)
            {
                var genre = new PickedGenre
                {
                    id = item.id,
                    name = item.name,
                    genreid = item.genreid,
                    Selected = item.Selected
                };
                moviegenre.Add(genre);
            }

            NewMovie.SelectedGenres = moviegenre;
            this.movieContext.Movies.Add(NewMovie);
            this.movieContext.SaveChanges();
            return true;
        }




        public void AddComment(int movieId, string user, string post, string slug)
        {
            var movie = movieContext.Movies.SingleOrDefault(m => m.Slug == slug);
            if (movie != null)
            {
                var comment = new MovieComment
                {
                    User = user,
                    Post = post,
                    PostDate = DateTime.Now,
                    Slug = slug,
                    MovieId = movieId,
                };

                if (movie.MovieComments == null)
                    movie.MovieComments = new List<MovieComment>();

                movie.MovieComments.Add(comment);
                movieContext.SaveChanges();
            }
        }


        public void AddOrUpdateRating(int movieid, int ratingvalue, string user, string slug)
        {

            var movie = movieContext.Movies.SingleOrDefault(m => m.Slug == slug);

            var existingrating = movie.Ratings.FirstOrDefault(r => r.UserName == user);
            if (existingrating != null)
            {

                existingrating.Value = ratingvalue;

            }
            else
            {
                var rating = new Rating
                {
                    Value = ratingvalue,
                    MovieID = movieid,
                    UserName = user,
                    Slug = slug,
                };
                if (movie.Ratings == null)
                    movie.Ratings = new List<Rating>();
                movie.Ratings.Add(rating);
            }

            movieContext.SaveChanges();

        }
        public decimal GetRatingAverage(string slug)
        {
            var movie = movieContext.Movies.FirstOrDefault(r => r.Slug == slug);
            if (movie.Ratings == null || movie.Ratings.Count() == 0)
            {
                return 0;
            }
            var total_rating = movie.Ratings.Sum(r => r.Value);
            decimal average = total_rating / movie.Ratings.Count();
            return average;

        }





    }
}


