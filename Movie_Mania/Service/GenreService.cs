using Movie_Mania.IService;
using Movie_Mania.Models;
using System.Collections.Generic;
using System.Linq;

namespace Movie_Mania.Service
{
    public class GenreService : IGenreService
    {
        private readonly MovieContext movieContext;
        public GenreService()
        {
            movieContext = new MovieContext();
        }
        public Genre SaveGenre(Genre genre)
        {
            this.movieContext.genres.Add(genre);
            this.movieContext.SaveChanges();
            return genre;
        }

        public IEnumerable<Genre> GetAll()
        {
            return movieContext.genres.ToList();
        }
        public Genre GetGenreByID(int id)
        {
            return this.movieContext.genres.Find(id);
        }

        public void DeleteGenre(int id)
        {
            var genre = GetGenreByID(id);
            this.movieContext.genres.Remove(genre);
            this.movieContext.SaveChanges();
        }

    }
}