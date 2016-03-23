namespace TeleConsult.Web.Areas.Consultations.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Transactions;
    using System.Web;

    using System.Web.Mvc;
    using Common.Helpers;
    using Data.Filters.Admin;
    using Data.Filters.Consultations;
    using Data.Models;
    using Data.Models.Enumerations;
    using Data.Proxies;
    using Data.Repositories;
    using Web.Models;
    using Web.Models.Base;

    public class ConsultationModel : BaseModel, IModel<bool>
    {
        public ConsultationProxy ViewModel { get; set; }

        public BloodExaminationProxy BloodExaminationViewModel { get; set; }

        public UrinalysisProxy UrinalysisViewModel { get; set; }

        public VisualExaminationProxy VisualExaminationViewModel { get; set; }

        public IEnumerable<SelectListItem> Genders { get; set; }

        public IEnumerable<SelectListItem> Types { get; set; }

        public IEnumerable<SelectListItem> Specialities { get; set; }

        public IEnumerable<SelectListItem> VisualExaminationTypes { get; set; }

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

                this.VisualExaminationTypes = Enum.GetValues(typeof(VisualExaminationType)).Cast<VisualExaminationType>()
                    .Select(v => new SelectListItem
                    {
                        Text = v.GetDescription(),
                        Value = ((int)v).ToString()
                    });
            }
        }

        public List<ConsultationProxy> GetConsultations(ConsultationFilter filter)
        {
            filter.SpecialistId = this.RepoFactory.Get<UserRepository>().GetUserId(HttpContext.Current.User.Identity.Name);
            return this.RepoFactory.Get<ConsultationRepository>().Get(filter).ToList();
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
                    using (var transaction = new TransactionScope())
                    {
                        var senderId = this.RepoFactory.Get<UserRepository>().GetUserId(HttpContext.Current.User.Identity.Name);
                        var consultantId = this.GetConsultantId(proxy.SpecialityId);

                        var consultation = new Consultation
                        {
                            SenderId = senderId,
                            ConsultantId = consultantId,
                            PatientInitials = proxy.PatientInitials,
                            PatientAge = proxy.PatientAge,
                            Gender = proxy.PatientGender,
                            PreliminaryDiagnosisCode = proxy.PreliminaryDiagnosisCode.ToUpper(),
                            Anamnesis = proxy.Anamnesis,
                            Type = proxy.ConsultationType,
                            Stage = ConsultationStage.Sent,
                            Date = DateTime.Now
                        };

                        var repo = this.RepoFactory.Get<ConsultationRepository>();
                        repo.Add(consultation);

                        consultation.BloodExaminations = proxy.BloodExaminations != null ? this.SaveBloodExaminations(proxy.BloodExaminations, consultation.Id) : null;
                        consultation.Urinalysis = proxy.Urinalysis != null ? this.SaveUrinalysis(proxy.Urinalysis, consultation.Id) : null;
                        consultation.VisualExaminations = proxy.VisualExaminations != null ? this.SaveVisualExaminations(proxy.VisualExaminations, consultation.Id) : null;
                        repo.SaveChanges();

                        this.Logger.Log(ActionType.SendConsultation, string.Format("Id: {1}, Sender:{0}, Consultant: {2}", consultation.Id, consultation.SenderId, consultation.ConsultantId));

                        transaction.Complete();
                        return consultation.Id;
                    }
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

        private ICollection<BloodExamination> SaveBloodExaminations(IEnumerable<BloodExaminationProxy> bloodExaminations, int consultationId)
        {
            var result = new List<BloodExamination>();

            foreach (var item in bloodExaminations)
            {
                var bloodExamination = new BloodExamination
                {
                    BleedingTime = item.BleedingTime,
                    BloodSugar = item.BloodSugar,
                    CoagulationTime = item.CoagulationTime,
                    Date = item.Date.Value,
                    Erythrocytes = item.Erythrocytes,
                    Hct = item.Hct,
                    Hemoglobin = item.Hemoglobin,
                    Leuc = item.Leuc,
                    Mch = item.Mch,
                    Mchc = item.Mchc,
                    Mcv = item.Mcv,
                    MorphologyErythrocytes = item.MorphologyErythrocytes,
                    Ret = item.Ret,
                    Sue = item.Sue
                };

                result.Add(bloodExamination);

                this.Logger.Log(ActionType.AddBloodExamination, string.Format("ConsultationId: {0}, Date: {1}", consultationId, bloodExamination.Date));
            }

            return result;
        }

        private ICollection<Urinalysis> SaveUrinalysis(IEnumerable<UrinalysisProxy> urinalyses, int consultationId)
        {
            var result = new List<Urinalysis>();

            foreach (var item in urinalyses)
            {
                var urinalysis = new Urinalysis
                {
                    Amylase = item.Amylase,
                    Bilirubin = item.Bilirubin,
                    Blood = item.Blood,
                    Date = item.Date.Value,
                    Diuresis = item.Diuresis,
                    FormedElements = item.FormedElements,
                    Glucose = item.Glucose,
                    GlucoseWeight = item.GlucoseWeight,
                    KetoneBodies = item.KetoneBodies,
                    Ketosteroids = item.Ketosteroids,
                    Ph = item.Ph,
                    Porphobilinogen = item.Porphobilinogen,
                    Protein = item.Protein,
                    ProteinWeight = item.ProteinWeight,
                    Sediments = item.Sediments,
                    SpecificGravity = item.SpecificGravity,
                    Urobilinogen = item.Urobilinogen
                };

                result.Add(urinalysis);

                this.Logger.Log(ActionType.AddUrinalysis, string.Format("ConsultationId: {0}, Date: {1}", consultationId, urinalysis.Date));
            }

            return result;
        }

        private ICollection<VisualExamination> SaveVisualExaminations(IEnumerable<VisualExaminationProxy> visualExaminations, int consultationId)
        {
            var result = new List<VisualExamination>();

            foreach (var item in visualExaminations)
            {
                var visualExamination = new VisualExamination
                {
                    Date = item.Date.Value,
                    FileContent = this.GetBytes(item.FileContent),
                    FileType = item.FileType,
                    InputInformation = item.InputInformation,
                    Type = item.Type
                };

                result.Add(visualExamination);

                this.Logger.Log(ActionType.AddVisualExamination, string.Format("ConsultationId: {0}, Date: {1}", consultationId, visualExamination.Date));
            }

            return result;
        }

        private byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
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