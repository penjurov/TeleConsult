namespace TeleConsult.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using Common;
    using TeleConsult.Web.Controllers.Base;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public abstract class AdminBaseController : BaseController
    {
    }
}