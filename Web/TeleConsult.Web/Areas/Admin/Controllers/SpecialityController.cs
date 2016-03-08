namespace TeleConsult.Web.Areas.Admin.Controllers
{
    using Data.Filters.Admin;
    using Data.Proxies;
    using Models;
    using System.Web.Mvc;

    public class SpecialityController : AdminBaseController
    {
        // GET: Admin/Specialty
        public ActionResult Index()
        {
            var model = this.LoadModel<SpecialityModel, bool>(true);
            return View(model);
        }

        [HttpGet]
        public JsonResult GetSpecialities(AdminFilter filter)
        {
            var model = LoadModel<SpecialityModel, bool>(false);
            var result = model.GetSpecialities(filter);
            return this.Json(new { records = result, total = filter.Count }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(SpecialityProxy proxy)
        {
            var model = LoadModel<SpecialityModel, bool>(false);
            var result = model.Save(proxy, this.ModelState);
            return this.Json(result);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var model = LoadModel<SpecialityModel, bool>(false);
            model.Delete(id);
            return this.Json(true);
        }

        [HttpPost]
        public JsonResult Activate(int id)
        {
            var model = LoadModel<SpecialityModel, bool>(false);
            model.Activate(id);
            return this.Json(true);
        }
    }
}