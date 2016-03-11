namespace TeleConsult.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    using Data.Filters.Admin;
    using Data.Proxies;
    using Models;

    public class HospitalController : AdminBaseController
    {
        public ActionResult Index()
        {
            var model = this.LoadModel<HospitalModel, bool>(true);
            return this.View(model);
        }

        [HttpGet]
        public JsonResult GetHospitals(AdminFilter filter)
        {
            var model = LoadModel<HospitalModel, bool>(false);
            var result = model.GetHospitals(filter);
            return this.Json(new { records = result, total = filter.Count }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(HospitalProxy proxy)
        {
            var model = LoadModel<HospitalModel, bool>(false);
            var result = model.Save(proxy, this.ModelState);
            return this.Json(result);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var model = LoadModel<HospitalModel, bool>(false);
            model.Delete(id);
            return this.Json(true);
        }

        [HttpPost]
        public JsonResult Activate(int id)
        {
            var model = LoadModel<HospitalModel, bool>(false);
            model.Activate(id);
            return this.Json(true);
        }
    }
}