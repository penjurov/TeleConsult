namespace TeleConsult.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using TeleConsult.Web.Models.Base;

    public class ManageUserViewModel : BaseModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Текуща парола")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Паролата трябва да бъде поне {2} символа", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Нова парола")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потвърдете новата парола")]
        [Compare("NewPassword", ErrorMessage = "Паролите не съвпадат!")]
        public string ConfirmPassword { get; set; }
    }
}