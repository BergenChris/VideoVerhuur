using VideoVerhuur.Data;
using VideoVerhuur.Models;
using VideoVerhuur.Repos;

namespace VideoVerhuur.Services
{
    public class KlantenService : IKlantenRepo
    {

        private readonly IKlantenRepo repo;
        public KlantenService(IKlantenRepo repo)
        {
            this.repo= repo;
        }

        public Klanten? Get(int id)
        {
            return repo.Get(id);
        }

        public IEnumerable<Klanten> GetAll()
        { 
            return repo.GetAll();
        } 



    }
}
