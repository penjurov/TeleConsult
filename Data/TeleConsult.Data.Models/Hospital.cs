namespace TeleConsult.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TeleConsult.Contracts;

    public class Hospital : DeletableEntity
    {
        public Hospital()
        {
            this.Specialists = new HashSet<Specialist>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public virtual ICollection<Specialist> Specialists { get; set; }
    }
}
