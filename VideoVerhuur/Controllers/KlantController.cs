using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VideoVerhuur.Models;
using VideoVerhuur.Services;
using VideoVerhuur.Models;

namespace VideoVerhuur.Controllers
{
	public class KlantController : Controller
	{
		private readonly KlantenService klantenservice;

		public KlantController(KlantenService service)
		{
			this.klantenservice = service;
		}

		
	}
}
