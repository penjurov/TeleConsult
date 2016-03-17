namespace TeleConsult.Data.Repositories
{
    using System.Linq;
    using Microsoft.Practices.Unity;
    using TeleConsult.Data.Models;

    public class DiagnosisRepository : BaseRepository<Diagnosis>
    {
        [InjectionConstructor]
        public DiagnosisRepository(ITeleConsultDbContext context)
            : base(context)
        {
        }

        public string GetDiagnosisByCode(string code)
        {
            return this.All().FirstOrDefault(d => d.Code == code).Description;
        }
    }
}
