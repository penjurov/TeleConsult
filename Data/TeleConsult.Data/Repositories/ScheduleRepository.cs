namespace TeleConsult.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Common.Helpers;
    using Filters.Admin;
    using Microsoft.Practices.Unity;
    using Proxies;
    using TeleConsult.Data.Models;

    public class ScheduleRepository : BaseRepository<Schedule>
    {
        [InjectionConstructor]
        public ScheduleRepository(ITeleConsultDbContext context)
                : base(context)
        {
        }

        public IEnumerable<ScheduleProxy> Get(SchedulesFilter filter)
        {
            var result = this.All()
                .Where(filter.StartDate.HasValue && filter.EndDate.HasValue, s => s.StartDate >= filter.StartDate && s.EndDate <= filter.EndDate)
                .Where(filter.StartDate.HasValue && !filter.EndDate.HasValue, s => s.StartDate >= filter.StartDate)
                .Where(filter.EndDate.HasValue && !filter.StartDate.HasValue, s => s.EndDate <= filter.EndDate)
                .Where(filter.Date.HasValue, s => s.StartDate <= filter.Date && filter.Date <= s.EndDate)
                .Where(filter.SpecialistId, s => s.SpecialistId == filter.SpecialistId)
                .Where(filter.Description, s => s.Description.Contains(filter.Description))
                .Where(filter.SpecialityId, s => s.Specialist.SpecialityId == filter.SpecialityId);

            return this.GetProxy(result);
        }

        public IEnumerable<ScheduleProxy> GetForToday()
        {
            var now = DateTime.Now;
            var result = this.All()
                .Where(s => s.StartDate < now && now < s.EndDate);

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
                    LastName = s.Specialist.LastName,
                    UserName = s.Specialist.UserName,
                    SpecialityId = s.Specialist.SpecialityId,
                    SpecialityName = s.Specialist.Speciality.Name
                },
                SpecialistSpeciality = s.Specialist.Speciality.Name
            }).ToList();

            result.ForEach(r =>
            {
                r.StartDate = r.StartDate.HasValue ? TimeZoneInfo.ConvertTimeFromUtc(r.StartDate.Value, timeZone) : r.StartDate;
                r.EndDate = r.EndDate.HasValue ? TimeZoneInfo.ConvertTimeFromUtc(r.EndDate.Value, timeZone) : r.EndDate;
            });

            return result;
        }
    }
}
