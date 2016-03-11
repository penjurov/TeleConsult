namespace TeleConsult.Data.Filters.Admin
{
    public class SpecialistFilter : AdminFilter
    {
        public int? HospitalId { get; set; }

        public int? SpecialityId { get; set; }

        public int? Title { get; set; }
    }
}
