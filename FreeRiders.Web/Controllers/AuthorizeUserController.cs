namespace FreeRiders.Web.Controllers
{
    using System.Web.Mvc;

    using FreeRiders.Data.UnitsOfWork;

    [Authorize]
    public class AuthorizeUserController : BaseController
    {
        public AuthorizeUserController(IFreeRidersData data)
            : base(data)
        {
        }
    }
}