namespace TeleConsult.Data.Models.Enumerations
{
    using System.ComponentModel;
    using TeleConsult.Common;

    public enum ConsultationType
    {
        [Description(GlobalConstants.Planned)]
        Planned,
        [Description(GlobalConstants.Emergency)]
        Emergency
    }
}
