using Microsoft.EntityFrameworkCore;
using VideoVerhuur.Data;
using VideoVerhuur.Models;


namespace VideoVerhuur.Repos
{
    public class SQLVideoVerhuurRepo : IVideoVerhuurRepo
    {
        private readonly SampleDBContext context;

        public SQLVideoVerhuurRepo(SampleDBContext context)
        {
            this.context = context;
        }

       
        public Klanten? Get(int id)
        {
            return context.Klanten.FirstOrDefault(x => x.KlantId == id);
        }

        public IEnumerable<Klanten> GetAll()
        {
            return context.Klanten;
        }

        public IEnumerable<Genres> GetGenres()
        {
            return context.Genres;
        }
        public Genres? GetGenre(int id)
        {
            return context.Genres.FirstOrDefault(x => x.GenreId == id);
        }

        public Films? GetFilm(int filmId)
        {
            return context.Films.FirstOrDefault(x => x.FilmId == filmId);
        }

        public IEnumerable<Films> FilmsByGenre(int genreId)
        {
            return context.Films.Where(x => x.GenreId == genreId).ToList() ;
        }


        public IEnumerable<Films> FilmsById(List<int> ids)
        {
            return context.Films.Where(x => ids.Contains(x.FilmId)).ToList();
        }

        public void UpdateFilm(Films film)
        {
            context.Films.Update(film);
            context.SaveChanges();
         
        }
    }
}
