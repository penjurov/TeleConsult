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
    
    public class SpecialityRepository : BaseRepository<Speciality>
    {
        [InjectionConstructor]
        public SpecialityRepository(ITeleConsultDbContext context)
            : base(context)
        {
        }

        public IEnumerable<SpecialityProxy> Get(AdminFilter filter)
        {
            var result = this.All()
                .Where(filter.Name, s => s.Name.Contains(filter.Name))
                .Where(filter.IsDeleted.HasValue, s => s.IsDeleted == filter.IsDeleted.Value)
                .OrderBy(s => s.Id);

            filter.Count = result.Count();
            return this.GetProxy(result.OrderByFilter(filter).PageByFilter(filter));
        }

        public IEnumerable<SpecialityProxy> GetActive()
        {
            var result = this.All().Where(s => !s.IsDeleted).ToList();
            return this.GetProxy(result);
        }

        public bool SpecialityExist(string name, int? id)
        {
            return this.All().Any(s => s.Name == name && s.Id != id);
        }

        private IEnumerable<SpecialityProxy> GetProxy(List<Speciality> result)
        {
            return result.Select(s => new SpecialityProxy
            {
                Id = s.Id,
                Name = s.Name,
                IsDeleted = s.IsDeleted
            });
        }
    }
}
