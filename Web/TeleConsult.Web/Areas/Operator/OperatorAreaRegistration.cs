namespace TeleConsult.Web.Areas.Operator
{
    using System.Web.Mvc;
    using Common;

    public class OperatorAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return GlobalConstants.OperatorAreaName;
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Operator_default",
                "Operator/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional });
        }
    }
}