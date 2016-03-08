namespace TeleConsult.Data.Filters.Admin
{
    public class AdminFilter : PagingFilter
    {
        public string Name { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
