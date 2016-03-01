namespace TeleConsult.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using TeleConsult.Data.Models.Enumerations;

    public class Specialist : User
    {
        public Title Title { get; set; }

        [Required]
        public int SpecialityId { get; set; }

        public virtual Speciality Speciality { get; set; }

        [Required]
        public int HospitalId { get; set; }

        public virtual Hospital Hospital { get; set; }
    }
}
