namespace TeleConsult.Data.Models.Enumerations
{
    using System.ComponentModel;
    using TeleConsult.Common;

    public enum Gender
    {
        [Description(GlobalConstants.Male)]
        Male,
        [Description(GlobalConstants.Female)]
        Female
    }
}
