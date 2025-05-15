using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using VideoVerhuur.Models;

namespace VideoVerhuur.ViewComponents
{
    public class WelkomsBericht: ViewComponent
    {
        private readonly IWebHostEnvironment env;
        public WelkomsBericht(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public IViewComponentResult Invoke()
        {
            var klantJson = HttpContext.Session.GetString("Klant");

            if (!string.IsNullOrEmpty(klantJson))
            {
                var klant = JsonSerializer.Deserialize<Klanten>(klantJson); // of je eigen Klant-model
                return View("WelkomsBericht",klant);
            }

            return Content(""); // geen klant? geen output
        }
    }
}
