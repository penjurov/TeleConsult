using System.ComponentModel.DataAnnotations;

namespace TeleConsult.Web.Models
{
    public class ResetPasswordViewModel
    {
        [Required]
        [Display(Name = "Потребител")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Паролата трябва да бъде поне {2} символа", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потвърдете паролата")]
        [Compare("Password", ErrorMessage = "Паролите не съвпадат")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}