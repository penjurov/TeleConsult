using System;

namespace TeleConsult.Data.Filters.Admin
{
    public class SchedulesFilter : AdminFilter
    {

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? Date { get; set; }

        public string SpecialistId { get; set; }

        public int? SpecialityId { get; set; }

        public string Description { get; set; }
    }
}
