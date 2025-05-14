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

                return RedirectToAction("Index", "Klant");
            }

            ModelState.AddModelError("", "Klant niet gevonden.");
            return View("Index", klant);
        }

        
        

       

    }
}
