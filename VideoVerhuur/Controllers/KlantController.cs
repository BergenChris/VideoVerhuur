using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VideoVerhuur.Models;
using VideoVerhuur.Services;
using VideoVerhuur.Models;
using System.Text.Json;
using VideoVerhuur.Repos;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using VideoVerhuur.Filters;

namespace VideoVerhuur.Controllers
{
    [KlantSessionAuthorize]
    public class KlantController : Controller
	{
        private readonly SQLVideoVerhuurRepo _service;
        private Klanten _klant;

        public KlantController(SQLVideoVerhuurRepo service)
        {
            this._service = service;
        }

        private void Klant()
        {
            var klantJson = HttpContext.Session.GetString("Klant");
            if (!string.IsNullOrEmpty(klantJson))
            {
                _klant = JsonSerializer.Deserialize<Klanten>(klantJson);
            }
        }

        public IActionResult LastGenre(int genreId)
        {
            // Sla het genreId op
            HttpContext.Session.SetInt32("LaatsteGenreId", genreId);

            // Haal films van dit genre op, etc.
            var films = _service.FilmsByGenre(genreId);
            return View(films);
        }

        [HttpGet]
        public IActionResult Index()
        {
            Klant();
            if (_klant != null)
            {
                ViewBag.Genres = _service.GetGenres().ToList();
                return View(_klant); // geef het model mee
            }
            else
            {
                return Redirect("/");
            }
           
        }

        [HttpPost]
        public IActionResult Index(int genreId)
        {
            // Sla de genreId op in de session
            HttpContext.Session.SetInt32("LaatsteGenreId", genreId);

            // Redirect naar de Genre-pagina om films voor dit genre te tonen
            return RedirectToAction("Genre");
        }

        [HttpGet]
        public IActionResult Genre()
        {

            Klant();
            ViewBag.Klant = _klant;
            // Haal genreId uit de session
            var genreId = HttpContext.Session.GetInt32("LaatsteGenreId");

            // Als genreId niet bestaat in de session, kun je een standaard waarde instellen of NotFound teruggeven
            if (!genreId.HasValue)
            {
                genreId = 1;
            }

            // Verkrijg films voor het genre
            var films = _service.FilmsByGenre(genreId.Value);

            // Als er geen films zijn voor het genre, geef NotFound terug
            if (films == null || !films.Any())
            {
                return NotFound();
            }

            // Zet de gegevens in ViewBag
            ViewBag.GenreId = genreId.Value;
            ViewBag.Films = films;
            ViewBag.Klant = _klant;

            return View("Genre", films);
        }






        [HttpGet]
        public IActionResult Winkelmandje(int? filmId)
        {
            Klant();
            ViewBag.Klant = _klant;

            var laatsteGenreId = HttpContext.Session.GetInt32("LaatsteGenreId");
            if (laatsteGenreId.HasValue)
            {
                ViewBag.LaatsteGenreId = laatsteGenreId.Value;
            }

            // Session ophalen
            var winkelmandjeJson = HttpContext.Session.GetString("Winkelmandje");

            // Lijst van int (filmIds) deserialiseren of nieuwe lijst starten
            List<int> filmIds = string.IsNullOrEmpty(winkelmandjeJson)
                ? new List<int>()
                : JsonSerializer.Deserialize<List<int>>(winkelmandjeJson);

            var productenInMandje = _service.FilmsById(filmIds);

            return View("Winkelmandje", productenInMandje);
        }

        [HttpPost]
        public IActionResult Winkelmandje(int filmId)
        {
            Klant();
            ViewBag.Klant = _klant;

            var laatsteGenreId = HttpContext.Session.GetInt32("LaatsteGenreId");
            if (laatsteGenreId.HasValue)
            {
                ViewBag.LaatsteGenreId = laatsteGenreId.Value;
            }

            // Session ophalen
            var winkelmandjeJson = HttpContext.Session.GetString("Winkelmandje");

            // Lijst van int (filmIds) deserialiseren of nieuwe lijst starten
            List<int> filmIds = string.IsNullOrEmpty(winkelmandjeJson)
                ? new List<int>()
                : JsonSerializer.Deserialize<List<int>>(winkelmandjeJson);

            var film = _service.GetFilm(filmId);
            if (film == null)
            {
                return NotFound();
            }

            // Film-ID toevoegen aan de lijst met controle
            if (!filmIds.Contains(filmId))
            {
                filmIds.Add(filmId);

                // Hier ga je de voorraad verlagen — alleen als je dat in je _service of DB doorvoert
                film.InVoorraad -= 1;
                _service.UpdateFilm(film); // ⬅️ Zorg dat deze methode bestaat en opslaat

                //Lijst opslaan in session
                var nieuweJson = JsonSerializer.Serialize(filmIds);
                HttpContext.Session.SetString("Winkelmandje", nieuweJson);
                HttpContext.Session.SetInt32("LaatsteGenreId", film.GenreId);
            }

            var productenInMandje = _service.FilmsById(filmIds);

            // Na het toevoegen van een product redirecten naar de winkelmand (GET)
            return RedirectToAction("Winkelmandje", new { filmId = (int?)null });
        }

