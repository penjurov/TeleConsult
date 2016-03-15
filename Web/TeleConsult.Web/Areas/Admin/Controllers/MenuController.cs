namespace TeleConsult.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    public class MenuController : AdminBaseController
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}