namespace FreeRiders.Web.Controllers
{
    using System.Web.Mvc;

    public class LocationsController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LocationDetails(int id)
        {
            var result = this.Data.Locations.Find(id);

            return View( result);
        }
    }
}