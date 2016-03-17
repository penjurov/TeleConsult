namespace TeleConsult.Web.Areas.Consultations.Controllers
{
    using Data.Proxies;
    using Models;
    using System.Web.Mvc;

    public class ConsultationController : ConsultationBaseController
    {
        public ActionResult RequestConsultation()
        {
            var model = this.LoadModel<ConsultationModel, bool>(true);
            model.IsConsultation = false;
            return View(model);
        }

        [HttpPost]
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