namespace FreeRiders.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using FreeRiders.Data.UnitsOfWork;
    using FreeRiders.Web.ViewModels.Event;

    public class EventController : BaseController
    {
        public EventController(IFreeRidersData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var eventForView = this.Data
                .Events
                .All()
                .OrderByDescending(ev => ev.DateOfEvent)
                .Project()
                .To<EventsIndexViewModel>()
                .ToList();

            return View(eventForView);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            

            return View();
        }
    }
}