using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Movie_Mania.Models
{
    public class MovieContext : DbContext
    {

        public DbSet<Registers> registers { get; set; }
       public DbSet<Logins> logins { get; set; }
        public DbSet<Genre> genres { get; set; }
       public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieComment> MoviesComment { get; set; }
        public DbSet<PickedGenre> pickedGenres { get; set; }
        public DbSet<Series> series { get; set; }
        public DbSet<SeriesPickedGenre> seriespickedGenres { get; set; }
        public DbSet<Rating> rating { get; set; }
        public DbSet<SeriesRatings> seriesrating { get; set; }










    }
}