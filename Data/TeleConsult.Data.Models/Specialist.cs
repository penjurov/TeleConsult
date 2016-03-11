namespace TeleConsult.Data.Models
{
    using System.ComponentModel.DataAnnotations;
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
    }
}
