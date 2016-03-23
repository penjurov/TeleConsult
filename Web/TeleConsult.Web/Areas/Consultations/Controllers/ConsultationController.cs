namespace TeleConsult.Web.Areas.Consultations.Controllers
{
    using System.Web.Mvc;

    using Data.Filters.Consultations;
    using Data.Proxies;
    using Infrastructure.Attributes;
    using Models;

    public class ConsultationController : ConsultationBaseController
    {
        public ActionResult Request()
        {
            var model = this.LoadModel<ConsultationModel, bool>(true);
            model.IsConsultation = false;
            return this.View(model);
        }

        public ActionResult Received()
        {
            var model = this.LoadModel<ConsultationModel, bool>(true);
            model.IsConsultation = true;
            return this.View(model);
        }

        public ActionResult Sent()
        {
            var model = this.LoadModel<ConsultationModel, bool>(true);
            model.IsConsultation = false;
            return this.View(model);
        }

        [HttpPost]
        [ValidateHeaderAntiForgeryToken]
        public JsonResult Send(ConsultationProxy proxy)
        {
            var model = LoadModel<ConsultationModel, bool>(false);
            var result = model.Send(proxy, this.ModelState);
            return this.Json(result);
        }

        [HttpGet]
        public JsonResult GetDiagnosis(string code)
        {
            var model = LoadModel<ConsultationModel, bool>(false);
            var diagnosis = model.GetDiagnosis(code);
            return this.Json(diagnosis, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetConsultations(ConsultationFilter filter)
        {
            var model = LoadModel<ConsultationModel, bool>(false);
            var result = model.GetConsultations(filter);
            return this.Json(new { records = result, total = filter.Count }, JsonRequestBehavior.AllowGet);
        }
    }
}