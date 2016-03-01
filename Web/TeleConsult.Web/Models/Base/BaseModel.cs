namespace TeleConsult.Web.Models.Base
{
    using System;
    using Data.RepoFactory;
    using Microsoft.Practices.Unity;

    public abstract class BaseModel : IModel
    {
        [Dependency]
        public IRepoFactory RepoFactory { get; set; }

        public void Init()
        {
        }
    }
}