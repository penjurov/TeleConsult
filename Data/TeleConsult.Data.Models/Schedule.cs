namespace TeleConsult.Data.Models
{
    using System;

    using System.ComponentModel.DataAnnotations;
    using TeleConsult.Contracts;

    public class Schedule : DeletableEntity
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public int SpecialistId { get; set; }

        public virtual Specialist Specialist { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartingDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndingDate { get; set; }

        public string StartTimezone { get; set; }

        public string EndTimezone { get; set; }

        public bool IsAllDay { get; set; }

        public string RecurrenceException { get; set; }

        public string RecurrenceRule { get; set; }
    }
}
