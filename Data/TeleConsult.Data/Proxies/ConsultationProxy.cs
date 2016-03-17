namespace TeleConsult.Data.Proxies
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using TeleConsult.Common;
    using TeleConsult.Common.Helpers;
    using TeleConsult.Data.Models.Enumerations;

    public class ConsultationProxy
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string SenderId { get; set; }

        public string ConsultantId { get; set; }

        [Required(ErrorMessage = GlobalConstants.PatientInitialsRequireText)]
        [DisplayName(GlobalConstants.PatientInitialsDisplay)]
        [MinLength(2, ErrorMessage = GlobalConstants.PatientInitialsMinConstrainText)]
        [MaxLength(3, ErrorMessage = GlobalConstants.PatientInitialsMaxConstrainText)]
        [UIHint("SingleLineTemplate")]
        public string PatientInitials { get; set; }

        [Required(ErrorMessage = GlobalConstants.PatientAgeRequireText)]
        [Range(1, 120, ErrorMessage = GlobalConstants.PatientAgeConstrainText)]
        [DisplayName(GlobalConstants.PatientAgeDisplay)]
        [UIHint("IntTemplate")]
        public int PatientAge { get; set; }

        [Required(ErrorMessage = GlobalConstants.GenderRequireText)]
        [DisplayName(GlobalConstants.GenderDisplay)]
        [UIHint("EnumTemplate")]
        public Gender Gender { get; set; }

        [DisplayName(GlobalConstants.GenderDisplay)]
        [UIHint("SingleLineTemplate")]
        public string GenderName
        {
            get
            {
                return this.Gender.GetDescription();
            }
        }

        [Required(ErrorMessage = GlobalConstants.PreliminaryDiagnosisRequireText)]
        [DisplayName(GlobalConstants.PreliminaryDiagnosisDisplay)]
        [UIHint("CodeTemplate")]
        public string PreliminaryDiagnosisCode { get; set; }

        [UIHint("DescriptionTemplate")]
        public string PreliminaryDiagnosisDescription { get; set; }

        [Required(ErrorMessage = GlobalConstants.AnamnesisRequireText)]
        [DataType(DataType.MultilineText)]
        [DisplayName(GlobalConstants.AnamnesisDisplay)]
        [UIHint("MultiLineTemplate")]
        public string Anamnesis { get; set; }

        public IEnumerable<int> BloodExaminationIds { get; set; }

        public IEnumerable<int> UrinalysisIds { get; set; }

        public IEnumerable<int> VisualExaminationIds { get; set; }

        [DisplayName(GlobalConstants.FinalDiagnosisDisplay)]
        [UIHint("CodeTemplate")]
        public string FinalDiagnosisCode { get; set; }

        [UIHint("DescriptionTemplate")]
        public string FinalDiagnosisDescription { get; set; }

        [DisplayName(GlobalConstants.ConclusionDisplay)]
        [DataType(DataType.MultilineText)]
        [UIHint("MultiLineTemplate")]
        public string Conclusion { get; set; }

        [DisplayName(GlobalConstants.TypeDisplay)]
        [Required]
        [UIHint("EnumTemplate")]
        public ConsultationType Type { get; set; }

        [DisplayName(GlobalConstants.TypeDisplay)]
        [UIHint("SingleLineTemplate")]
        public string TypeName
        {
            get
            {
                return this.Type.GetDescription();
            }
        }

        [Required]
        public ConsultationStage Stage { get; set; }

        [Required(ErrorMessage = GlobalConstants.SpecialityRequireText)]
        [DisplayName(GlobalConstants.SpecialityDisplay)]
        [UIHint("DropDownTemplate")]
        public int SpecialityId { get; set; }

        [Required]
        [DisplayName(GlobalConstants.DateDisplay)]
        [UIHint("DateTemplate")]
        public DateTime Date { get; set; }

        public bool Deleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
