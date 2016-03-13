namespace TeleConsult.Data.Proxies
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Common;
    using Common.Helpers;
    using TeleConsult.Data.Models.Enumerations;

    public class SpecialistProxy
    {
        public string Id { get; set; }

        [DisplayName(GlobalConstants.NameDisplay)]
        [Required(ErrorMessage = GlobalConstants.NameRequireText)]
        [MinLength(3)]
        [UIHint("SingleLineTemplate")]
        public string FirstName { get; set; }

        [DisplayName(GlobalConstants.LastNameDisplay)]
        [Required(ErrorMessage = GlobalConstants.LastNameRequireText)]
        [MinLength(5)]
        [UIHint("SingleLineTemplate")]
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1} {2}", this.TitleName, this.FirstName, this.LastName);
            }
        }

        [DisplayName(GlobalConstants.UinDisplay)]
        [Required(ErrorMessage = GlobalConstants.UinRequireText)]
        [MinLength(10)]
        [MaxLength(10)]
        [UIHint("SingleLineTemplate")]
        public string Uin { get; set; }

        [DisplayName(GlobalConstants.SpecialistTitleDisplay)]
        [UIHint("EnumTemplate")]
        public Title Title { get; set; }

        public string TitleName
        {
            get
            {
                return this.Title.GetDescription();
            }
        }

        [DisplayName(GlobalConstants.PhoneDisplay)]
        [UIHint("SingleLineTemplate")]
        public string PhoneNumber { get; set; }

        [DisplayName(GlobalConstants.EmailDisplay)]
        [Required(ErrorMessage = GlobalConstants.EmailRequireText)]
        [DataType(DataType.EmailAddress)]
        [UIHint("SingleLineTemplate")]
        public string Email { get; set; }

        [Required(ErrorMessage = GlobalConstants.SpecialityRequireText)]
        [DisplayName(GlobalConstants.SpecialityDisplay)]
        [UIHint("DropDownTemplate")]
        public int SpecialityId { get; set; }

        public string SpecialityName { get; set; }

        [Required(ErrorMessage = GlobalConstants.HospitalRequireText)]
        [DisplayName(GlobalConstants.HospitalDisplay)]
        [UIHint("DropDownTemplate")]
        public int HospitalId { get; set; }

        public string HospitalName { get; set; }

        [Required(ErrorMessage = GlobalConstants.UserRequireText)]
        [DisplayName(GlobalConstants.UserDisplay)]
        [MinLength(6)]
        [UIHint("SingleLineTemplate")]
        public string UserName { get; set; }

        [Required(ErrorMessage = GlobalConstants.PasswordRequireText)]
        [DisplayName(GlobalConstants.PasswordDisplay)]
        [MinLength(6)]
        [UIHint("PasswordTemplate")]
        public string Password { get; set; }

        public bool IsDeleted { get; set; }
    }
}
