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
            var result = this.All().FirstOrDefault(d => d.Code == code);

            if (result != null)
            {
                return result.Description;
            }

            return string.Empty;  
        }
    }
}
