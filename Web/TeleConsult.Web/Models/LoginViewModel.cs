namespace TeleConsult.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using TeleConsult.Web.Models.Base;

    public class LoginViewModel : BaseModel
    {
        [Required]
        [Display(Name = "Потребител")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [Display(Name = "Запомни ме?")]
        public bool RememberMe { get; set; }
    }
}