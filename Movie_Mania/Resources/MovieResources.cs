using Movie_Mania.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movie_Mania.Resources
{
    public class MovieResources
    {
        public int Id { get; set; }
        public int Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        [Required]

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
       
        [Required]
        [Range(0, double.PositiveInfinity)]

        public decimal TicketPrice { get; set; }

        public string Country { get; set; }

        public string ImagePath { get; set; }
        public HttpPostedFileBase Image { get; set; }

        public string VideoPath { get; set; }

        public List<PickedGenreresource> SelectedGenres { get; set; }
        public virtual List<MovieCommentresource> MovieCommentsresource { get; set; }
        public MovieCommentresource NewCommentresource { get; set; }

        public string Slug { get; set; }

        public RatingResource NewRating { get; set; }
        public virtual List<RatingResource> Ratings { get; set; }
       

    }
    public class PickedGenreresource
    {
        public int id { get; set; }
        public string name { get; set; }
        public int genreid { get; set; }
        public Genre Genre { get; set; }

        public bool Selected { get; set; } = false;

    }
    public class MovieCommentresource
    {
        public int Id { get; set; }
        public string User { get; set; }
        [Required]
        public string Post { get; set; }

        [DataType(DataType.Date)]
        public DateTime PostDate { get; set; } = DateTime.Now;
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
        public string Slug { get; set; }


    }
    public class RatingResource
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int MovieID { get; set; }

        public string UserName { get; set; }
        public virtual Movie Movie { get; set; }
        public string Slug { get; set; }
    }

}