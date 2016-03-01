using System.ComponentModel.DataAnnotations;

namespace TeleConsult.Web.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [Display(Name = "Потребителско име")]
        public string UserName { get; set; }
    }
}