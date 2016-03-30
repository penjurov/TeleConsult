namespace TeleConsult.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Common.Helpers;
    using Filters.Consultations;
    using Helpers;
    using Microsoft.Practices.Unity;
    using Proxies;
    using TeleConsult.Data.Models;

    public class BloodExaminationRepository : BaseRepository<BloodExamination>
    {
        [InjectionConstructor]
        public BloodExaminationRepository(ITeleConsultDbContext context)
            : base(context)
        {
        }

        public IEnumerable<BloodExaminationProxy> Get(ConsultationFilter filter)
        {
            var result = this.All()
                .Where(be => !be.IsDeleted)
                .Where(filter.ConsultationId, c => c.ConsultationId == filter.ConsultationId)
                .OrderBy(c => c.Id);

            filter.Count = result.Count();

            return this.GetProxy(result.OrderByFilter(filter).PageByFilter(filter));
        }

        private IEnumerable<BloodExaminationProxy> GetProxy(List<BloodExamination> result)
        {
            return result.Select(be => new BloodExaminationProxy
            {
                Id = be.Id,
                BleedingTime = be.BleedingTime,
                BloodSugar = be.BloodSugar,
                CoagulationTime = be.CoagulationTime,
                Date = be.Date,
                Erythrocytes = be.Erythrocytes,
                Hct = be.Hct,
                Hemoglobin = be.Hemoglobin,
                Leuc = be.Leuc,
                Mch = be.Mch,
                Mchc = be.Mchc,
                Mcv = be.Mcv,
                MorphologyErythrocytes = be.MorphologyErythrocytes,
                Ret = be.Ret,
                Sue = be.Sue
            });
        }
    }
}
