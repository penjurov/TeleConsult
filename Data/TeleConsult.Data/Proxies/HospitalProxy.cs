namespace TeleConsult.Data.Proxies
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Common;

    public class HospitalProxy
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = GlobalConstants.NameRequireText)]
        [DisplayName(GlobalConstants.NameDisplay)]
        [UIHint("SingleLineTemplate")]
        public string Name { get; set; }

        [DisplayName(GlobalConstants.AddressDisplay)]
        [UIHint("SingleLineTemplate")]
        public string Address { get; set; }

        [DisplayName(GlobalConstants.PhoneDisplay)]
        [UIHint("SingleLineTemplate")]
        public string Phone { get; set; }

        [UIHint("SingleLineTemplate")]
        public double? Latitude { get; set; }

        [UIHint("SingleLineTemplate")]
        public double? Longitude { get; set; }

        public bool IsDeleted { get; set; }
    }
}
