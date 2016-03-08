namespace TeleConsult.Data.Proxies
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using TeleConsult.Common;

    public class SpecialityProxy
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = GlobalConstants.NameRequireText)]
        [DisplayName(GlobalConstants.NameDisplay)]
        [UIHint("SingleLineTemplate")]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}
