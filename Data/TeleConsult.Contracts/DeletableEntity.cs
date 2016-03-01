namespace TeleConsult.Contracts
{
    using System;

    public class DeletableEntity : IDeletableEntity
    {
        public bool Deleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
