﻿namespace TeleConsult.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Contracts;
    using Enumerations;
    
    public class Consultation : DeletableEntity
    {
        public Consultation()
        {
            this.BloodExaminations = new HashSet<BloodExamination>();
            this.VisualExaminations = new HashSet<VisualExamination>();
            this.Urinalysis = new HashSet<Urinalysis>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int SenderId { get; set; }

        public Specialist Sender { get; set; }

        public int? ConsultantId { get; set; }

        public Specialist Consultant { get; set; }

        [Required]
        public string PatientInitials { get; set; }

        [Required]
        public int PatientAge { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public string PreliminaryDiagnosisCode { get; set; }

        public virtual Diagnosis PreliminaryDiagnosis { get; set; }

        [Required]
        public string PreliminaryDiagnosisDescription { get; set; }

        [Required]
        public string Anamnesis { get; set; }

        public ICollection<BloodExamination> BloodExaminations { get; set; }

        public ICollection<VisualExamination> VisualExaminations { get; set; }

        public ICollection<Urinalysis> Urinalysis { get; set; }

        public string FinalDiagnosisCode { get; set; }

        public virtual Diagnosis FinalDiagnosis { get; set; }

        public string FinalDiagnosisDescription { get; set; }

        public string Conclusion { get; set; }

        [Required]
        public ConsultationType Type { get; set; }

        [Required]
        public ConsultationStage Stage { get; set; }

        [Required]
        public int SpecialityId { get; set; }

        public Speciality Speciality { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
