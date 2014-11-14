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
            :base(data)
        {
        }
        
        // GET: Administration/AlbumCategory
        public ActionResult Index()
        {
            var categories = this.Data.AlbumCategories
                .All()
                .Select(InputAlbumCategoryModel.FromAlbumCategory)
                .ToList();

            return View(categories);
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

            return Redirect("Index");
        }
    }
}