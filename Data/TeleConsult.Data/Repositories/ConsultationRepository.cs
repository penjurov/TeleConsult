namespace TeleConsult.Data.Repositories
{
    using Microsoft.Practices.Unity;
    using Proxies;
    using System.Collections.Generic;
    using System.Linq;
    using TeleConsult.Data.Models;
    using System;

    public class ConsultationRepository : BaseRepository<Consultation>
    {
        [InjectionConstructor]
        public ConsultationRepository(ITeleConsultDbContext context)
            : base(context)
        {
        }

        public IEnumerable<ConsultationProxy> GetByConsultantIds(List<string> consultantIds)
        {
            var result = this.All().Where(c => consultantIds.Contains(c.ConsultantId));

            return this.GetProxy(result);
        }

        private IEnumerable<ConsultationProxy> GetProxy(IQueryable<Consultation> result)
        {
            return result.Select(c => new ConsultationProxy
            {
                Id = c.Id,
                PatientInitials = c.PatientInitials,
                PatientAge = c.PatientAge,
                SenderId = c.SenderId,
                ConsultantId = c.ConsultantId,
                Anamnesis = c.Anamnesis,
                Conclusion = c.Conclusion,
                Date = c.Date,
                Gender = c.Gender,
                SpecialityId = c.Consultant.SpecialityId,
                PreliminaryDiagnosisCode = c.PreliminaryDiagnosisCode,
                PreliminaryDiagnosisDescription = c.PreliminaryDiagnosis.Description,
                FinalDiagnosisCode = c.FinalDiagnosisCode,
                FinalDiagnosisDescription = c.FinalDiagnosis.Description,
                Stage = c.Stage,
                Type = c.Type,
                BloodExaminationIds = c.BloodExaminations.Select(be => be.Id),
                UrinalysisIds = c.Urinalysis.Select(ur => ur.Id),
                VisualExaminationIds = c.VisualExaminations.Select(ve => ve.Id)
            });
        }
    }
}
