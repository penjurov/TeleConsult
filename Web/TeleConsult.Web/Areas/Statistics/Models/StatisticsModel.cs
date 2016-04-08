namespace TeleConsult.Web.Areas.Statistics.Models
{
    using System.Collections.Generic;
    using System.Linq;

    using Data.Filters.Admin;
    using Data.Proxies;
    using Data.Repositories;
    using Web.Models;
    using Web.Models.Base;

    public class StatisticsModel : BaseModel, IModel
    {
        public List<SpecialistProxy> GetBestDoctors()
        {
            var filter = new SpecialistFilter
            {
                Limit = 10
            };

            var result = this.RepoFactory.Get<SpecialistRepository>().Get(filter)
                    .OrderByDescending(s => s.Rating)
                    .ToList();

            return result;
        }

        public List<HospitalProxy> GetBestHospitals()
        {
            var filter = new AdminFilter
            {
                Limit = 10
            };

            return this.RepoFactory.Get<HospitalRepository>().Get(filter)
                .OrderByDescending(h => h.Rating)
                .ToList();
        }
    }
}