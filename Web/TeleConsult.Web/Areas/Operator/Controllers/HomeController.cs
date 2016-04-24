namespace TeleConsult.Web.Areas.Operator.Controllers
{
    using System.Web.Mvc;

    using Common;
    using Data.Filters.Consultations;
    using Models;
    using Web.Controllers.Base;

    [Authorize(Roles = GlobalConstants.OperatorRoleName)]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var model = LoadModel<EmergencyConsultationModel, bool>(true);
            return this.View(model);
        }

        [HttpGet]
        public JsonResult GetEmergencyConsultations(ConsultationFilter filter)
        {
            var model = LoadModel<EmergencyConsultationModel, bool>(false);
            var result = model.GetEmergencyConsultations(filter);
            return this.Json(new { records = result, total = filter.Count }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSpecialistOnSchedule(int specialityId)
        {
            var model = LoadModel<EmergencyConsultationModel, bool>(false);
            var result = model.GetSpecialistOnSchedule(specialityId);
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SetConsultant(int consultationId, string consultantId)
        {
            var model = LoadModel<EmergencyConsultationModel, bool>(false);
            var result = model.SetConsultant(consultationId, consultantId);
            return this.Json(result);
        }
    }
}