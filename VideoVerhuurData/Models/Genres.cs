using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http.Headers;

namespace VideoVerhuurVDAB.Models
{
	public class Genres
	{
		[Key]
		public int GenreId { get; set; }

		public string GenraNaam { get; set; } = string.Empty;
	}
}
