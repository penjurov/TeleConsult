namespace TeleConsult.Web.Models.Base
{
    using System.Linq;
    using System.Web.Mvc;

    using Common;
    using Data.RepoFactory;
    using Microsoft.Practices.Unity;
    using System.Data.Entity.Validation;
    using System.Text;
    public abstract class BaseModel : IModel
    {
        [Dependency]
        public IRepoFactory RepoFactory { get; set; }

        public void Init()
        {
        }

        protected string HandleErrors(ModelStateDictionary modelState)
        {
            var error = GlobalConstants.Errors.General;

            foreach (var value in modelState.Values)
            {
                if (value.Errors.Count > 0)
                {
                    error = value.Errors.FirstOrDefault().ErrorMessage;
                    break;
                }
            }

            return error;
        }

        protected string HandleDbEntityValidationException(DbEntityValidationException e)
        {
            StringBuilder builder = new StringBuilder();

            foreach (var eve in e.EntityValidationErrors)
            {
                builder.AppendLine(string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                    eve.Entry.Entity.GetType().Name, eve.Entry.State));
                foreach (var ve in eve.ValidationErrors)
                {
                    builder.AppendLine(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                }
            }

            return builder.ToString();
        }
    }
}