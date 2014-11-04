namespace FreeRiders.Web.Controllers
{
    using System.Web.Mvc;

    using FreeRiders.Data.UnitsOfWork;

    public abstract class BaseController : Controller
    {
        private IFreeRidersData Data;
    }
}