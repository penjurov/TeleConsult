namespace TeleConsult.Web.Areas.Consultations.Controllers
{
    using System.Web.Mvc;

    public class MenuController : ConsultationBaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}