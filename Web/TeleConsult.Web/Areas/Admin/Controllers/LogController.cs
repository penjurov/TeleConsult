namespace TeleConsult.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    using Data.Filters.Admin;
    using Models;

    public class LogController : AdminBaseController
    {
        public ActionResult Index()
        {
            var model = this.LoadModel<LogModel, bool>(true);
            return this.View(model);
        }

        [HttpGet]
        public JsonResult GetLogs(LogFilter filter)
        {
            var model = LoadModel<LogModel, bool>(false);
            var result = model.GetLogs(filter);
            return this.Json(new { records = result, total = filter.Count }, JsonRequestBehavior.AllowGet);
        }
    }
}