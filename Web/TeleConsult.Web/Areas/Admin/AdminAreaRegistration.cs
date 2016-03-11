namespace TeleConsult.Web.Areas.Admin
{
    using System.Web.Mvc;

    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
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