        [HttpGet]
        public IActionResult DeleteRent(int filmId)
        {
            Klant();
            ViewBag.Klant = _klant;

            // Haal het winkelmandje op uit de session
            var winkelmandjeJson = HttpContext.Session.GetString("Winkelmandje");


            if (string.IsNullOrEmpty(winkelmandjeJson))
            {
                // Als het winkelmandje leeg is, redirect dan naar de index of winkelmand pagina
                return RedirectToAction("Winkelmandje");
            }

            // Deserialize de lijst van filmIds
            var filmIds = JsonSerializer.Deserialize<List<int>>(winkelmandjeJson);

            var film = _service.GetFilm(filmId);
            


            // Als de lijst leeg is, kun je de session verwijderen
            if (!filmIds.Any())
            {
                HttpContext.Session.Remove("Winkelmandje");
            }
            else
            {
                // Sla de bijgewerkte lijst terug in de session
                var nieuweJson = JsonSerializer.Serialize(filmIds);
                HttpContext.Session.SetString("Winkelmandje", nieuweJson);
            }

            // Redirect terug naar het winkelmandje om de wijziging te tonen




            return View("DeleteRent",film);
        }

        [HttpPost]
        public IActionResult DeleteRentExecute(int filmId)
        {
            Klant();
            ViewBag.Klant = _klant;

            // Haal het winkelmandje op uit de session
            var winkelmandjeJson = HttpContext.Session.GetString("Winkelmandje");


            if (string.IsNullOrEmpty(winkelmandjeJson))
            {
                // Als het winkelmandje leeg is, redirect dan naar de index of winkelmand pagina
                return RedirectToAction("Winkelmandje");
            }

            // Deserialize de lijst van filmIds
            var filmIds = JsonSerializer.Deserialize<List<int>>(winkelmandjeJson);

            // Verwijder de filmId uit de lijst
            filmIds.Remove(filmId);
            var film = _service.GetFilm(filmId);
            film.InVoorraad++;
            _service.UpdateFilm(film);


            // Als de lijst leeg is, kun je de session verwijderen
            if (!filmIds.Any())
            {
                HttpContext.Session.Remove("Winkelmandje");
            }
            else
            {
                // Sla de bijgewerkte lijst terug in de session
                var nieuweJson = JsonSerializer.Serialize(filmIds);
                HttpContext.Session.SetString("Winkelmandje", nieuweJson);
            }

            // Redirect terug naar het winkelmandje om de wijziging te tonen

            var productenInMandje = _service.FilmsById(filmIds);

            return View("Winkelmandje", productenInMandje);
        }

        [HttpGet]
        public IActionResult Afrekenen()
        {
            Klant();
            ViewBag.Klant = _klant;

            var laatsteGenreId = HttpContext.Session.GetInt32("LaatsteGenreId");
            if (laatsteGenreId.HasValue)
            {
                ViewBag.LaatsteGenreId = laatsteGenreId.Value;
            }

            // Session ophalen
            var winkelmandjeJson = HttpContext.Session.GetString("Winkelmandje");

            // Lijst van int (filmIds) deserialiseren of nieuwe lijst starten
            List<int> filmIds = string.IsNullOrEmpty(winkelmandjeJson)
                ? new List<int>()
                : JsonSerializer.Deserialize<List<int>>(winkelmandjeJson);

            var productenInMandje = _service.FilmsById(filmIds);

            foreach (var film in productenInMandje)
            {
                Verhuringen nieuweVerhuring =
                    new Verhuringen
                    {
                        KlantId = _klant.KlantId,
                        FilmId = film.FilmId,
                        VerhuurDatum = DateTime.Today
                    };
                _service.Verhuur(nieuweVerhuring);
            }



            return View("Afrekenen", productenInMandje);

        }


        public IActionResult Logout()
        {
            // Wis de gebruikersspecifieke sessiegegevens
            HttpContext.Session.Clear();

            // Of wis de hele sessie:
            // HttpContext.Session.Clear();

            // Redirect naar de inlogpagina of een andere pagina
            return Redirect("/");
        }



    }
}
