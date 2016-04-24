namespace TeleConsult.Web.Areas.Admin
{
    using System.Web.Mvc;
    using Common;

    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return GlobalConstants.AdminAreaName;
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Menu", action = "Index", id = UrlParameter.Optional });
        }
    }
}