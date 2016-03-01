namespace TeleConsult.Data.Repositories
{
    using System.Linq;

    using Microsoft.Practices.Unity;
    using TeleConsult.Data.Models;

    public class HospitalRepository : BaseRepository<Hospital>
    {
        [InjectionConstructor]
        public HospitalRepository(ITeleConsultDbContext context)
            : base(context)
        {
        }

        public IQueryable<Hospital> GetAll()
        {
            return this.All();
        }

        public IQueryable<Hospital> GetActive()
        {
            return this.All().Where(r => r.Deleted == false);

        }
    }
}
