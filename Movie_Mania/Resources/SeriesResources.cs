using Movie_Mania.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movie_Mania.Resources
{
    public class SeriesResources
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
        public List<SeriesPickedGenreresource> SeriesSelectedGenresresource { get; set; }

        public HttpPostedFileBase Image { get; set; }
        public string VideoPath { get; set; }


        public virtual List<SeriesCommentresource> SeriesCommentresources { get; set; }
        public SeriesCommentresource NewCommentresource { get; set; }
        public string Slug {get; set;}
        public SeriesRatingresource NewSeriesRating { get; set; }
        public virtual List<SeriesRatingresource> SeriesRatings { get; set; }

    }
    public class SeriesPickedGenreresource
    {
        public int id { get; set; }
        public string name { get; set; }
        public int genreid { get; set; }
        public Genre Genre { get; set; }

        public bool Selected { get; set; } = false;
    }
    public class SeriesCommentresource
    {
        public int Id { get; set; }
        public string User { get; set; }
        [Required]
        public string Post { get; set; }

        [DataType(DataType.Date)]
        public DateTime PostDate { get; set; } = DateTime.Now;
        public int seriesId { get; set; }
        public virtual Series Series { get; set; }
        public string slug { get; set; }
    }
    public class SeriesRatingresource
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int SeriesID { get; set; }

        public string UserName { get; set; }
        public virtual Series series { get; set; }
        public string Slug { get; set; }
    }
}