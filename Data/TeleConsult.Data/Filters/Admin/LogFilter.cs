namespace TeleConsult.Data.Filters.Admin
{
    using System;

    public class LogFilter : AdminFilter
    {
        public string Details { get; set; }

        public int? ActionType { get; set; }

        public string UserId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
