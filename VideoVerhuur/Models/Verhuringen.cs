using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http.Headers;

namespace VideoVerhuur.Models
{
	public class Verhuringen
	{
		[Required]
		[Key]
		public int VerhuurId { get; set; }

		[ForeignKey("Klanten")]
		public int KlantId { get; set; }

		[ForeignKey("Films")]
		public int FilmId { get; set; }

		public DateTime VerhuurDatum { get; set; } = DateTime.Today;

	}
}
