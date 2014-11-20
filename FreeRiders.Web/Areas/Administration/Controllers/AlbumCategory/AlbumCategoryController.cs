namespace FreeRiders.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using FreeRiders.Data.UnitsOfWork;
    using FreeRiders.Models;
    using FreeRiders.Web.Areas.Administration.ViewModels;
    using FreeRiders.Web.Controllers;

    public class AlbumCategoryController : AdminController
    {
        public AlbumCategoryController(IFreeRidersData data)
            : base(data)
        {
        }

        // GET: Administration/AlbumCategory
        public ActionResult Index()
        {
            var categories = this.Data.AlbumCategories
                .All()
                .Select(InputAlbumCategoryModel.FromAlbumCategory)
                .ToList();

            return this.View(categories);
        }

        [HttpPost]
        public ActionResult Create(InputAlbumCategoryModel albumCategory)
        {
            var newAlbumCategory = new AlbumCategory
            {
                Name = albumCategory.Name,
            };

            this.Data.AlbumCategories.Add(newAlbumCategory);
            this.Data.SaveChanges();

            return this.Redirect("Index");
        }

        [HttpPost]
        public ActionResult Delete(string categoryName)
        {
            var category = this.Data.AlbumCategories.All().FirstOrDefault(c => c.Name == categoryName);
            var albums = this.Data.Albums.All().Where(a => a.CategoryID == category.ID).ToList();

            foreach (var album in albums)
            {
                this.Data.Albums.Delete(album);
            }

            this.Data.SaveChanges();

            this.Data.AlbumCategories.Delete(category);
            this.Data.SaveChanges();
            return this.Index();
        }
    }
}