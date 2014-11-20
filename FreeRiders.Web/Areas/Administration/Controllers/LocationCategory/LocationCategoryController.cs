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
            : base(data)
        {
        }

        // GET: Administration/LocationCategory
        public ActionResult Index()
        {
            var locationCategories = this.Data
                .LocationCategories
                .All()
                .ToList();

            return this.View(locationCategories);
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

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(string categoryName)
        {
            var category = this.Data.LocationCategories.All().FirstOrDefault(c => c.Name == categoryName);
            var locations = this.Data.Locations.All().Where(l => l.CategoryID == category.ID).ToList();

            foreach (var location in locations)
            {
                var albums = location.Albums;

                foreach (var album in albums)
                {
                    this.Data.Albums.Delete(album);
                }

                this.Data.SaveChanges();
                this.Data.Locations.Delete(location.ID);
                this.Data.SaveChanges();
            }

            this.Data.AlbumCategories.Delete(category.ID);
            this.Data.SaveChanges();
            return this.Index();
        }
    }
}