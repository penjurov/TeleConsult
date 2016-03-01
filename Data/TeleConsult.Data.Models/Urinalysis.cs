namespace TeleConsult.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TeleConsult.Contracts;

    public class Urinalysis : DeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public double? SpecificGravity { get; set; }

        public double? Ph { get; set; }

        public double? Protein { get; set; }

        public double? ProteinWeight { get; set; }

        public double? Glucose { get; set; }

        public double? GlucoseWeight { get; set; }

        public double? KetoneBodies { get; set; }

        public double? Bilirubin { get; set; }

        public double? Urobilinogen { get; set; }

        public double? Blood { get; set; }

        public double? Porphobilinogen { get; set; }

        public double? Amylase { get; set; }

        public double? Ketosteroids { get; set; }

        public double? Diuresis { get; set; }

        public string Sediments { get; set; }

        public string FormedElements { get; set; }

        public virtual ICollection<Consultation> Consultations { get; set; }
    }
}
