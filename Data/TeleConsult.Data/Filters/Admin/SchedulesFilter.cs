using System;

namespace TeleConsult.Data.Filters.Admin
{
    public class SchedulesFilter : AdminFilter
    {

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string SpecialistId { get; set; }

        public string Description { get; set; }
    }
}
