namespace FreeRiders.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using FreeRiders.Data.UnitsOfWork;
    using FreeRiders.Models;
    using FreeRiders.Web.Controllers;
    using FreeRiders.Web.Areas.Administration.ViewModels;

    public class LocationCategoryController : AdminController
    {
        public LocationCategoryController(IFreeRidersData data)
            :base(data)
        {
        }

        // GET: Administration/LocationCategory
        public ActionResult Index()
        {
            var locationCategories = this.Data
                .LocationCategories
                .All()
                .ToList();

            return View(locationCategories);
        }

        [HttpPost]
        public ActionResult Create(InputLocationCategoryModel inputCategory)
        {
            var newCategory = new LocationCategory
            {
                Name = inputCategory.Name,
            };

            this.Data.LocationCategories.Add(newCategory);
            this.Data.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}