namespace TeleConsult.Web.Controllers
{
    using System.Web.Mvc;

    using Base;

    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}