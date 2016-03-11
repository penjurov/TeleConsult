namespace TeleConsult.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    public class MenuController : AdminBaseController
    {
        // GET: Admin/Menu
        public ActionResult Index()
        {
            return this.View();
        }
    }
}