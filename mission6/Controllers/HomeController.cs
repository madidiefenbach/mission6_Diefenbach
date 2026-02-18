using Microsoft.AspNetCore.Mvc;
using mission6.Models;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace mission6.Controllers
{
    public class HomeController : Controller
    {
        private FilmFormContext _context;

        public HomeController(FilmFormContext temp)
        {
            _context = temp;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult FilmForm()
        {
            try
            {
                ViewBag.Category = _context.Category // allows drop down
                    .OrderBy(x => x.CategoryName).ToList();
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Database error: {ex.Message}";
                ViewBag.Category = new List<mission6_Diefenbach.Models.Category>();
            }
            return View("FilmForm", new Movie());
        }

        [HttpPost]
        public IActionResult FilmForm(Movie response)
        {
            // Only save if validation passes (required: Title, Edited)
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Movie.Add(response);
                    _context.SaveChanges();
                    return View("Confirmation", response);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error saving movie: {ex.Message}");
                }
            }
            try
            {
                ViewBag.Category = _context.Category.OrderBy(x => x.CategoryName).ToList();
            }
            catch
            {
                ViewBag.Category = new List<mission6_Diefenbach.Models.Category>();
            }
            return View(response);
        }
        public IActionResult GetToKnow()
        {
            return View();
        }

        public IActionResult ShowMovie()
        {
            try
            {
                var movies = _context.Movie
                    .OrderBy(x => x.Title) // show alphabetcal order
                    .ToList();
                return View(movies);
            }
            catch (Exception ex) // error handling
            {
                var msg = ex.InnerException != null ? $"{ex.Message} | {ex.InnerException.Message}" : ex.Message;
                ViewBag.Error = msg;
                return View(new List<Movie>());
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var recordToEdit = _context.Movie.Single(x => x.MovieID == id);
            ViewBag.Category = _context.Category.OrderBy(x => x.CategoryName).ToList();
            return View("FilmForm", recordToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Movie updateInfo) // post for edit
        {
            if (ModelState.IsValid)
            {
                _context.Update(updateInfo);
                _context.SaveChanges();
                return RedirectToAction("ShowMovie");
            }
            ViewBag.Category = _context.Category.OrderBy(x => x.CategoryName).ToList();
            return View("FilmForm", updateInfo);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var recordToDelete = _context.Movie
                .Single(x => x.MovieID == id);
            return View(recordToDelete);

        }

        [HttpPost]
        public IActionResult Delete(Movie movie) 
        {
            var movieToDelete = _context.Movie.Single(x => x.MovieID == movie.MovieID);
            _context.Movie.Remove(movieToDelete);
            _context.SaveChanges(); 
            return RedirectToAction("ShowMovie");
        }
    }
}
