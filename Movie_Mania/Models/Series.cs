using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movie_Mania.Models
{
    public class Series
    {
        public int Id { get; set; }
        public int Code { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]

        public string Description { get; set; }
        [Required]

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        
        [Required]
        [Range(0, double.PositiveInfinity)]
        public decimal TicketPrice { get; set; }
        [Required]

        public string Country { get; set; }
        [Required]

        public int NumberOfSeasons { get; set; }
        public string ImagePath { get; set; }
        public string VideoPath { get; set; }

        public List<SeriesPickedGenre> SeriesSelectedGenres { get; set; }
        public virtual List<SeriesComment> SeriesComments { get; set; }
        public SeriesComment NewComment { get; set; }

        public string Slug {get; set;}
        public virtual List<SeriesRatings> seriesRatings { get; set; }
        public SeriesRatings NewSeriesRating { get; set; }

    }
    public class SeriesPickedGenre
    {
        public int id { get; set; }
        public string name { get; set; }
        public int genreid { get; set; }
        public Genre Genre { get; set; }

        public bool Selected { get; set; } = false;
    }
    public class SeriesComment
    {
        public int Id { get; set; }
        public string User { get; set; }
        [Required]
        public string Post { get; set; }

        [DataType(DataType.Date)]
        public DateTime PostDate { get; set; } = DateTime.Now;
        public int SeriesId { get; set; }
        public virtual Series Series { get; set; }
        public string Slug { get; set; }

    }
    public class SeriesRatings
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int SeriesID { get; set; }
        public virtual Series Series { get; set; }
        public string UserName { get; set; }
        public string Slug { get; set; }
    }
}