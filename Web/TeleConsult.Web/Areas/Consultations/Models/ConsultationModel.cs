namespace TeleConsult.Web.Areas.Consultations.Models
{
    using Data.Proxies;
    using Web.Models.Base;
    using Web.Models;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System;
    using Data.Models.Enumerations;
    using System.Linq;
    using Common.Helpers;
    using Data.Repositories;
    using Data.Models;
    using System.Web;
    using System.Data.Entity.Validation;
    using Data.Filters.Admin;
    public class ConsultationModel : BaseModel, IModel<bool>
    {
        public ConsultationProxy ViewModel { get; set; }

        public IEnumerable<SelectListItem> Genders { get; set; }

        public IEnumerable<SelectListItem> Types { get; set; }

        public IEnumerable<SelectListItem> Specialities { get; set; }

        public bool IsConsultation { get; set; }

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

                this.Types = Enum.GetValues(typeof(ConsultationType)).Cast<ConsultationType>()
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

        public string GetDiagnosis(string code)
        {
            return this.RepoFactory.Get<DiagnosisRepository>().GetDiagnosisByCode(code);
        }

        public int Send(ConsultationProxy proxy, ModelStateDictionary modelState)
        {
            if (proxy != null && modelState.IsValid)
            {
                try
                {
                    var senderId = this.RepoFactory.Get<UserRepository>().GetUserId(HttpContext.Current.User.Identity.Name);
                    var consultantId = this.GetConsultantId(proxy.SpecialityId);

                    var consultation = new Consultation
                    {
                        SenderId = senderId,
                        ConsultantId = consultantId,
                        PatientInitials = proxy.PatientInitials,
                        PatientAge = proxy.PatientAge,
                        Gender = proxy.Gender,
                        PreliminaryDiagnosisCode = proxy.PreliminaryDiagnosisCode,
                        Anamnesis = proxy.Anamnesis,
                        Type = proxy.Type,
                        Stage = ConsultationStage.Sent,
                        Date = DateTime.Now
                    };

                    var repo = this.RepoFactory.Get<ConsultationRepository>();
                    repo.Add(consultation);
                    repo.SaveChanges();

                    this.Logger.Log(ActionType.SendConsultation, consultation.SenderId.ToString());

                    return consultation.Id;
                }
                catch (DbEntityValidationException e)
                {
                    throw new Exception(this.HandleDbEntityValidationException(e));
                }
            }
            else
            {
                throw new Exception(this.HandleErrors(modelState));
            }
        }

        private string GetConsultantId(int specialityId)
        {
            var filter = new SchedulesFilter
            {
                Date = DateTime.Now,
                SpecialityId = specialityId
            };

            var specialistsOnScheduleIds = this.RepoFactory.Get<ScheduleRepository>().Get(filter)
                .Where(s => s.Specialist.UserName != HttpContext.Current.User.Identity.Name)
                .Select(s => s.SpecialistId)
                .ToList();

            string lessAvailableSpecialistId = string.Empty;

            if (specialistsOnScheduleIds.Any())
            {
                var consultationRepo = this.RepoFactory.Get<ConsultationRepository>();

                var openConsultations = consultationRepo.GetByConsultantIds(specialistsOnScheduleIds).ToList()
                    .Where(c => c.Stage != ConsultationStage.Finnished);

                if (openConsultations.Any())
                {
                    var mostBusySpecialistId = openConsultations
                        .GroupBy(c => c.ConsultantId)
                        .OrderByDescending(g => g.Count())
                        .FirstOrDefault()
                        .Key;

                    if (specialistsOnScheduleIds.Count > 1)
                    {
                        specialistsOnScheduleIds.Remove(mostBusySpecialistId);
                    }

                    lessAvailableSpecialistId = specialistsOnScheduleIds.FirstOrDefault();
                }
                else
                {
                    lessAvailableSpecialistId = specialistsOnScheduleIds.FirstOrDefault();
                }
            }
            else
            {
                throw new Exception("Няма специалист на смяна по избраната специалност");
            }



            return lessAvailableSpecialistId;
        }
    }
}