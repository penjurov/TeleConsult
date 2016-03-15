namespace TeleConsult.Common
{
    using System.Collections.Generic;

    public class GlobalConstants
    {
        public const string ProjectTitle = "Теле Консулт";
        public const string AdministratorRoleName = "Admin";
        public const string SpecialistRoleName = "Specialist";
        public const string InitialPassword = "123456";

        // Enums
        public const string Male = "Мъж";
        public const string Female = "Жена";
        public const string Planned = "Планов";
        public const string Emergency = "Спешен";
        public const string Professor = "професор";
        public const string AssociateProfessor = "доцент";
        public const string AssitantProfessor = "асистент";
        public const string Md = "д-р";
        public const string Ecg = "ЕКГ";
        public const string Xray = "Рентген";
        public const string Holter = "Холтер";
        public const string Scanner = "Скенер";
        public const string Mammography = "Мамография";
        public const string Other = "Друго";

        // Areas
        public const string AdminAreaName = "Admin";
        public const string ConsultationAreaName = "Consultation";

        // Controllers
        public const string AdministrationControllerTitle = "Администрация";
        public const string HospitalsControllerTitle = "Лечебни заведения";
        public const string SpecialistsControllerTitle = "Специалисти";
        public const string SpecialtiesControllerTitle = "Специалности";
        public const string SchedulesControllerTitle = "График";
        public const string LogsControllerTitle = "Системна хронология";
        public const string ConsultationsControllerTitle = "Консултации";
        public const string NewConsultationsControllerTitle = "Нова";
        public const string SentConsultationsControllerTitle = "Изпратени";
        public const string RecievedConsultationsControllerTitle = "Получени";
        public const string ReportsControllerTitle = "Отчети";

        // User
        public const string Login = "Вход";
        public const string Register = "Регистрация";
        public const string Logoff = "Изход";
        public const string Hello = "Здравей ";
        public const string Manage = "Управление на акаунта";
        public const string Resetpassword = "Рестартиране на парола";
        public const string InvalidLoginAttempt = "Грешно потребителско име или парола";
        public const string CreateNewAccount = "Нов потребител";

        // Grid
        public const string Send = "Изпрати";
        public const string EditLabel = "Редакция";
        public const string AddEditLabel = "Добави/Редактирай";
        public const string Create = "Нов запис";
        public const string Update = "Обнови";
        public const string Delete = "Изтрий";
        public const string Cancel = "Отказ";
        public const string GetLocation = "Локация";
        public const string ShowLocation = "Покажи на карта";

        // Scheduler
        public const string Specialist = "Специалист";
        public const string TimeZone = "Etc/GMT+2";

        // ViewModels

        // Hospital
        public const string NameDisplay = "Име";
        public const string NameRequireText = "Името е задължително";
        public const string AddressDisplay = "Адрес";
        public const string PhoneDisplay = "Телефон";

        // Log
        public const string DateDisplay = "Дата";
        public const string DateRequireText = "Датата е задължителна";
        public const string UserDisplay = "Потребител";
        public const string ActionDisplay = "Действие";
        public const string InformationDisplay = "Информация";

        // Schedule
        public const string SpecialistDisplay = "Специалист";
        public const string SpecialistRequireText = "Специалистът е задължителен";
        public const string TitleDisplay = "Заглавие";
        public const string DescriptionDisplay = "Описание";
        public const string StartDisplay = "Начало";
        public const string StartRequireText = "Началния час е задължителен";
        public const string EndDisplay = "Край";
        public const string EndRequireText = "Крайния час е задължителен";

        // Specialist
        public const string LastNameDisplay = "Фамилия";
        public const string LastNameRequireText = "Фамилията е задължителна";
        public const string UinDisplay = "УИН";
        public const string UinRequireText = "УИН е задължителен";
        public const string SpecialistTitleDisplay = "Титла";
        public const string SpecialityDisplay = "Специалност";
        public const string SpecialityRequireText = "Специалността е задължителна";
        public const string HospitalDisplay = "Лечебно заведение";
        public const string HospitalRequireText = "Лечебното заведение е задължително";
        public const string UserRequireText = "Потребителското име е задължително";
        public const string PasswordDisplay = "Парола";
        public const string PasswordRequireText = "Паролата е задължителна";
        public const string EmailDisplay = "Имейл";
        public const string EmailRequireText = "Имейлът е задължителен";

        // Blood Examination
        public const string HemoglobinDisplay = "Хемоглобин";
        public const string ErythrocytesDisplay = "Еритроцити";
        public const string BleedingTimeDisplay = "Време кървене";
        public const string CoagulationTimeDisplay = "Време съсирване";
        public const string MorphologyErythrocytesDisplay = "Морфология еритроцити";
        public const string BloodSugarDisplay = "Кръвна захар";

        // Consultation
        public const string PatientInitialsDisplay = "Инициали на пациента";
        public const string PatientInitialsRequireText = "Инициалите на пациента са задължителни";
        public const string PatientInitialsMinConstrainText = "Моля напишете поне 2 символа";
        public const string PatientInitialsMaxConstrainText = "Инициалите са максимум 3 символа";
        public const string PatientAgeDisplay = "Възраст на пациента";
        public const string PatientAgeRequireText = "Възрастта на пациента е задължителна";
        public const string PatientAgeConstrainText = "Моля изберете число между 1 и 120";
        public const string GenderDisplay = "Пол на пациента";
        public const string GenderRequireText = "Пола на пациента е задължителен";
        public const string PreliminaryDiagnosisDisplay = "Предполагаема диагноза";
        public const string PreliminaryDiagnosisRequireText = "Предполагаемата диагноза е задължителна";
        public const string AnamnesisDisplay = "Анамнеза";
        public const string AnamnesisRequireText = "Анамнезата е задължителна";
        public const string FinalDiagnosisDisplay = "Диагноза";
        public const string ConclusionDisplay = "Заключение";
        public const string TypeDisplay = "Тип";

        // Urinalysis
        public const string SpecificGravityDisplay = "Специфично тегло";
        public const string ProteinDisplay = "Белтък";
        public const string ProteinWeightDisplay = "Белтък - количество";
        public const string GlucoseDisplay = "Захар";
        public const string GlucoseWeightDisplay = "Захар - количество";
        public const string KetoneBodiesDisplay = "Кетонни тела";
        public const string BilirubinDisplay = "Билирубин";
        public const string UrobilinogenDisplay = "Уробилиноген";
        public const string BloodDisplay = "Кръв";
        public const string PorphobilinogenDisplay = "Порфобилиноген";
        public const string AmylaseDisplay = "Амилаза";
        public const string KetosteroidsDisplay = "17 - кетостероиди";
        public const string DiuresisDisplay = "Диуреза(24 ч.)";
        public const string SedimentsDisplay = "Седимент";
        public const string FormedElementsDisplay = "Формени елементи";

        // VisualExamination
        public const string TypeRequireText = "Типът е задължителен";
        public const string InputInformationDisplay = "Информация";
        public const string ConsultationInformationDisplay = "Консултация";

        // ActionLinks
        public const string BloodExaminationLink = "Кръвни изследвания";
        public const string UrinalysisLink = "Изследвания урина";
        public const string VisualExaminationLink = "Образни изследвания";
        public const string View = "Разгледай";

        public static class Statuses
        {
            public const string All = "Всички";
            public const string Active = "Активни";
            public const string Deleted = "Изтрити";
        }

        public static class Errors
        {
            public const string General = "Грешка с данните";
            public const string HospitalExist = "Лечебно заведение с такова име вече съществува";
            public const string SpecialityExist = "Специалност с такова име вече съществува";
            public const string SpecialistExist = "Специалист с такова име вече съществува";
        }
    }
}
