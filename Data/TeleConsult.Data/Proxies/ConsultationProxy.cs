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
        public Gender PatientGender { get; set; }

        [DisplayName(GlobalConstants.GenderDisplay)]
        [UIHint("SingleLineTemplate")]
        public string Gender
        {
            get
            {
                return this.PatientGender.GetDescription();
            }
        }

        [Required(ErrorMessage = GlobalConstants.PreliminaryDiagnosisRequireText)]
        [DisplayName(GlobalConstants.PreliminaryDiagnosisDisplay)]
        [UIHint("CodeTemplate")]
        public string PreliminaryDiagnosisCode { get; set; }

        [Required(ErrorMessage = GlobalConstants.PreliminaryDiagnosisDescriptionRequireText)]
        [UIHint("DescriptionTemplate")]
        public string PreliminaryDiagnosisDescription { get; set; }

        [Required(ErrorMessage = GlobalConstants.AnamnesisRequireText)]
        [DataType(DataType.MultilineText)]
        [DisplayName(GlobalConstants.AnamnesisDisplay)]
        [UIHint("MultiLineTemplate")]
        public string Anamnesis { get; set; }

        public IEnumerable<BloodExaminationProxy> BloodExaminations { get; set; }

        public IEnumerable<int> BloodExaminationIds { get; set; }

        public IEnumerable<UrinalysisProxy> Urinalysis { get; set; }

        public IEnumerable<int> UrinalysisIds { get; set; }

        public IEnumerable<VisualExaminationProxy> VisualExaminations { get; set; }

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
        public ConsultationType ConsultationType { get; set; }

        [DisplayName(GlobalConstants.TypeDisplay)]
        [UIHint("SingleLineTemplate")]
        public string Type
        {
            get
            {
                return this.ConsultationType.GetDescription();
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
        public DateTime ConsultationDate { get; set; }

        public string Date
        {
            get
            {
                return this.ConsultationDate.ToString();
            }
        }

        public bool Deleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
