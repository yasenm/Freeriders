namespace FreeRiders.Web.Controllers
{
    using System.Web.Mvc;

    using FreeRiders.Data.Common;
    using FreeRiders.Data.UnitsOfWork;

    [Authorize(Roles = GlobalConstants.AdminRole)]
    public class AdminController : BaseController
    {
        public AdminController(IFreeRidersData data)
            : base(data)
        {
        }
    }
}