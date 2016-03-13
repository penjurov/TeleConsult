namespace TeleConsult.Web.Areas.Admin.Controllers
{
    using Data.Filters.Admin;
    using Data.Proxies;
    using Models;
    using System.Web.Mvc;

    public class SchedulesController : AdminBaseController
    {
        public ActionResult Index()
        {
            var model = this.LoadModel<ScheduleModel, bool>(true);
            return View(model);
        }

        [HttpGet]
        public JsonResult GetSchedules(SchedulesFilter filter)
        {
            var model = LoadModel<ScheduleModel, bool>(false);
            var result = model.GetSchedules(filter);
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(ScheduleProxy proxy)
        {
            var model = LoadModel<ScheduleModel, bool>(false);
            var result = model.Save(proxy, this.ModelState);
            return this.Json(result);
        }
    }
}