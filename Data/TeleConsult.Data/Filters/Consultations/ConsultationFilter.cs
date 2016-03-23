namespace TeleConsult.Data.Filters.Consultations
{
    public class ConsultationFilter : PagingFilter
    {
        public bool IsConsultation { get; set; }

        public string SpecialistId { get; set; }
    }
}
