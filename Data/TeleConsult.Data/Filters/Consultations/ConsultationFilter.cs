namespace TeleConsult.Data.Filters.Consultations
{
    public class ConsultationFilter : PagingFilter
    {
        public int ConsultationId { get; set; }

        public bool IsConsultation { get; set; }

        public string SpecialistId { get; set; }

        public string HospitalName { get; set; }

        public int? SpecialityId { get; set; }

        public int? TypeId { get; set; }

        public int? GenderId { get; set; }
    }
}
