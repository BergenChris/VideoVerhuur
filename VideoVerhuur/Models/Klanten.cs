using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http.Headers;
using VideoVerhuur.Models;

namespace VideoVerhuur.Models
{
	public class Klanten
	{
		[Key]
		public int KlantId { get; set; }

		public string Naam { get; set; } = string.Empty;

		public string Voornaam { get; set; } = string.Empty;

		public string straat_Nr { get; set; } = string.Empty;

		public string PostCode { get; set; } = string.Empty;

		public string Gemeente { get; set; } = string.Empty;

		public string KlantStat { get; set; } = string.Empty;

		public int HuurAantal { get; set; }

		public DateTime DatumLid { get; set; } = DateTime.Today;

		public int Lidgeld { get; set; }

		public Winkelmandje? mandje { get; set; } = new();


	}
}
