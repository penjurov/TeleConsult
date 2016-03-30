namespace TeleConsult.Data.Proxies
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Common;

    public class UrinalysisProxy
    {
        [HiddenInput(DisplayValue = false)]
        public int? Id { get; set; }

        [Required(ErrorMessage = GlobalConstants.DateRequireText)]
        [DisplayName(GlobalConstants.DateDisplay)]
        [UIHint("DateTemplate")]
        public DateTime? Date { get; set; }

        [DisplayName(GlobalConstants.SpecificGravityDisplay)]
        [UIHint("DoubleTemplate")]
        public double? SpecificGravity { get; set; }

        [UIHint("DoubleTemplate")]
        public double? Ph { get; set; }

        [DisplayName(GlobalConstants.ProteinDisplay)]
        [UIHint("DoubleTemplate")]
        public double? Protein { get; set; }

        [DisplayName(GlobalConstants.ProteinWeightDisplay)]
        [UIHint("DoubleTemplate")]
        public double? ProteinWeight { get; set; }

        [DisplayName(GlobalConstants.GlucoseDisplay)]
        [UIHint("DoubleTemplate")]
        public double? Glucose { get; set; }

        [DisplayName(GlobalConstants.GlucoseWeightDisplay)]
        [UIHint("DoubleTemplate")]
        public double? GlucoseWeight { get; set; }

        [DisplayName(GlobalConstants.KetoneBodiesDisplay)]
        [UIHint("DoubleTemplate")]
        public double? KetoneBodies { get; set; }

        [DisplayName(GlobalConstants.BilirubinDisplay)]
        [UIHint("DoubleTemplate")]
        public double? Bilirubin { get; set; }

        [DisplayName(GlobalConstants.UrobilinogenDisplay)]
        [UIHint("DoubleTemplate")]
        public double? Urobilinogen { get; set; }

        [DisplayName(GlobalConstants.BloodDisplay)]
        [UIHint("DoubleTemplate")]
        public double? Blood { get; set; }

        [DisplayName(GlobalConstants.PorphobilinogenDisplay)]
        [UIHint("DoubleTemplate")]
        public double? Porphobilinogen { get; set; }

        [DisplayName(GlobalConstants.AmylaseDisplay)]
        [UIHint("DoubleTemplate")]
        public double? Amylase { get; set; }

        [DisplayName(GlobalConstants.KetosteroidsDisplay)]
        [UIHint("DoubleTemplate")]
        public double? Ketosteroids { get; set; }

        [DisplayName(GlobalConstants.DiuresisDisplay)]
        [UIHint("DoubleTemplate")]
        public double? Diuresis { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName(GlobalConstants.SedimentsDisplay)]
        [UIHint("MultiLineTemplate")]
        public string Sediments { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName(GlobalConstants.FormedElementsDisplay)]
        [UIHint("MultiLineTemplate")]
        public string FormedElements { get; set; }
    }
}
