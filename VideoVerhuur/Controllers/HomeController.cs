using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using VideoVerhuur.Services;
using VideoVerhuur.Models;
using System.Text.Json;
using VideoVerhuur.Repos;



namespace VideoVerhuur.Controllers
{
    public class HomeController : Controller
    {
        private readonly SQLVideoVerhuurRepo _service;

        public HomeController(SQLVideoVerhuurRepo service)
        {
            this._service = service;
        }

        Winkelmandje winkelmandje = new();
     

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Inloggen(Klanten klant)
        {
            var gevondenKlant = _service
            .GetAll()
            .FirstOrDefault(k => k.Naam == klant.Naam && k.PostCode == klant.PostCode);

            if (gevondenKlant != null)
            {
                // Zet klant in session als JSON string
                var klantJson = JsonSerializer.Serialize(gevondenKlant);
                HttpContext.Session.SetString("Klant", klantJson);

                return RedirectToAction("Welkom");
            }

            ModelState.AddModelError("", "Klant niet gevonden.");
            return View("Index", klant);
        }

        public IActionResult Welkom()
        {
            var klantJson = HttpContext.Session.GetString("Klant");
            if (!string.IsNullOrEmpty(klantJson))
            {
                var klant = JsonSerializer.Deserialize<Klanten>(klantJson);
                ViewBag.Genres = _service.GetGenres().ToList();
                return View(klant); // geef het model mee
            }

            return RedirectToAction("Index");
        }


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
            return View(films);
        }

        public IActionResult Rent(int filmId)
        {
            
            var film = _service.GetFilm(filmId);
            var klantJson = HttpContext.Session.GetString("Klant");
        
            if (klantJson.)
            {
                winkelmandje.FilmsInMandje.Add(film);
            }

            return View(winkelmandje);
            
        }

       

    }
}
