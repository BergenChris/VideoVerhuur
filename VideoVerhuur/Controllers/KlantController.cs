using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VideoVerhuur.Models;
using VideoVerhuur.Services;
using VideoVerhuur.Models;
using System.Text.Json;
using VideoVerhuur.Repos;

namespace VideoVerhuur.Controllers
{
	public class KlantController : Controller
	{
        private readonly SQLVideoVerhuurRepo _service;

        public KlantController(SQLVideoVerhuurRepo service)
        {
            this._service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var klantJson = HttpContext.Session.GetString("Klant");
            if (!string.IsNullOrEmpty(klantJson))
            {
                var klant = JsonSerializer.Deserialize<Klanten>(klantJson);
                ViewBag.Genres = _service.GetGenres().ToList();
                return View(klant); // geef het model mee
            }

            return Redirect("/");
        }

        [HttpGet]
        public IActionResult Genre(int genreId)
        {
            // Haal de films op voor dit genre
            var films = _service.FilmsByGenre(genreId);

            if (films == null || !films.Any())
            {
                return NotFound(); // Geen films gevonden voor dit genre
            }

            // Zet films in de ViewBag zodat je ze kunt gebruiken in de view
            ViewBag.GenreId = genreId;  // Dit is optioneel: voeg genreId toe voor eventueel gebruik in de view
            ViewBag.Films = films;

            // Je kunt de films ook rechtstreeks naar de view sturen
            return View("Genre",films);
        }

    }
}
