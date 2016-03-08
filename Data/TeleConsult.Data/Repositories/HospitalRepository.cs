namespace TeleConsult.Data.Repositories
{
    using System.Linq;

    using Microsoft.Practices.Unity;
    using TeleConsult.Data.Models;
    using Proxies;
    using Filters.Admin;
    using System.Collections.Generic;
    using TeleConsult.Common.Helpers;

    public class HospitalRepository : BaseRepository<Hospital>
    {
        [InjectionConstructor]
        public HospitalRepository(ITeleConsultDbContext context)
            : base(context)
        {
        }

        public List<HospitalProxy> Get(AdminFilter filter)
        {
            var result = this.All()
                .Where(filter.Name, h => h.Name.Contains(filter.Name))
                .Where(filter.IsDeleted.HasValue, h => h.IsDeleted == filter.IsDeleted.Value);

            filter.Count = result.Count();
            return this.GetProxy(result);
        }

        public bool HospitalExist(string name, int? id)
        {
            return this.All().Any(h => h.Name == name && h.Id != id);
        }

        private List<HospitalProxy> GetProxy(IQueryable<Hospital> result)
        {
            return result.Select(h => new HospitalProxy
            {
                Id = h.Id,
                Name = h.Name,
                Latitude = h.Latitude,
                Longitude = h.Longitude,
                Address = h.Address,
                Phone = h.Phone,
                IsDeleted = h.IsDeleted
            }).ToList();
        }
    }
}
