using Movie_Mania.IService;
using Movie_Mania.Models;
using Movie_Mania.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Movie_Mania.Service
{
    public class SeriesService : ISeriesService
    {
        private readonly MovieContext seriescontext;
        public SeriesService()
        {
            seriescontext = new MovieContext();
        }
        public IEnumerable<SeriesResources> GetAllSeries()
        {
            var series = seriescontext.series.ToList();
            var seriesResourcelist = new List<SeriesResources>();
            foreach (var serie in series)
            {
                var seriesResource = new SeriesResources
                {
                    Code = serie.Code,
                    Name = serie.Name,
                    TicketPrice = serie.TicketPrice,
                    Description = serie.Description,
                    Country = serie.Country,
                    ReleaseDate = serie.ReleaseDate,
                    Id = serie.Id,
                    ImagePath = serie.ImagePath,
                    VideoPath = serie.VideoPath,

                    Slug = serie.Slug,
                    NumberOfSeasons = serie.NumberOfSeasons,
                };
                seriesResourcelist.Add(seriesResource);
            }
            return seriesResourcelist;
        }

        public IEnumerable<string> GetCountry()
        {
            var country = seriescontext.series
                .OrderBy(c => c.Country)
                .Select(c => c.Country)
                .Distinct();
            return country;
        }
        public IEnumerable<SeriesResources> GetSeriesByCountry(string countryName)
        {
            var movies = from movie in seriescontext.series
                         where movie.Country == countryName
                         select movie;

            var movieList = new List<SeriesResources>();

            foreach (var movie in movies)
            {
                var movieResource = new SeriesResources
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
                    NumberOfSeasons = movie.NumberOfSeasons,
                };

                movieList.Add(movieResource);
            }

            return movieList;
        }
        public IEnumerable<string> GetGenres()
        {
            var genre = seriescontext.seriespickedGenres
                 .OrderBy(c => c.name)
                 .Select(c => c.name)
                 .Distinct();
            return genre;
        }

        public IEnumerable<SeriesResources> GetSeriesByGenre(string genreName)
        {
            var movies = from movie in seriescontext.series
                         where movie.SeriesSelectedGenres.Any(genre => genre.name == genreName)
                         select movie;

            var movieList = new List<SeriesResources>();

            foreach (var movie in movies)
            {
                var movieResource = new SeriesResources
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
                    NumberOfSeasons = movie.NumberOfSeasons,
                };

                movieList.Add(movieResource);
            }

            return movieList;
        }

        public SeriesResources GetSeriesById(int id)
        {
            var serie = seriescontext.series
                .Include(x => x.SeriesSelectedGenres)
                .SingleOrDefault(p => p.Id == id);
            var seriesresc = new SeriesResources
            {
                Code = serie.Code,
                Name = serie.Name,
                TicketPrice = serie.TicketPrice,
                Description = serie.Description,
                Country = serie.Country,
                ReleaseDate = serie.ReleaseDate,
                Slug= serie.Slug,
                Id = serie.Id,
                ImagePath = serie.ImagePath,
                VideoPath = serie.VideoPath,

                NumberOfSeasons = serie.NumberOfSeasons,
                SeriesSelectedGenresresource = serie.SeriesSelectedGenres.Select(p => new SeriesPickedGenreresource
                {
                    name = p.name,
                    genreid = p.genreid,
                    Selected = p.Selected,
                }).ToList()
            };
            return seriesresc;
        }
        public SeriesResources GetSeriesByCode(string slug)

        {
            var serie = seriescontext.series
                .Include(x => x.SeriesSelectedGenres)
                .FirstOrDefault(p => p.Slug == slug);
            var seriesresc = new SeriesResources
            {
                Code = serie.Code,
                Name = serie.Name,
                TicketPrice = serie.TicketPrice,
                Description = serie.Description,
                Country = serie.Country,
                ReleaseDate = serie.ReleaseDate,
                Id = serie.Id,
                ImagePath = serie.ImagePath,
                VideoPath = serie.VideoPath,

                NumberOfSeasons = serie.NumberOfSeasons,
                Slug = serie.Slug,
                SeriesRatings = serie.seriesRatings.Select(p => new SeriesRatingresource
                {
                    Id = p.Id,
                    Value = p.Value,
                    Slug = p.Slug,
                    UserName = p.UserName,
                }).ToList(),
                SeriesCommentresources = serie.SeriesComments.Select(p => new SeriesCommentresource
                {
                    Id = p.Id,
                    User = p.User,
                    Post = p.Post,
                    PostDate = p.PostDate,
                    slug= p.Slug,
                }).ToList(),
                SeriesSelectedGenresresource = serie.SeriesSelectedGenres.Select(p => new SeriesPickedGenreresource
                {
                    name = p.name,
                    genreid = p.genreid,
                    Selected = p.Selected,
                }).ToList()
            };
            return seriesresc;
        }

        public bool AddSeries(SeriesResources serie)
        {
            var slug = $"{serie.Name}-{serie.Code}".ToLower().Replace(" ", "-");

            var genres = serie.SeriesSelectedGenresresource;
            genres = genres.Where(p => p.Selected).ToList();
            var NewSeries = new Series
            {
                Code = serie.Code,
                Name = serie.Name,
                TicketPrice = serie.TicketPrice,
                Description = serie.Description,
                Country = serie.Country,
                ReleaseDate = serie.ReleaseDate,
                ImagePath = serie.ImagePath,
                VideoPath = serie.VideoPath,

                Id = serie.Id,
                NumberOfSeasons = serie.NumberOfSeasons,
                Slug = slug,
            };
            var seriesgenre = new List<SeriesPickedGenre>();
            foreach (var item in genres)
            {
                var genre = new SeriesPickedGenre
                {
                    id = item.id,
                    name = item.name,
                    genreid = item.genreid,
                    Selected = item.Selected
                };
                seriesgenre.Add(genre);
            }
            NewSeries.SeriesSelectedGenres = seriesgenre;
            this.seriescontext.series.Add(NewSeries);
            this.seriescontext.SaveChanges();
            return true;
        }
        public SeriesResources Getgenredetails()
        {
            var list = new SeriesResources();
            var genres = new List<SeriesPickedGenreresource>();
            var data = seriescontext.genres.OrderBy(o => o.Name).ToList();
            foreach (var item in data)
            {
                var genre = new SeriesPickedGenreresource
                {
                    name = item.Name,
                    genreid = item.Id,
                };
                genres.Add(genre);
            }
            list.SeriesSelectedGenresresource = genres;

            return list;
        }
        public void UpdateSeries(SeriesResources series)
        {

            var existingMovie = seriescontext.series.FirstOrDefault(p => p.Id == series.Id);

            if (existingMovie != null)
            {
                existingMovie.TicketPrice = series.TicketPrice;
                existingMovie.Country = series.Country;
                existingMovie.Description = series.Description;
                existingMovie.Name = series.Name;
                existingMovie.ReleaseDate = series.ReleaseDate;
                existingMovie.ImagePath = series.ImagePath;
                existingMovie.VideoPath = series.VideoPath;

                existingMovie.Slug = series.Slug;
                existingMovie.NumberOfSeasons = series.NumberOfSeasons;



                seriescontext.SaveChanges();
            }
            }

            public void DeleteSeries(int id)
        {
            var movie = seriescontext.series.Include(c => c.SeriesSelectedGenres).FirstOrDefault(p => p.Id == id);
            seriescontext.seriespickedGenres.RemoveRange(movie.SeriesSelectedGenres);
            seriescontext.series.Remove(movie);
            seriescontext.SaveChanges();


        }
        public IEnumerable<SeriesResources> Search()
        {
            var movie = from c in seriescontext.series
                        select c;
            var movielist = new List<SeriesResources>();
            foreach (var movies in movie)
            {
                var movieresc = new SeriesResources
                {
                    Code = movies.Code,
                    Name = movies.Name,
                    TicketPrice = movies.TicketPrice,
                    Description = movies.Description,
                    Country = movies.Country,
                    ReleaseDate = movies.ReleaseDate,
                    ImagePath = movies.ImagePath,
                    VideoPath = movies.VideoPath,

                    Id = movies.Id,
                    NumberOfSeasons = movies.NumberOfSeasons,
                    Slug = movies.Slug,
                };
                movielist.Add(movieresc);
            }
            return movielist;
        }


        public void AddCommentForSeries(int seriesId, string user, string post, string slug)
        {
            var serie = seriescontext.series.SingleOrDefault(m => m.Slug == slug);
            if (serie != null)
            {
                var comment = new SeriesComment
                {
                    User = user,
                    Post = post,
                    PostDate = DateTime.Now,
                    SeriesId = seriesId,
                    Slug = slug,

                };

                if (serie.SeriesComments == null)
                    serie.SeriesComments = new List<SeriesComment>();

                serie.SeriesComments.Add(comment);
                seriescontext.SaveChanges();
            }
        }

        public void AddOrUpdateSeriesRating(int seriesId, int rating, string user, string slug)
        {
            var movie = seriescontext.series.SingleOrDefault(m => m.Slug == slug);

            var existingrating = movie.seriesRatings.FirstOrDefault(r => r.UserName == user);
            if (existingrating != null)
            {

                existingrating.Value = rating;

            }
            else
            {
                var ratings = new SeriesRatings
                {
                    Value = rating,
                    SeriesID = seriesId,
                    UserName = user,
                    Slug = slug,
                };
                if (movie.seriesRatings == null)
                    movie.seriesRatings = new List<SeriesRatings>();
                movie.seriesRatings.Add(ratings);
            }

            seriescontext.SaveChanges();
        }

        public decimal GetSeriesRating(string slug)
        {
            var movie = seriescontext.series.FirstOrDefault(r => r.Slug == slug);
            if (movie.seriesRatings == null || movie.seriesRatings.Count() == 0)
            {
                return 0;
            }
            var total_rating = movie.seriesRatings.Sum(r => r.Value);
            decimal average = total_rating / movie.seriesRatings.Count();
            return average;
        }
        public IEnumerable<Series> GetSeriesWithHighRating()
        {
            var highRatedMovieIds = seriescontext.seriesrating
                .Where(rating => rating.Value == 5)
                .Select(rating => rating.SeriesID)
                .Distinct();

            var highRatedMovies = seriescontext.series
                .Include(movie => movie.SeriesSelectedGenres)

                .Where(movie => highRatedMovieIds.Contains(movie.Id));

            return highRatedMovies.ToList();
        }

    }
}