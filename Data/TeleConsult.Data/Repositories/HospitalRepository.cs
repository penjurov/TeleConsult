namespace TeleConsult.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Filters.Admin;
    using Helpers;
    using Microsoft.Practices.Unity;
    using Proxies;
    using TeleConsult.Common.Helpers;
    using TeleConsult.Data.Models;
    
    public class HospitalRepository : BaseRepository<Hospital>
    {
        [InjectionConstructor]
        public HospitalRepository(ITeleConsultDbContext context)
            : base(context)
        {
        }

        public IEnumerable<HospitalProxy> Get(AdminFilter filter)
        {
            var result = this.All()
                .Where(filter.Name, h => h.Name.Contains(filter.Name))
                .Where(filter.IsDeleted.HasValue, h => h.IsDeleted == filter.IsDeleted.Value)
                .OrderBy(h => h.Id);

            filter.Count = result.Count();

            return this.GetProxy(result.OrderByFilter(filter).PageByFilter(filter));
        }

        public IEnumerable<HospitalProxy> GetActive()
        {
            var result = this.All().Where(s => !s.IsDeleted).ToList();
            return this.GetProxy(result);
        }

        public bool HospitalExist(string name, int? id)
        {
            return this.All().Any(h => h.Name == name && h.Id != id);
        }

        private IEnumerable<HospitalProxy> GetProxy(List<Hospital> result)
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
            });
        }
    }
}
