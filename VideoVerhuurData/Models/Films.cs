using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http.Headers;

namespace VideoVerhuurVDAB.Models
{
	public class Films
	{
		[Key]
		public int FilmId { get; set; }

		public string titel { get; set; }

		[ForeignKey("Genres")]
		public int GenreId { get; set; }

		public int InVoorraad { get; set; }

		public int UitVoorraad { get; set; }

		public double Prijs { get; set; }

		public int TotaalVerhuurd { get; set; }

	}
}
