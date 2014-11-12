namespace FreeRiders.Web.Controllers
{
    using System.Web.Mvc;

    using FreeRiders.Data.UnitsOfWork;

    public class EventController : BaseController
    {
        public EventController(IFreeRidersData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}