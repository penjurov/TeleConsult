namespace TeleConsult.Data.Repositories
{
    using System.Linq;

    using Microsoft.Practices.Unity;
    using TeleConsult.Data.Models;
    using Proxies;
    using Filters.Admin;
    using System.Collections.Generic;
    using TeleConsult.Common.Helpers;

    public class SpecialityRepository : BaseRepository<Speciality>
    {
        [InjectionConstructor]
        public SpecialityRepository(ITeleConsultDbContext context)
            : base(context)
        {
        }

        public List<SpecialityProxy> Get(AdminFilter filter)
        {
            var result = this.All()
                .Where(filter.Name, h => h.Name.Contains(filter.Name))
                .Where(filter.IsDeleted.HasValue, h => h.IsDeleted == filter.IsDeleted.Value);

            filter.Count = result.Count();
            return this.GetProxy(result);
        }

        public bool SpecialityExist(string name, int? id)
        {
            return this.All().Any(h => h.Name == name && h.Id != id);
        }

        private List<SpecialityProxy> GetProxy(IQueryable<Speciality> result)
        {
            return result.Select(h => new SpecialityProxy
            {
                Id = h.Id,
                Name = h.Name,
                IsDeleted = h.IsDeleted
            }).ToList();
        }
    }
}
