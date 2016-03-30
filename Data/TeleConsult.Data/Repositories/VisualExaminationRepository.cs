namespace TeleConsult.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

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
                FileContent = Encoding.UTF8.GetString(ve.FileContent, 0, ve.FileContent.Length),
                FileType = ve.FileType,
                InputInformation = ve.InputInformation,
                Type = ve.Type
            });
        }
    }
}
