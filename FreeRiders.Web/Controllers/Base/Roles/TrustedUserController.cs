namespace FreeRiders.Web.Controllers
{
    using System.Web.Mvc;

    using FreeRiders.Data.Common;
    using FreeRiders.Data.UnitsOfWork;

    [Authorize(Roles = GlobalConstants.TrustedUserRole)]
    public class TrustedUserController : BaseController
    {
        public TrustedUserController(IFreeRidersData data)
            : base(data)
        {
        }
    }
}