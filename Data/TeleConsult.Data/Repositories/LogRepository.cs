namespace TeleConsult.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Common.Helpers;
    using Filters.Admin;
    using Helpers;
    using Microsoft.Practices.Unity;
    using Models.Enumerations;
    using Proxies;
    using TeleConsult.Data.Models;

    public class LogRepository : BaseRepository<Log>
    {
        [InjectionConstructor]
        public LogRepository(ITeleConsultDbContext context)
            : base(context)
        {
        }

        public IEnumerable<LogProxy> Get(LogFilter filter)
        {
            var result = this.All()
                .Where(filter.ActionType, l => l.Action == (ActionType)filter.ActionType.Value)
                .Where(filter.UserId, l => l.UserId == filter.UserId)
                .Where(filter.Details, l => l.Details.Contains(filter.Details))
                .Where(filter.StartDate.HasValue, l => l.Date >= filter.StartDate.Value)
                .Where(filter.EndDate.HasValue, l => l.Date <= filter.EndDate.Value)
                .OrderByDescending(s => s.Id);

            filter.Count = result.Count();

            if (filter.SortBy == "UserName")
            {
                filter.SortBy = "UserId";
            }

            if (filter.SortBy == "ActionName")
            {
                filter.SortBy = "Action";
            }

            return this.GetProxy(result.OrderByFilter(filter).PageByFilter(filter));
        }

        private IEnumerable<LogProxy> GetProxy(List<Log> result)
        {
            return result.Select(l => new LogProxy
            {
                Id = l.Id,
                Details = l.Details,
                Action = l.Action,
                Date = l.Date.ToString(),
                UserName = l.User.UserName
            });
        }
    }
}
