namespace TeleConsult.Data.Models.Enumerations
{
    using System.ComponentModel;
    using TeleConsult.Common;

    public enum ExaminationType
    {
        [Description(GlobalConstants.Ecg)]
        Ecg,
        [Description(GlobalConstants.Xray)]
        XRay,
        [Description(GlobalConstants.Holter)]
        Holter,
        [Description(GlobalConstants.Scanner)]
        Scanner,
        [Description(GlobalConstants.Mammography)]
        Mammography,
        [Description(GlobalConstants.Other)]
        Other
    }
}
