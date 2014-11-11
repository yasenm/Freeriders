namespace FreeRiders.Web.Controllers
{
    using System.Web.Mvc;

    using FreeRiders.Data.UnitsOfWork;

    public class LocationsController : BaseController
    {
        public LocationsController(IFreeRidersData data)
            :base(data)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LocationDetails(int id)
        {
            var result = this.Data.Locations.Find(id);

            return View(result);
        }
    }
}