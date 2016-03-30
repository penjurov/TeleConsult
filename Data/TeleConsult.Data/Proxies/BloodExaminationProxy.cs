namespace TeleConsult.Data.Proxies
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Common;

    public class BloodExaminationProxy
    {
        [HiddenInput(DisplayValue = false)]
        public int? Id { get; set; }

        [Required(ErrorMessage = GlobalConstants.DateRequireText)]
        [DisplayName(GlobalConstants.DateDisplay)]
        [UIHint("DateTemplate")]
        public DateTime? Date { get; set; }

        [DisplayName(GlobalConstants.HemoglobinDisplay)]
        [UIHint("DoubleTemplate")]
        public double? Hemoglobin { get; set; }

        [DisplayName(GlobalConstants.ErythrocytesDisplay)]
        [UIHint("DoubleTemplate")]
        public double? Erythrocytes { get; set; }

        [UIHint("DoubleTemplate")]
        public double? Hct { get; set; }

        [UIHint("DoubleTemplate")]
        public double? Leuc { get; set; }

        [UIHint("DoubleTemplate")]
        public double? Mchc { get; set; }

        [UIHint("DoubleTemplate")]
        public double? Mch { get; set; }

        [UIHint("DoubleTemplate")]
        public double? Mcv { get; set; }

        [UIHint("DoubleTemplate")]
        public double? Ret { get; set; }

        [UIHint("DoubleTemplate")]
        public double? Sue { get; set; }

        [DisplayName(GlobalConstants.BleedingTimeDisplay)]
        [UIHint("DoubleTemplate")]
        public double? BleedingTime { get; set; }

        [DisplayName(GlobalConstants.CoagulationTimeDisplay)]
        [UIHint("DoubleTemplate")]
        public double? CoagulationTime { get; set; }

        [DisplayName(GlobalConstants.MorphologyErythrocytesDisplay)]
        [UIHint("DoubleTemplate")]
        public double? MorphologyErythrocytes { get; set; }

        [DisplayName(GlobalConstants.BloodSugarDisplay)]
        [UIHint("DoubleTemplate")]
        public double? BloodSugar { get; set; }
    }
}
