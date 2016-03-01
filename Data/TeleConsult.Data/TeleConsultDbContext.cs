namespace TeleConsult.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;
    using Migrations;
    using Models;
    
    public class TeleConsultDbContext : IdentityDbContext<User>, ITeleConsultDbContext
    {
        public TeleConsultDbContext() : base("TeleConsultDb", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TeleConsultDbContext, Configuration>());
        }

        public IDbSet<BloodExamination> BloodExaminations { get; set; }

        public IDbSet<Consultation> Consultations { get; set; }

        public IDbSet<Diagnosis> Diagnosis { get; set; }

        public IDbSet<Hospital> Hospitals { get; set; }

        public IDbSet<Schedule> Schedules { get; set; }

        public IDbSet<Specialist> Specialists { get; set; }

        public IDbSet<Speciality> Specialities { get; set; }

        public IDbSet<Urinalysis> Urinalysis { get; set; }

        public IDbSet<VisualExamination> VisualExaminations { get; set; }

        public IDbSet<Log> Logs { get; set; }

        public static TeleConsultDbContext Create()
        {
            return new TeleConsultDbContext();
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public new void Dispose()
        {
            base.Dispose();
        }
    }
}
