﻿namespace TeleConsult.Web.Areas.Consultations.Models
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
                this.ViewModel = new ConsultationProxy();

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

        public void BuildModel(int? id)
        {
            if (id.HasValue)
            {
                this.ViewModel = this.RepoFactory.Get<ConsultationRepository>().GetProxyById(id.Value);
                var currentUserId = this.RepoFactory.Get<UserRepository>().GetUserId(HttpContext.Current.User.Identity.Name);
                this.IsConsultation = this.ViewModel.ConsultantId == currentUserId;
            }
            else
            {
                this.IsConsultation = false;
            }
        }

        public List<ConsultationProxy> GetConsultations(ConsultationFilter filter)
        {
            filter.SpecialistId = this.RepoFactory.Get<UserRepository>().GetUserId(HttpContext.Current.User.Identity.Name);
            return this.RepoFactory.Get<ConsultationRepository>().Get(filter).ToList();
        }

        public List<BloodExaminationProxy> GetBloodExaminations(ConsultationFilter filter)
        {
            return this.RepoFactory.Get<BloodExaminationRepository>().Get(filter).ToList();
        }

        public List<UrinalysisProxy> GetUrinalysis(ConsultationFilter filter)
        {
            return this.RepoFactory.Get<UrinalysisRepository>().Get(filter).ToList();
        }

        public List<VisualExaminationProxy> GetVisualExaminations(ConsultationFilter filter)
        {
            return this.RepoFactory.Get<VisualExaminationRepository>().Get(filter).ToList();
        }

        public string GetDiagnosis(string code)
        {
            return this.RepoFactory.Get<DiagnosisRepository>().GetDiagnosisByCode(code);
        }

        public int Save(ConsultationProxy proxy, ModelStateDictionary modelState)
        {
            if (proxy != null && (modelState.IsValid || proxy.IsConsultation))
            {
                try
                {
                    var repo = this.RepoFactory.Get<ConsultationRepository>();
                    var currentSpecialistId = this.RepoFactory.Get<UserRepository>().GetUserId(HttpContext.Current.User.Identity.Name);
                    Consultation consultation;

                    using (var transaction = new TransactionScope())
                    {
                        if (!proxy.Id.HasValue || proxy.Id == 0)
                        {
                            var consultantId = this.GetConsultantId(proxy.SpecialityId);

                            consultation = new Consultation
                            {
                                SenderId = currentSpecialistId,
                                ConsultantId = consultantId,
                                PatientInitials = proxy.PatientInitials,
                                PatientAge = proxy.PatientAge.Value,
                                Gender = proxy.PatientGender,
                                PreliminaryDiagnosisCode = proxy.PreliminaryDiagnosisCode.ToUpper(),
                                Anamnesis = proxy.Anamnesis,
                                Type = proxy.ConsultationType,
                                Stage = ConsultationStage.Sent,
                                AddedDate = DateTime.Now,
                                ModifiedDate = DateTime.Now
                            };

                            repo.Add(consultation);

                            this.Logger.Log(ActionType.SendConsultation, string.Format("Id: {0}, Sender:{1}, Consultant: {2}, Date: {3}", consultation.Id, consultation.SenderId, consultation.ConsultantId, DateTime.Now));
                        }
                        else
                        {
                            consultation = repo.GetById(proxy.Id.Value);
                            
                            if (consultation.SenderId == currentSpecialistId)
                            {
                                consultation.PatientInitials = proxy.PatientInitials;
                                consultation.PatientAge = proxy.PatientAge.Value;
                                consultation.Gender = proxy.PatientGender;
                                consultation.PreliminaryDiagnosisCode = proxy.PreliminaryDiagnosisCode.ToUpper();
                                consultation.Anamnesis = proxy.Anamnesis;
                                consultation.Type = proxy.ConsultationType;
                                consultation.Stage = ConsultationStage.Sent;
                                consultation.ModifiedDate = DateTime.Now;

                                this.Logger.Log(ActionType.EditConsultation, string.Format("Id: {0}, Sender:{1}, Consultant: {2}, Date: {3}", consultation.Id, consultation.SenderId, consultation.ConsultantId, DateTime.Now));
                            }
                            else
                            {
                                if (proxy.FinalDiagnosisCode != null)
                                {
                                    consultation.Stage = ConsultationStage.Finnished;
                                    consultation.FinalDiagnosisCode = proxy.FinalDiagnosisCode.ToUpper();
                                    this.Logger.Log(ActionType.FinnishConsultation, string.Format("Id: {0}, Sender:{1}, Consultant: {2}, Date: {3}", consultation.Id, consultation.SenderId, consultation.ConsultantId, DateTime.Now));
                                }
                                else
                                {
                                    consultation.Stage = ConsultationStage.Answered;
                                    this.Logger.Log(ActionType.AnswerConsultation, string.Format("Id: {0}, Sender:{1}, Consultant: {2}, Date: {3}", consultation.Id, consultation.SenderId, consultation.ConsultantId, DateTime.Now));
                                }

                                consultation.Conclusion = proxy.Conclusion;
                                consultation.ModifiedDate = DateTime.Now;
                            }
                        }

                        this.SaveBloodExaminations(proxy.BloodExaminations ?? new List<BloodExaminationProxy>(), consultation);
                        this.SaveUrinalysis(proxy.Urinalysis ?? new List<UrinalysisProxy>(), consultation);
                        this.SaveVisualExaminations(proxy.VisualExaminations ?? new List<VisualExaminationProxy>(), consultation);

                        repo.SaveChanges();

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

        private void SaveBloodExaminations(IEnumerable<BloodExaminationProxy> bloodExaminations, Consultation consultation)
        {
            var bloodExaminationToDeactivate = consultation.BloodExaminations.Where(ex => !bloodExaminations.Any(be => be.Id == ex.Id));

            if (bloodExaminationToDeactivate.Any())
            {
                this.RepoFactory.Get<BloodExaminationRepository>().Deactivate(bloodExaminationToDeactivate);
                this.Logger.Log(ActionType.DeactivateBloodExamination, string.Format("ConsultationId: {0}, Deactivated: {1}", consultation.Id, string.Join(";", bloodExaminationToDeactivate.Select(ve => ve.Id).ToList())));
            }

            foreach (var item in bloodExaminations)
            {
                BloodExamination bloodExamination;

                if (!item.Id.HasValue)
                {
                    bloodExamination = new BloodExamination();

                    consultation.BloodExaminations.Add(bloodExamination);
                    this.Logger.Log(ActionType.AddBloodExamination, string.Format("ConsultationId: {0}, Date: {1}", consultation.Id, bloodExamination.Date));
                }
                else
                {
                    bloodExamination = consultation.BloodExaminations.FirstOrDefault(be => be.Id == item.Id);
                    this.Logger.Log(ActionType.EditBloodExamination, string.Format("ConsultationId: {0}, Date: {1}", consultation.Id, bloodExamination.Date));
                }

                bloodExamination.BleedingTime = item.BleedingTime;
                bloodExamination.BloodSugar = item.BloodSugar;
                bloodExamination.CoagulationTime = item.CoagulationTime;
                bloodExamination.Date = item.Date.Value;
                bloodExamination.Erythrocytes = item.Erythrocytes;
                bloodExamination.Hct = item.Hct;
                bloodExamination.Hemoglobin = item.Hemoglobin;
                bloodExamination.Leuc = item.Leuc;
                bloodExamination.Mch = item.Mch;
                bloodExamination.Mchc = item.Mchc;
                bloodExamination.Mcv = item.Mcv;
                bloodExamination.MorphologyErythrocytes = item.MorphologyErythrocytes;
                bloodExamination.Ret = item.Ret;
                bloodExamination.Sue = item.Sue;
            }
        }

        private void SaveUrinalysis(IEnumerable<UrinalysisProxy> urinalyses, Consultation consultation)
        {
            var urinalysesToDeactivate = consultation.Urinalysis.Where(ex => !urinalyses.Any(u => u.Id == ex.Id));

            if (urinalysesToDeactivate.Any())
            {
                this.RepoFactory.Get<UrinalysisRepository>().Deactivate(urinalysesToDeactivate);
                this.Logger.Log(ActionType.DeactivateUrinalysis, string.Format("ConsultationId: {0}, Deactivated: {1}", consultation.Id, string.Join(";", urinalysesToDeactivate.Select(ve => ve.Id).ToList())));
            }

            foreach (var item in urinalyses)
            {
                Urinalysis urinalysis;
                if (!item.Id.HasValue)
                {
                    urinalysis = new Urinalysis();

                    consultation.Urinalysis.Add(urinalysis);
                    this.Logger.Log(ActionType.AddUrinalysis, string.Format("ConsultationId: {0}, Date: {1}", consultation.Id, urinalysis.Date));
                }
                else
                {
                    urinalysis = consultation.Urinalysis.FirstOrDefault(u => u.Id == item.Id);
                    this.Logger.Log(ActionType.EditUrinalysis, string.Format("ConsultationId: {0}, Date: {1}", consultation.Id, urinalysis.Date));
                }

                urinalysis.Amylase = item.Amylase;
                urinalysis.Bilirubin = item.Bilirubin;
                urinalysis.Blood = item.Blood;
                urinalysis.Date = item.Date.Value;
                urinalysis.Diuresis = item.Diuresis;
                urinalysis.FormedElements = item.FormedElements;
                urinalysis.Glucose = item.Glucose;
                urinalysis.GlucoseWeight = item.GlucoseWeight;
                urinalysis.KetoneBodies = item.KetoneBodies;
                urinalysis.Ketosteroids = item.Ketosteroids;
                urinalysis.Ph = item.Ph;
                urinalysis.Porphobilinogen = item.Porphobilinogen;
                urinalysis.Protein = item.Protein;
                urinalysis.ProteinWeight = item.ProteinWeight;
                urinalysis.Sediments = item.Sediments;
                urinalysis.SpecificGravity = item.SpecificGravity;
                urinalysis.Urobilinogen = item.Urobilinogen;
            }
        }

        private void SaveVisualExaminations(IEnumerable<VisualExaminationProxy> visualExaminations, Consultation consultation)
        {
            var visualExaminationToDeactivate = consultation.VisualExaminations.Where(ex => !visualExaminations.Any(ve => ve.Id == ex.Id));

            if (visualExaminationToDeactivate.Any())
            {
                this.RepoFactory.Get<VisualExaminationRepository>().Deactivate(visualExaminationToDeactivate);
                this.Logger.Log(ActionType.DeactivateVisualExamination, string.Format("ConsultationId: {0}, Deactivated: {1}", consultation.Id, string.Join(";", visualExaminationToDeactivate.Select(ve => ve.Id).ToList())));
            }

            foreach (var item in visualExaminations)
            {
                VisualExamination visualExamination;

                if (!item.Id.HasValue)
                {
                    visualExamination = new VisualExamination();

                    consultation.VisualExaminations.Add(visualExamination);
                    this.Logger.Log(ActionType.AddVisualExamination, string.Format("ConsultationId: {0}, Date: {1}", consultation.Id, visualExamination.Date));
                }
                else
                {
                    visualExamination = consultation.VisualExaminations.FirstOrDefault(u => u.Id == item.Id);
                    this.Logger.Log(ActionType.EditVisualExamination, string.Format("ConsultationId: {0}, Date: {1}", consultation.Id, visualExamination.Date));
                }

                var base64 = item.FileContent.Substring(item.FileContent.IndexOf(',') + 1);

                visualExamination.Date = item.Date.Value;
                visualExamination.FileContent = Convert.FromBase64String(base64);
                visualExamination.FileType = item.FileType;
                visualExamination.InputInformation = item.InputInformation;
                visualExamination.Type = item.Type;
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