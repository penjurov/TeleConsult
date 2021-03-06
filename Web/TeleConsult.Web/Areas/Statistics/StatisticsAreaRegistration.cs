﻿namespace TeleConsult.Web.Areas.Statistics
{
    using System.Web.Mvc;
    using Common;

    public class StatisticsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return GlobalConstants.StatisticsAreaName;
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Statistics_default",
                "Statistics/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional });
        }
    }
}