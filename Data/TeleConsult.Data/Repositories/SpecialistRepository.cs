namespace TeleConsult.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Common;
    using Filters.Admin;
    using Helpers;
    using Microsoft.Practices.Unity;
    using Models.Enumerations;
    using Proxies;
    using TeleConsult.Common.Helpers;
    using TeleConsult.Data.Models;
    
    public class SpecialistRepository : BaseRepository<Specialist>
    {
        [InjectionConstructor]
        public SpecialistRepository(ITeleConsultDbContext context)
            : base(context)
        {
        }

        public IEnumerable<SpecialistProxy> Get(SpecialistFilter filter)
        {
            var result = this.All()
                .Where(filter.Name, s => s.FirstName.Contains(filter.Name) || s.LastName.Contains(filter.Name))
                .Where(filter.Title, s => s.Title == (Title)filter.Title.Value)
                .Where(filter.HospitalId, s => s.HospitalId == filter.HospitalId.Value)
                .Where(filter.SpecialityId, s => s.SpecialityId == filter.SpecialityId.Value)
                .Where(filter.IsDeleted.HasValue, s => s.IsDeleted == filter.IsDeleted.Value)
                .OrderBy(s => s.Id);

            filter.Count = result.Count();

            if (filter.SortBy == "SpecialityName")
            {
                filter.SortBy = "SpecialityId";
            }

            if (filter.SortBy == "HospitalName")
            {
                filter.SortBy = "HospitalId";
            }

            if (filter.SortBy == "Names")
            {
                if (filter.SortDirection == SortDirection.Asc)
                {
                    return this.GetProxy(result.OrderBy(s => s.Title).ThenBy(s => s.FirstName).ThenBy(s => s.LastName).PageByFilter(filter));
                }
                else
                {
                    return this.GetProxy(result.OrderByDescending(s => s.Title).ThenByDescending(s => s.FirstName).ThenByDescending(s => s.LastName).PageByFilter(filter));
                } 
            }
            else
            {
                return this.GetProxy(result.OrderByFilter(filter).PageByFilter(filter));
            }
        }

        public bool SpecialistExist(string uin, string id)
        {
            return this.All().Any(s => s.Uin == uin && s.Id != id);
        }

        private IEnumerable<SpecialistProxy> GetProxy(List<Specialist> result)
        {
            return result.Select(s => new SpecialistProxy
            {
                Id = s.Id,
                Title = s.Title,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Uin = s.Uin,
                PhoneNumber = s.PhoneNumber,
                Email = s.Email,
                SpecialityId = s.SpecialityId,
                SpecialityName = s.Speciality.Name,
                UserName = s.UserName,
                HospitalId = s.HospitalId,
                HospitalName = s.Hospital.Name,
                IsDeleted = s.IsDeleted
            });
        }
    }
}
