using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http.Headers;

namespace VideoVerhuur.Models
{
	public class Genres
	{
		[Key]
		public int GenreId { get; set; }

		public string GenreNaam { get; set; } = string.Empty;
	}
}
