using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TeleConsult.Common;

namespace TeleConsult.Data.Proxies
{
    public class ScheduleProxy
    {
        public int? Id { get; set; }

        [DisplayName(GlobalConstants.DescriptionDisplay)]
        [UIHint("MultiLineTemplate")]
        public string Description { get; set; }

        [Required(ErrorMessage = GlobalConstants.SpecialistRequireText)]
        [DisplayName(GlobalConstants.SpecialistDisplay)]
        [UIHint("DropDownTemplate")]
        public string SpecialistId { get; set; }

        public string SpecialistName
        {
            get
            {
                return this.Specialist.FullName;
            }
        }

        public SpecialistProxy Specialist { get; set; }

        public string SpecialistSpeciality { get; set; }

        [Required(ErrorMessage = GlobalConstants.StartRequireText)]
        [DisplayName(GlobalConstants.StartDisplay)]
        [UIHint("DateTemplate")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = GlobalConstants.EndRequireText)]
        [DisplayName(GlobalConstants.EndDisplay)]
        [UIHint("DateTemplate")]
        public DateTime? EndDate { get; set; }

        public bool IsAllDay { get; set; }
    }
}
