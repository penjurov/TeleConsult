namespace TeleConsult.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Practices.Unity;
    using Proxies;
    using TeleConsult.Data.Models;

    public class UserRepository : BaseRepository<User>
    {
        [InjectionConstructor]
        public UserRepository(ITeleConsultDbContext context)
            : base(context)
        {
        }

        public string GetUserId(string userName)
        {
            return this.All().FirstOrDefault(u => u.UserName == userName).Id;
        }

        public IEnumerable<UserProxy> GetActive()
        {
            var result = this.All().Where(s => !s.IsDeleted).ToList();
            return this.GetProxy(result);
        }

        private IEnumerable<UserProxy> GetProxy(List<User> result)
        {
            return result.Select(s => new UserProxy
            {
                Id = s.Id,
                Name = (!string.IsNullOrEmpty(s.FirstName) && !string.IsNullOrEmpty(s.LastName)) ? s.FirstName + " " + s.LastName : s.UserName
            });
        }
    }
}
