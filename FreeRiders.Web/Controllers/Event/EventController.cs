namespace FreeRiders.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using FreeRiders.Data.UnitsOfWork;
    using FreeRiders.Web.ViewModels.Event;
    using FreeRiders.Web.ViewModels.User;
    using System.Web;
    using AutoMapper;
    using FreeRiders.Models;
    using FreeRiders.Web.Infrastructure.Services;

    public class EventController : AuthorizeUserController
    {
        private DDLServices ddlServices;

        public EventController(IFreeRidersData data, DDLServices ddlServices)
            : base(data)
        {
            this.ddlServices = ddlServices;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var eventForView = this.Data
                .Events
                .All()
                .OrderByDescending(ev => ev.DateOfEvent)
                .Project()
                .To<EventsIndexViewModel>()
                .ToList();

            return this.View(eventForView);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var currentEvent = this.Data.Events
                .All()
                .Where(ev => ev.ID == id)
                .Project()
                .To<EventDetailsViewModel>()
                .FirstOrDefault();

            currentEvent.CreatorName = this.Data.Users
                .GetById(currentEvent.CreatorID)
                .UserName;

            var joinedUSers = this.Data.EventsUsers
                .All()
                .Where(eu => eu.EventID == id)
                .AsQueryable()
                .Project()
                .To<UserBasicViewModel>()
                .ToList();

            currentEvent.Users = joinedUSers;

            return this.View(currentEvent);
        }

        [HttpPost]
        public void Join(int eventID)
        {
            var currentEvent = this.Data.Events.GetById(eventID);

            var eventToUser = new EventsUsers
            {
                Event = currentEvent,
                EventID = eventID,
                UserID = this.GetUserId(),
                User = this.CurrentUser,
            };

            this.Data.EventsUsers.Add(eventToUser);
            this.Data.SaveChanges();

            var evnts = this.Data.EventsUsers.All().Where(etu => etu.EventID == currentEvent.ID).ToList();

            var something = evnts[0];
            this.Data.SaveChanges();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new EventDetailsViewModel();
            this.ViewBag.Categories = this.ddlServices.LocationsDDL;

            return this.View(model);
        }

        [HttpPost]
        public ActionResult Create(EventDetailsViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var dbEvent = Mapper.Map<Event>(model);
                dbEvent.Creator = this.CurrentUser;
                dbEvent.CreatorID = this.CurrentUser.Id;
                dbEvent.JoinedUsers.Add(this.CurrentUser);

                this.Data.Events.Add(dbEvent);
                this.Data.SaveChanges();

                return this.RedirectToAction("Index", "Event", new { Area = string.Empty });
            }

            throw new HttpException(404, "Event could not be created");
        }
    }
}