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

    public class UrinalysisRepository : BaseRepository<Urinalysis>
    {
        [InjectionConstructor]
        public UrinalysisRepository(ITeleConsultDbContext context)
            : base(context)
        {
        }

        public IEnumerable<UrinalysisProxy> Get(ConsultationFilter filter)
        {
            var result = this.All()
                .Where(be => !be.IsDeleted)
                .Where(filter.ConsultationId, c => c.ConsultationId == filter.ConsultationId)
                .OrderBy(c => c.Id);

            filter.Count = result.Count();

            return this.GetProxy(result.OrderByFilter(filter).PageByFilter(filter));
        }

        private IEnumerable<UrinalysisProxy> GetProxy(List<Urinalysis> result)
        {
            return result.Select(ur => new UrinalysisProxy
            {
                Id = ur.Id,
                Amylase = ur.Amylase,
                Bilirubin = ur.Bilirubin,
                Blood = ur.Blood,
                Date = ur.Date,
                Diuresis = ur.Diuresis,
                FormedElements = ur.FormedElements,
                Glucose = ur.Glucose,
                GlucoseWeight = ur.GlucoseWeight,
                KetoneBodies = ur.KetoneBodies,
                Ketosteroids = ur.Ketosteroids,
                Ph = ur.Ph,
                Porphobilinogen = ur.Porphobilinogen,
                Protein = ur.Protein,
                ProteinWeight = ur.ProteinWeight,
                Sediments = ur.Sediments,
                SpecificGravity = ur.SpecificGravity,
                Urobilinogen = ur.Urobilinogen
            });
        }
    }
}
