namespace TeleConsult.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Hosting;

    using Common;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    
    internal sealed class Configuration : DbMigrationsConfiguration<TeleConsultDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TeleConsultDbContext context)
        {
            this.SeedRoles(context);
            this.SeedUsers(context);
            this.SeedDiagnosis(context);
        }

        private void SeedDiagnosis(TeleConsultDbContext context)
        {
            if (context.Diagnosis.Any())
            {
                return;
            }

            int count = 0;

            using (var streamReader = new StreamReader(this.MapPath("~/App_Data/Seed/Diagnosis.csv"), System.Text.Encoding.UTF8, true))
            {
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine().Split(';');
                    var diagnosis = new Diagnosis();
                    diagnosis.Code = line[0];
                    diagnosis.Description = line[1];
                    context.Diagnosis.Add(diagnosis);

                    if (count % 200 == 0)
                    {
                        count = 0;
                        context.SaveChanges();
                    }

                    count++;
                }
            }
        }

        private void SeedUsers(TeleConsultDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            var store = new UserStore<User>(context);
            var manager = new UserManager<User>(store);

            var administrator = new User { UserName = GlobalConstants.AdministratorRoleName };
            manager.Create(administrator, GlobalConstants.InitialPassword);
            manager.AddToRole(administrator.Id, GlobalConstants.AdministratorRoleName);

            var operatorUser = new User { UserName = GlobalConstants.OperatorRoleName };
            manager.Create(operatorUser, GlobalConstants.InitialPassword);
            manager.AddToRole(operatorUser.Id, GlobalConstants.OperatorRoleName);

            context.SaveChanges();
        }

        private void SeedRoles(TeleConsultDbContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }

            context.Roles.Add(new IdentityRole { Name = GlobalConstants.AdministratorRoleName });
            context.Roles.Add(new IdentityRole { Name = GlobalConstants.SpecialistRoleName });
            context.Roles.Add(new IdentityRole { Name = GlobalConstants.OperatorRoleName });
            context.SaveChanges();
        }

        private string MapPath(string seedFile)
        {
            if (HttpContext.Current != null)
            {
                return HostingEnvironment.MapPath(seedFile);
            }

            var absolutePath = new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath;
            var directoryName = Path.GetDirectoryName(absolutePath);
            var path = Path.Combine(directoryName, string.Format("..{0}", seedFile.TrimStart('~').Replace('/', '\\')));

            return path;
        }
    }
}
