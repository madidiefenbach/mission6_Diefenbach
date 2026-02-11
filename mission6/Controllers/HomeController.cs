using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mission6.Models;

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
            return View();
        }
        [HttpPost]
        public IActionResult FilmForm(FilmForm response)
        {
            _context.FilmForms.Add(response);
            _context.SaveChanges();
            return View("Confirmation", response);
        }
        public IActionResult GetToKnow()
        {
            return View();
        }
    }
}
