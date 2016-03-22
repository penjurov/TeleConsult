namespace TeleConsult.Data.Proxies
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using TeleConsult.Common;
    using TeleConsult.Data.Models.Enumerations;

    public class VisualExaminationProxy
    {
        [Required(ErrorMessage = GlobalConstants.DateRequireText)]
        [DisplayName(GlobalConstants.DateDisplay)]
        [UIHint("DateTemplate")]
        public DateTime? Date { get; set; }

        [Required(ErrorMessage = GlobalConstants.TypeRequireText)]
        [DisplayName(GlobalConstants.TypeDisplay)]
        [UIHint("EnumTemplate")]
        public VisualExaminationType Type { get; set; }

        [DisplayName(GlobalConstants.InputInformationDisplay)]
        [UIHint("MultiLineTemplate")]
        public string InputInformation { get; set; }

        [DisplayName(GlobalConstants.ConsultationInformationDisplay)]
        [UIHint("MultiLineTemplate")]
        public string ConsultInformation { get; set; }

        [Required]
        public string FileContent { get; set; }

        [Required]
        public string FileType { get; set; }
    }
}
