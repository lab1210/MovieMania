using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Movie_Mania.Service;
using Movie_Mania.Models;
using Movie_Mania.Resources;
using System.IO;
using Movie_Mania.IService;
using Movie_Mania.Migrations;

namespace Movie_Mania.Controllers
{
    public class MovieController : Controller
    {
        private readonly MovieService movieService;
        private readonly GenreService genreService;
        private readonly RegisterService registerService;
        private readonly SeriesService seriesService;


        public MovieController()
        {
            movieService = new MovieService();
            seriesService = new SeriesService();
            genreService = new GenreService();
            registerService = new RegisterService();

        }
        public ActionResult AddMovie()
        {
            var loggedInUserName = HttpContext.Session["LoggedInUserName"] as string;
            ViewBag.LoggedInUserName = loggedInUserName;
            var details = movieService.Getgenredetails();
            
            return View(details);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMovie(MovieResources movie)
        {
            string filePath = "";

            if (ModelState.IsValid)
            {
                if (movie.Image != null && movie.Image.ContentLength > 0)
                {

                    var filename = Path.GetFileName(movie.Image.FileName);
                    var path = Path.Combine(Server.MapPath("~/MoviePics"), filename);
                    movie.Image.SaveAs(path);
                    filePath = "/MoviePics/" + filename;
                }

              

            }
            movie.ImagePath = filePath;

            Random random = new Random();
            int code = random.Next(1000, 10000);
            movie.Code = code;
            var result = movieService.AddMovie(movie);
            if (result)

                return RedirectToAction("MovieList");
            else

                return View(movie);


        }
        public ActionResult MovieList()
        {
            var loggedInUserName = HttpContext.Session["LoggedInUserName"] as string;
            ViewBag.LoggedInUserName = loggedInUserName;
            return View(movieService.GetAllMovies());
        }
        public ActionResult EditMovie(int id)
        {
            var loggedInUserName = HttpContext.Session["LoggedInUserName"] as string;
            ViewBag.LoggedInUserName = loggedInUserName;
            var movie = movieService.GetMovieById(id);
          
            return View(movie);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMovie(MovieResources movie)
        {
            string filePath = "";

            if (ModelState.IsValid)
            {
                if (movie.Image != null && movie.Image.ContentLength > 0)
                {

                    

                    var filename = Path.GetFileName(movie.Image.FileName);
                    var path = Path.Combine(Server.MapPath("~/MoviePics"), filename);
                    movie.Image.SaveAs(path);
                    filePath = "/MoviePics/" + filename;
                }

               
                movie.ImagePath = filePath;


                movieService.UpdateMovie(movie);
                return RedirectToAction("MovieList");
            }

            return View(movie);
        }
        [HttpGet]
        public ActionResult DeleteMovie(int id)
        {
            movieService.DeleteMovie(id);
            return RedirectToAction("MovieList");

        }
        public ActionResult AddSeries()
        {
            var loggedInUserName = HttpContext.Session["LoggedInUserName"] as string;
            ViewBag.LoggedInUserName = loggedInUserName;
            var details = seriesService.Getgenredetails();

            return View(details);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSeries(SeriesResources series)
        {

            string filePath = "";

            if (ModelState.IsValid)
            {
                if (series.Image != null && series.Image.ContentLength > 0)
                {

                    var allowedFileTypes = new[] { "image/jpeg", "image/png", "image/JPG" };
                    if (!allowedFileTypes.Contains(series.Image.ContentType))
                    {

                        ModelState.AddModelError("file", "Only JPEG,JPG and PNG file types are allowed.");
                    }

                    var filename = Path.GetFileName(series.Image.FileName);
                    var path = Path.Combine(Server.MapPath("~/MoviePics"), filename);
                    series.Image.SaveAs(path);
                    filePath = "/MoviePics/" + filename;
                }

              

            }
            series.ImagePath = filePath;

            Random random = new Random();
            int code;
            code = random.Next(1000, 10000);
            series.Code = code;
            var result = seriesService.AddSeries(series);
            if (result)

                return RedirectToAction("SeriesList");
            else

                return View(series);

        }
        public ActionResult SeriesList()
        {
            var loggedInUserName = HttpContext.Session["LoggedInUserName"] as string;
            ViewBag.LoggedInUserName = loggedInUserName;
            return View(seriesService.GetAllSeries());

        }
        public ActionResult EditSeries(int id)
        {
            var loggedInUserName = HttpContext.Session["LoggedInUserName"] as string;
            ViewBag.LoggedInUserName = loggedInUserName;
            var movie = seriesService.GetSeriesById(id);
            
            return View(movie);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSeries(SeriesResources movie)
        {
            string filePath = "";


            if (ModelState.IsValid)
            {
                if (movie.Image != null && movie.Image.ContentLength > 0)
                {

                    var allowedFileTypes = new[] { "image/jpeg", "image/png", "image/JPG" };
                    if (!allowedFileTypes.Contains(movie.Image.ContentType))
                    {

                        ModelState.AddModelError("file", "Only JPEG,JPG and PNG file types are allowed.");
                    }

                    var filename = Path.GetFileName(movie.Image.FileName);
                    var path = Path.Combine(Server.MapPath("~/MoviePics"), filename);
                    movie.Image.SaveAs(path);
                    filePath = "/MoviePics/" + filename;
                }
               
                movie.ImagePath = filePath;

                seriesService.UpdateSeries(movie);
                return RedirectToAction("SeriesList");
            }

            return View(movie);
        }
        [HttpGet]
        public ActionResult DeleteSeries(int id)
        {
            seriesService.DeleteSeries(id);
            return RedirectToAction("SeriesList");

        }
        public ActionResult GenreList()
        {
            var loggedInUserName = HttpContext.Session["LoggedInUserName"] as string;
            ViewBag.LoggedInUserName = loggedInUserName;
            return View(genreService.GetAll());
        }
        public ActionResult AddGenre()
        {
            var loggedInUserName = HttpContext.Session["LoggedInUserName"] as string;
            ViewBag.LoggedInUserName = loggedInUserName;
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddGenre(Genre Genre)
        {
            if (ModelState.IsValid)
            {
                genreService.SaveGenre(Genre);
                return RedirectToAction("GenreList");

            }
            return View(Genre);
        }
        [HttpGet]
        public ActionResult DeleteGenre(int id)
        {
            genreService.DeleteGenre(id);
            return RedirectToAction("GenreList");


        }
        public ActionResult Create_Admin()
        {
            var loggedInUserName = HttpContext.Session["LoggedInUserName"] as string;
            ViewBag.LoggedInUserName = loggedInUserName;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_Admin(Registers register)
        {
            if (ModelState.IsValid)
            {
                register.Role = "Admin";
                registerService.AddUser(register);
                return RedirectToAction("Admin_List", "Movie");

            }
            return View(register);
        }
        public ActionResult Admin_List()
        {
            var loggedInUserName = HttpContext.Session["LoggedInUserName"] as string;
            ViewBag.LoggedInUserName = loggedInUserName;
            return View(registerService.AdminList());
        }


    }
}