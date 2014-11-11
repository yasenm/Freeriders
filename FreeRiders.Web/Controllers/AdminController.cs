namespace FreeRiders.Web.Controllers
{
    using System.Web.Mvc;

    using FreeRiders.Data.UnitsOfWork;

    //[Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        public AdminController(IFreeRidersData data)
            : base(data)
        {
        }
    }
}