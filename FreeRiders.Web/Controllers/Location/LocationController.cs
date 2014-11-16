namespace FreeRiders.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using FreeRiders.Data.UnitsOfWork;
    using FreeRiders.Web.ViewModels;
using System.Collections.Generic;

    public class LocationController : BaseController
    {
        public LocationController(IFreeRidersData data)
            :base(data)
        {
        }

        public ActionResult Index()
        {
            var locationsForIndex = this.Data
                .Locations
                .All()
                .AsQueryable()
                .Project()
                .To<LocationViewModel>()
                .ToList();

            return View(locationsForIndex);
        }

        public ActionResult LocationDetails(int id)
        {
            var result = this.Data.Locations.GetById(id);

            return View(result);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult RenderLocationsHome(ICollection<LocationViewModel> collection)
        {
            return this.PartialView("LocationsHome", collection);
        }
    }
}