﻿namespace TeleConsult.Web.Areas.Consultation.Controllers
{
    using System.Web.Mvc;
    using Common;
    using TeleConsult.Web.Controllers.Base;

    [Authorize(Roles = GlobalConstants.SpecialistRoleName)]
    public abstract class ConsultationBaseController : BaseController
    {
    }
}