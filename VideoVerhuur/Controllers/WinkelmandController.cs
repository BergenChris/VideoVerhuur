using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using VideoVerhuur.Models;
using VideoVerhuur.Repos;

namespace VideoVerhuur.Controllers
{
    public class WinkelmandController: Controller
    {
        private readonly SQLVideoVerhuurRepo _service;

        public WinkelmandController(SQLVideoVerhuurRepo service)
        {
            this._service = service;
        }

      

        private List<int> GetWinkelmandProductIds()
        {
            var json = HttpContext.Session.GetString("winkelmand");

            // Als er nog niets is opgeslagen, starten we met een lege lijst
            return string.IsNullOrEmpty(json)
                ? new List<int>()
                : JsonSerializer.Deserialize<List<int>>(json);
        }

        public IActionResult Leegmaken()
        {
            HttpContext.Session.Remove("winkelmand");
            return Redirect("/Index");
        }




        [HttpGet]
        public IActionResult Index()
        {
            // Haal de lijst van product-ID's uit de session
            var winkelmandIds = GetWinkelmandProductIds();

            if (!winkelmandIds.Any())
            {
                return Redirect("/");
            }

            // Haal de producten op die in de winkelmand zitten
            var productenInMandje = _service.FilmsById(winkelmandIds);

            // Geef de producten door aan de view
            return View(productenInMandje);
        }
    }
}
