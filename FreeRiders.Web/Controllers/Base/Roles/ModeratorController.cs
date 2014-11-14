namespace FreeRiders.Web.Controllers
{
    using System.Web.Mvc;

    using FreeRiders.Data.Common;
    using FreeRiders.Data.UnitsOfWork;

    [Authorize(Roles = GlobalConstants.ModeratorRole)]
    public class ModeratorController : BaseController
    {
        public ModeratorController(IFreeRidersData data)
            : base(data)
        {
        }
    }
}