namespace TeleConsult.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using TeleConsult.Contracts;
    using TeleConsult.Data.Models.Enumerations;

    public class Log : DeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public DateTime ActionDate { get; set; }

        [Required]
        public ActionType Action { get; set; }

        [Required]
        public string ActionInfo { get; set; }
    }
}
