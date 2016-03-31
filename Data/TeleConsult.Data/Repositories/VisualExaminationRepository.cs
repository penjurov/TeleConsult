namespace TeleConsult.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Common.Helpers;
    using Filters.Consultations;
    using Helpers;
    using Microsoft.Practices.Unity;
    using Proxies;
    using TeleConsult.Data.Models;
    
    public class VisualExaminationRepository : BaseRepository<VisualExamination>
    {
        [InjectionConstructor]
        public VisualExaminationRepository(ITeleConsultDbContext context)
            : base(context)
        {
        }

        public IEnumerable<VisualExaminationProxy> Get(ConsultationFilter filter)
        {
            var result = this.All()
                .Where(be => !be.IsDeleted)
                .Where(filter.ConsultationId, c => c.ConsultationId == filter.ConsultationId)
                .OrderBy(c => c.Id);

            filter.Count = result.Count();

            return this.GetProxy(result.OrderByFilter(filter).PageByFilter(filter));
        }

        private IEnumerable<VisualExaminationProxy> GetProxy(List<VisualExamination> result)
        {
            return result.Select(ve => new VisualExaminationProxy
            {
                Id = ve.Id,
                Date = ve.Date,
                FileContent = string.Format("data:image/{0};base64,{1}", ve.FileType, Convert.ToBase64String(ve.FileContent)),
                FileType = ve.FileType,
                InputInformation = ve.InputInformation,
                Type = ve.Type
            });
        }
    }
}
