namespace TeleConsult.Web.Infrastructure.Logging
{
    using Data.Models.Enumerations;

    public interface ILogger
    {
        void Log(ActionType action, string actionInfo, string userId = null);
    }
}
