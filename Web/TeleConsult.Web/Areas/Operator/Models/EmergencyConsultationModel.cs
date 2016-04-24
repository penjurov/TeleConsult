namespace TeleConsult.Web.Areas.Operator.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Common.Helpers;
    using Data.Filters.Admin;
    using Data.Filters.Consultations;
    using Data.Models.Enumerations;
    using Data.Proxies;
    using Data.Repositories;
    using Web.Models;
    using Web.Models.Base;
   
    public class EmergencyConsultationModel : BaseModel, IModel<bool>
    {
        public IEnumerable<SelectListItem> Genders { get; set; }

        public IEnumerable<SelectListItem> Specialities { get; set; }

        public void Init(bool init)
        {
            base.Init();

            if (init)
            {
                this.Genders = Enum.GetValues(typeof(Gender)).Cast<Gender>()
                    .Select(v => new SelectListItem
                    {
                        Text = v.GetDescription(),
                        Value = ((int)v).ToString()
                    });

                this.Specialities = this.RepoFactory.Get<SpecialityRepository>().GetActive()
                    .Select(s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.Name
                    });
            }
        }

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