namespace TeleConsult.Data.Models
{    
    using System.ComponentModel.DataAnnotations;
    using Contracts;

    public class Speciality : DeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
