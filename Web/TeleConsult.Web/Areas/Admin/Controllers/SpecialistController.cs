namespace TeleConsult.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    using Data.Filters.Admin;
    using Data.Proxies;
    using Models;
    
    public class SpecialistController : AdminBaseController
    {
        public ActionResult Index()
        {
            var model = this.LoadModel<SpecialistModel, bool>(true);
            return this.View(model);
        }

        [HttpGet]
        public JsonResult GetSpecialists(SpecialistFilter filter)
        {
            var model = LoadModel<SpecialistModel, bool>(false);
            var result = model.GetSpecialists(filter);
            return this.Json(new { records = result, total = filter.Count }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(SpecialistProxy proxy)
        {
            var model = LoadModel<SpecialistModel, bool>(false);
            var result = model.Save(proxy, this.ModelState);
            return this.Json(result);
        }

        [HttpPost]
        public JsonResult Delete(string id)
        {
            var model = LoadModel<SpecialistModel, bool>(false);
            model.Delete(id);
            return this.Json(true);
        }

        [HttpPost]
        public JsonResult Activate(string id)
        {
            var model = LoadModel<SpecialistModel, bool>(false);
            model.Activate(id);
            return this.Json(true);
        }
    }
}