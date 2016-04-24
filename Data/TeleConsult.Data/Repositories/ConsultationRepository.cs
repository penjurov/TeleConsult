namespace TeleConsult.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Filters.Consultations;
    using Helpers;
    using Microsoft.Practices.Unity;
    using Models.Enumerations;
    using Proxies;
    using TeleConsult.Common.Helpers;
    using TeleConsult.Data.Models;

    public class ConsultationRepository : BaseRepository<Consultation>
    {
        [InjectionConstructor]
        public ConsultationRepository(ITeleConsultDbContext context)
            : base(context)
        {
        }

        public IEnumerable<ConsultationProxy> Get(ConsultationFilter filter)
        {
            var result = this.GetFiltered(filter)
                .Where(filter.IsConsultation, c => c.ConsultantId == filter.SpecialistId)
                .Where(!filter.IsConsultation, c => c.SenderId == filter.SpecialistId)
                .OrderBy(c => c.ModifiedDate);

            if (filter.SortBy == "PreliminaryDiagnosisDescription")
            {
                filter.SortBy = "PreliminaryDiagnosisCode";
            }

            filter.Count = result.Count();

            return this.GetProxy(result.OrderByFilter(filter).PageByFilter(filter));
        }

        public IEnumerable<ConsultationProxy> GetEmergencyConsultations(ConsultationFilter filter)
        {
            var result = this.GetFiltered(filter)
                .Where(c => c.Type == ConsultationType.Emergency && c.Consultant == null)
                .OrderBy(c => c.ModifiedDate);

            if (filter.SortBy == "PreliminaryDiagnosisDescription")
            {
                filter.SortBy = "PreliminaryDiagnosisCode";
            }

            filter.Count = result.Count();

            return this.GetProxy(result.OrderByFilter(filter).PageByFilter(filter));
        }

        public ConsultationProxy GetProxyById(int id)
        {
            var result = this.All().Where(c => c.Id == id).ToList();

            return this.GetProxy(result).FirstOrDefault();
        }

        public IEnumerable<ConsultationProxy> GetByConsultantIds(List<string> consultantIds)
        {
            var result = this.All().Where(c => consultantIds.Contains(c.ConsultantId));

            return this.GetProxy(result.ToList());
        }

        private IQueryable<Consultation> GetFiltered(ConsultationFilter filter)
        {
            var result = this.All()
                .Where(filter.HospitalName, c => c.Consultant != null && c.Consultant.Hospital.Name.Contains(filter.HospitalName))
                .Where(filter.TypeId, c => (int)c.Type == filter.TypeId)
                .Where(filter.GenderId, c => (int)c.Gender == filter.GenderId)
                .Where(filter.SpecialityId, c => c.SpecialityId == filter.SpecialityId);

            return result;
        }

        private IEnumerable<ConsultationProxy> GetProxy(List<Consultation> result)
        {
            return result.Select(c => new ConsultationProxy
            {
                Id = c.Id,
                PatientInitials = c.PatientInitials,
                PatientAge = c.PatientAge,
                PatientGender = c.Gender,
                SenderId = c.SenderId,
                ConsultantId = c.ConsultantId,
                Anamnesis = c.Anamnesis,
                Conclusion = c.Conclusion,
                ConsultationDate = c.AddedDate,
                SpecialityId = c.SpecialityId,
                PreliminaryDiagnosisCode = c.PreliminaryDiagnosisCode,
                PreliminaryDiagnosisDescription = c.PreliminaryDiagnosis != null ? c.PreliminaryDiagnosis.Description : null,
                FinalDiagnosisCode = c.FinalDiagnosisCode,
                FinalDiagnosisDescription = c.FinalDiagnosis != null ? c.FinalDiagnosis.Description : null,
                Stage = c.Stage,
                ConsultationType = c.Type,
                Rating = c.Rating
            });
        }
    }
}
