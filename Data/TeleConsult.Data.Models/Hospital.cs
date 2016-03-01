namespace TeleConsult.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using TeleConsult.Contracts;

    public class Hospital : DeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }
    }
}
