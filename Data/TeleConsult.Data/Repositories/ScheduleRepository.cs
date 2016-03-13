namespace TeleConsult.Data.Repositories
{
    using System.Linq;
    using System.Collections.Generic;

    using Common.Helpers;
    using Filters.Admin;
    using Helpers;
    using Microsoft.Practices.Unity;
    using Models.Enumerations;
    using Proxies;
    using TeleConsult.Data.Models;
    using System;
    public class ScheduleRepository: BaseRepository<Schedule>
    {
        [InjectionConstructor]
        public ScheduleRepository(ITeleConsultDbContext context)
                : base(context)
        {
        }

        public IEnumerable<ScheduleProxy> Get(SchedulesFilter filter)
        {
            var result = this.All()
                .Where(filter.StartDate, s => filter.StartDate <= s.StartDate)
                .Where(filter.EndDate, s => filter.EndDate >= s.EndDate)
                .Where(filter.SpecialistId, s => s.SpecialistId == filter.SpecialistId)
                .Where(filter.Description, s => s.Description.Contains(filter.Description));

            return this.GetProxy(result);
        }

        private IEnumerable<ScheduleProxy> GetProxy(IQueryable<Schedule> proxies)
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time");

            var result = proxies.Select(s => new ScheduleProxy
            {
                Id = s.Id,
                Description = s.Description,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                IsAllDay = s.IsAllDay,
                SpecialistId = s.SpecialistId,
                Specialist = new SpecialistProxy
                {
                    Title = s.Specialist.Title,
                    FirstName = s.Specialist.FirstName,
                    LastName = s.Specialist.LastName
                },
                SpecialistSpeciality = s.Specialist.Speciality.Name
            }).ToList();

            return result;
        }
    }
}
