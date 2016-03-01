namespace TeleConsult.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using TeleConsult.Contracts;

    public class Diagnosis : DeletableEntity
    {
        [Key]
        [Index(IsUnique = true)]
        public string Code { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
