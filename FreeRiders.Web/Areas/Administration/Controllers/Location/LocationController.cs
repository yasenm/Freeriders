﻿namespace FreeRiders.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using FreeRiders.Data.UnitsOfWork;
    using FreeRiders.Web.Areas.Administration.ViewModels;
    using FreeRiders.Web.Controllers;
    using FreeRiders.Web.Infrastructure;
    using FreeRiders.Web.Infrastructure.Services;

    public class LocationController : AdminController
    {
        private DDLServices ddlServices;
        private const string DefaultPictureUrl = "http://i.imgur.com/dQZEDmZ.jpg";

        public LocationController(IFreeRidersData data, DDLServices ddlServices)
            : base(data)
        {
            this.ddlServices = ddlServices;
        }

        // GET: Administration/Location
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Categories = ddlServices.LocationsCategoriesDDL;

            var location = new InputLocationModel();
            return View(location);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InputLocationModel location)
        {
            if (ModelState.IsValid)
            {
                var picture = ImageUploader.SavePictureInDb(location.Picture, this.Data);
                var newLocation = location.GetLocationForDb();
                newLocation.PictureID = picture.ID;

                this.Data.Locations.Add(newLocation);
                this.Data.SaveChanges();

                return Redirect("~/");
            }

            return View();
        }
    }
}