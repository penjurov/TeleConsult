namespace TeleConsult.Web.Areas.Operator.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Data.Filters.Admin;
    using Data.Filters.Consultations;
    using Data.Proxies;
    using Data.Repositories;
    using Web.Models;
    using Web.Models.Base;

    public class EmergencyConsultationModel : BaseModel, IModel
    {
        public List<ConsultationProxy> GetEmergencyConsultations(ConsultationFilter filter)
        {
            return this.RepoFactory.Get<ConsultationRepository>().GetEmergencyConsultations(filter).ToList();
        }

        public List<SelectListItem> GetSpecialistOnSchedule(int specialityId)
        {
            var filter = new SchedulesFilter
            {
                Date = DateTime.Now,
                SpecialityId = specialityId
            };

            var specialistsOnSchedule = this.RepoFactory.Get<ScheduleRepository>().Get(filter)
                .Select(s => new SelectListItem
                {
                    Value = s.SpecialistId,
                    Text = s.SpecialistName
                })
                .ToList();

            return specialistsOnSchedule;
        }

        public bool SetConsultant(int consultationId, string consultantId)
        {
            var repo = this.RepoFactory.Get<ConsultationRepository>();
            var consultation = repo.GetById(consultationId);

            consultation.ConsultantId = consultantId;

            return repo.SaveChanges() > 0;
        }
    }
}