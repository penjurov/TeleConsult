namespace TeleConsult.Data.RepoFactory
{
    using System.Web.Mvc;

    public class RepoFactory : IRepoFactory
    {
        public T Get<T>() where T : class
        {
            return DependencyResolver.Current.GetService<T>();
        }
    }
}
