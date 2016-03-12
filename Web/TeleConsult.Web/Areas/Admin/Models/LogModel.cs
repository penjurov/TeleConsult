namespace TeleConsult.Web.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Common.Helpers;
    using Data.Filters.Admin;
    using Data.Models.Enumerations;
    using Data.Proxies;
    using Data.Repositories;
    using TeleConsult.Web.Areas.Admin.Models.Base;
    using Web.Models;

    public class LogModel : AdminModel, IModel<bool>
    {
        public IEnumerable<SelectListItem> Actions { get; set; }

        public IEnumerable<SelectListItem> Users { get; set; }

        public override void Init(bool init)
        {
            base.Init(init);

            if (init)
            {
                this.Actions = Enum.GetValues(typeof(ActionType)).Cast<ActionType>()
                    .Select(v => new SelectListItem
                    {
                        Text = v.GetDescription(),
                        Value = ((int)v).ToString()
                    });

                this.Users = this.RepoFactory.Get<UserRepository>().GetActive()
                    .Select(s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.Name
                    });
            }
        }

        public List<LogProxy> GetLogs(LogFilter filter)
        {
            return this.RepoFactory.Get<LogRepository>().Get(filter).ToList();
        }
    }
}