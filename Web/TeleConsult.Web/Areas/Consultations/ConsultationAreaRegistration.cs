namespace TeleConsult.Web.Areas.Consultations
{
    using System.Web.Mvc;
    using Common;

    public class ConsultationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return GlobalConstants.ConsultationAreaName;
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Consultation_default",
                "Consultations/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional });
        }
    }
}