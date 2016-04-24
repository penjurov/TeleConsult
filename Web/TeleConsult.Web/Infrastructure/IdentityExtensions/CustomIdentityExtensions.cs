namespace TeleConsult.Web.Infrastructure.IdentityExtensions
{
    using System.Collections.Generic;
    using System.Security.Principal;

    using Common;
    using Common.Helpers;
    using Data.RepoFactory;
    using Data.Repositories;

    public static class CustomIdentityExtensions
    {
        private static Dictionary<string, string> displayNames;

        public static string GetUserName(IPrincipal loggedUser)
        {
            var result = string.Empty;

            if (displayNames == null)
            {
                displayNames = new Dictionary<string, string>();
            }

            var userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(loggedUser.Identity);

            if (displayNames.ContainsKey(userId))
            {
                result = displayNames[userId];
            }
            else
            {
                var repoFactory = new RepoFactory();

                if (loggedUser.IsInRole(GlobalConstants.SpecialistRoleName))
                {
                    var specialist = repoFactory.Get<SpecialistRepository>().GetById(userId);
                    result = string.Format("{0} {1} {2}", specialist.Title.GetDescription(), specialist.FirstName, specialist.LastName);
                }
                else
                {
                    var user = repoFactory.Get<UserRepository>().GetById(userId);
                    result = string.IsNullOrEmpty(user.FirstName) ? user.UserName : string.Format("{0} {1}", user.FirstName, user.LastName);
                }

                displayNames[userId] = result;
            }

            return result;
        }

        public static void ClearDisplayName(IPrincipal loggedUser)
        {
            var userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(loggedUser.Identity);

            displayNames.Remove(userId);
        }
    }
}