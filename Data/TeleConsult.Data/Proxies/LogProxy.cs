namespace TeleConsult.Data.Proxies
{
    using System;
    using TeleConsult.Common.Helpers;
    using TeleConsult.Data.Models.Enumerations;

    public class LogProxy
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Date { get; set; }

        public ActionType Action { get; set; }

        public string ActionName
        {
            get
            {
                return this.Action.GetDescription();
            }
        }

        public string Details { get; set; }
    }
}
