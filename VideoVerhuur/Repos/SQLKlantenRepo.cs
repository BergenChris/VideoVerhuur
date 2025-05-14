using Microsoft.EntityFrameworkCore;
using VideoVerhuur.Data;
using VideoVerhuur.Models;

namespace VideoVerhuur.Repos
{
    public class SQLKlantenRepo : IKlantenRepo
    {
        private readonly SampleDBContext context;


        public SQLKlantenRepo(SampleDBContext context)
        {
            this.context = context;
        }
        
        public Klanten? Get(int id) 
        {
            return context.Klanten.Find(id);
        }

        public IEnumerable<Klanten> GetAll()
        {
            return context.Klanten.AsNoTracking();
        }
    }
}
