using System.ComponentModel.DataAnnotations;

namespace TeleConsult.Web.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Потребител")]
        public string UserName { get; set; }
    }
}