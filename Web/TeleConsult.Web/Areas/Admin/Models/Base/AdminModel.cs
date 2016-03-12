namespace TeleConsult.Web.Areas.Admin.Models.Base
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Common;
    using Web.Models;
    using Web.Models.Base;
    
    public class AdminModel : BaseModel, IModel<bool>
    {
        public IEnumerable<SelectListItem> Statuses { get; set; }

        public virtual void Init(bool init)
        {
            if (init)
            {
                if (init)
                {
                    this.Statuses = new List<SelectListItem>()
                {
                    new SelectListItem
                    {
                        Text = GlobalConstants.Statuses.Active,
                        Value = bool.FalseString,
                        Selected = true
                    },
                    new SelectListItem
                    {
                        Text = GlobalConstants.Statuses.Deleted,
                        Value = bool.TrueString
                    },
                };
                }
            }
        }
    }
}