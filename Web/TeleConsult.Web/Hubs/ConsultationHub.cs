namespace TeleConsult.Web.Hubs
{
    using Microsoft.AspNet.SignalR;

    public class ConsultationHub : Hub
    {
        public static void Refresh(int consultationId, string consultantId, bool isInsert)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ConsultationHub>();
            context.Clients.All.refresh(consultationId, consultantId, isInsert);
        }

        public static void RefreshEmergency(int consultationId, bool isInsert)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ConsultationHub>();
            context.Clients.All.refreshEmergency(consultationId, isInsert);
        }
    }
}