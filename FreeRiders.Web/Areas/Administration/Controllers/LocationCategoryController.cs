namespace FreeRiders.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using FreeRiders.Web.Controllers;
    using FreeRiders.Web.Areas.Administration.ViewModels;
    using FreeRiders.Models;

    public class LocationCategoryController : AuthorizeUserController
    {
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