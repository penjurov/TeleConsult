namespace TeleConsult.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TeleConsult.Contracts;
    using TeleConsult.Data.Models.Enumerations;

    public class VisualExamination : DeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public VisualExaminationType Type { get; set; }

        public string InputInformation { get; set; }

        public string ConsultInformation { get; set; }

        [Required]
        public byte[] FileContent { get; set; }

        [Required]
        public string FileType { get; set; }

        public int ConsultationId { get; set; }

        public virtual Consultation Consultation { get; set; }
    }
}
