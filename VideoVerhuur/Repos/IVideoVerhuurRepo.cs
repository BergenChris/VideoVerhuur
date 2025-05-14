using VideoVerhuur.Models;

namespace VideoVerhuur.Repos
{
    public interface IVideoVerhuurRepo
    {
        //Klanten
        Klanten? Get(int id);
        IEnumerable<Klanten> GetAll();
      

        //Genres
        IEnumerable<Genres> GetGenres();
        Genres? GetGenre(int id);

        Films? GetFilm(int id);

        IEnumerable<Films> FilmsByGenre(int id);

        public IEnumerable<Films> FilmsById(List<int> ids);


    }
}
