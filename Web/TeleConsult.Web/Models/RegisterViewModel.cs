namespace TeleConsult.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using TeleConsult.Web.Models.Base;

    public class RegisterViewModel : BaseModel
    {
        [Required]
        [Display(Name = "Потребител")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Паролата трябва да бъде поне {2} символа.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потвърдете паролата")]
        [Compare("Password", ErrorMessage = "Паролите не съвпадат.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }
    }
}