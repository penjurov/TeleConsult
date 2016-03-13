namespace TeleConsult.Data.Models
{
    using System;

    using System.ComponentModel.DataAnnotations;
    using TeleConsult.Contracts;

    public class Schedule : DeletableEntity
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }

        [Required]
        public string SpecialistId { get; set; }

        public virtual Specialist Specialist { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public bool IsAllDay { get; set; }
    }
}
