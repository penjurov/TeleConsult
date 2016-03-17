namespace TeleConsult.Data.Models.Enumerations
{
    using System.ComponentModel;

    public enum ActionType
    {
        [Description("Вход в системата")]
        Login,
        [Description("Изход от системата")]
        Logout,
        [Description("Добавяне лечебно заведение")]
        AddHospital,
        [Description("Редакция лечебно заведение")]
        EditHospital,
        [Description("Изтриване лечебно заведение")]
        DeleteHospital,
        [Description("Активиране лечебно заведение")]
        ActivateHospital,
        [Description("Добавяне специалност")]
        AddSpecialty,
        [Description("Редакция специалност")]
        EditSpecialty,
        [Description("Изтриване специалност")]
        DeleteSpecialty,
        [Description("Активиране специалност")]
        ActivateSpeciality,
        [Description("Добавяне специалист")]
        AddSpecialist,
        [Description("Редакция специалист")]
        EditSpecialist,
        [Description("Изтриване специалист")]
        DeleteSpecialist,
        [Description("Активиране специалист")]
        ActivateSpecialist,
        [Description("Добавяне график")]
        AddSchedule,
        [Description("Редакция график")]
        EditSchedule,
        [Description("Изтриване график")]
        DeleteSchedule,
        [Description("Активиране график")]
        ActivateSchedule,
        [Description("Изпращане консултация")]
        SendConsultation,
        [Description("Редакция консултация")]
        EditConsultation,
        [Description("Добавяне кръвно изследване")]
        AddBloodExamination,
        [Description("Редакция кръвно изследване")]
        EditBloodExamination,
        [Description("Добавяне изследване урина")]
        AddUrinalysis,
        [Description("Редакция изследване урина")]
        EditUrinalysis,
        [Description("Добавяне образно изследване")]
        AddVisualExamination,
        [Description("Редакция образно изследване")]
        EditVisualExamination
    }
}
