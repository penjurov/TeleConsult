namespace TeleConsult.Web.Areas.Statistics.Controllers
{
    using System.Web.Mvc;

    using Models;

    public class HomeController : StatisticsBaseController
    {
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public JsonResult GetBestHospitals()
        {
            var model = LoadModel<StatisticsModel>();
            var result = model.GetBestHospitals();
            return this.Json(new { records = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetBestDoctors()
        {
            var model = LoadModel<StatisticsModel>();
            var result = model.GetBestDoctors();
            return this.Json(new { records = result }, JsonRequestBehavior.AllowGet);
        }
    }
}