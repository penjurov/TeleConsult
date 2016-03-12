namespace TeleConsult.Web.Infrastructure.Logging
{
    using System;
    using System.Web;

    using Data.Models;
    using Data.Models.Enumerations;
    using Data.RepoFactory;
    using Data.Repositories;
    using Microsoft.Practices.Unity;

    public class DataBaseLogger : ILogger
    {
        private static readonly object Padlock = new object();
        private static DataBaseLogger instance;
        private static IRepoFactory factory;

        public static DataBaseLogger Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (Padlock)
                    {
                        if (instance == null)
                        {
                            instance = new DataBaseLogger();
                            factory = new RepoFactory();
                        }
                    }
                }

                return instance;
            }
        }

        public void Log(ActionType action, string details, string userId = null)
        {
            var logRepo = factory.Get<LogRepository>();
            var userRepo = factory.Get<UserRepository>();

            if (userId == null)
            {
                userId = userRepo.GetUserId(HttpContext.Current.User.Identity.Name);
            }
            
            var log = new Log()
            {
                Date = DateTime.Now,
                Action = action,
                Details = details,
                UserId = userId
            };

            logRepo.Add(log);
            logRepo.SaveChanges();
        }
    }
}