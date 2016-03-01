namespace TeleConsult.Data.Models.Enumerations
{
    using System.ComponentModel;

    public enum ActionType
    {
        [Description("Logged into system")]
        Login,
        AddHospital,
        EditHospital,
        DeleteHospital,
        AddSpecialty,
        EditSpecialty,
        DeleteSpecialty,
        AddSpecialist,
        EditSpecialist,
        DeleteSpecialist,
        AddSchedule,
        EditSchedule,
        DeleteSchedule,
        AddConsultation,
        EditConsultation,
        AddBloodExamination,
        EditBloodExamination,
        AddUrinalysis,
        EditUrinalysis,
        AddVisualExamination,
        EditVisualExamination
    }
}
