namespace TeleConsult.Data.Filters
{  
    using System;
    using Helpers;

    [Serializable]
    public class PagingFilter
    {
        public int Limit { get; set; }

        public int Page { get; set; }

        public int Start
        {
            get
            {
                return (this.Page - 1) * this.Limit;
            }
        }

        public string SortBy { get; set; }

        public string Direction { get; set; }

        public SortDirection SortDirection
        {
            get
            {
                return Sort.GetDirection(this.Direction);
            }
        }

        public long Count { get; set; }

        public PagingFilter CopyPaging(PagingFilter filter)
        {
            this.Limit = filter.Limit;
            this.Page = filter.Page;
            this.SortBy = filter.SortBy;
            this.Direction = filter.Direction;

            return this;
        }
    }
}
