namespace TeleConsult.Web.Areas.Consultations.Controllers
{
    using System.Web.Mvc;
    using Data.Proxies;
    using Infrastructure.Attributes;
    using Models;

    public class ConsultationController : ConsultationBaseController
    {
        public ActionResult RequestConsultation()
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
    }
}