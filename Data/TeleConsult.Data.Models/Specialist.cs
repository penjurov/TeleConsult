namespace TeleConsult.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using TeleConsult.Data.Models.Enumerations;

    public class Specialist : User
    {
        [Required]
        [StringLength(10)]
        public string Uin { get; set; }

        public Title Title { get; set; }

        [Required]
        public int SpecialityId { get; set; }

        public virtual Speciality Speciality { get; set; }

        [Required]
        public int HospitalId { get; set; }

        public virtual Hospital Hospital { get; set; }

        [InverseProperty("Consultant")]
        public virtual ICollection<Consultation> Consultations { get; set; }
    }
}
