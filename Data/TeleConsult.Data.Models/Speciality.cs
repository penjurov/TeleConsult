﻿namespace TeleConsult.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Speciality
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
