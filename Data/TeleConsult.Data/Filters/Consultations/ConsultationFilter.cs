namespace TeleConsult.Data.Filters.Consultations
{
    public class ConsultationFilter : PagingFilter
    {
        public int ConsultationId { get; set; }

        public bool IsConsultation { get; set; }

        public string SpecialistId { get; set; }
    }
}
