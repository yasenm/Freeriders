namespace FreeRiders.Web.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using FreeRiders.Data.UnitsOfWork;
    using FreeRiders.Models;
    using FreeRiders.Web.Infrastructure;
    using FreeRiders.Web.ViewModels;
    using FreeRiders.Web.ViewModels.Home;
    using FreeRiders.Web.ViewModels.Event;
    using FreeRiders.Web.ViewModels.Album;

    public class HomeController : BaseController
    {
        public HomeController(IFreeRidersData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var indexViewResult = new HomeViewModel();

            indexViewResult.Albums = this.Data.Albums
                .All()
                .AsQueryable()
                .OrderByDescending(a => a.Rating)
                .Take(10)
                .Project()
                .To<AlbumIndexViewModel>()
                .ToList();

            indexViewResult.Locations = this.Data.Locations
                .All()
                .AsQueryable()
                .OrderByDescending(a => a.Rating)
                .Take(10)
                .Project()
                .To<LocationViewModel>()
                .ToList();

            indexViewResult.Events = this.Data.Events
                .All()
                .AsQueryable()
                .OrderByDescending(ev => ev.DateOfEvent)
                .Take(10)
                .Project()
                .To<EventsViewModel>()
                .ToList();

            return this.View(indexViewResult);
        }

        public ActionResult Error()
        {
            return this.View();
        }
    }
}