using VideoVerhuur.Models;

namespace VideoVerhuur.Repos
{
    public interface IKlantenRepo
    {
        Klanten? Get(int id);
        IEnumerable<Klanten> GetAll();
    }

    public interface IFilmUitlenen
    {
        Films? get(int id);
        IEnumerable<Films> GetAll();
     
    }

    


}
