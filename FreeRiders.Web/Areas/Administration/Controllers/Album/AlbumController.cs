namespace FreeRiders.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using FreeRiders.Data.UnitsOfWork;
    using FreeRiders.Models;
    using FreeRiders.Web.Areas.Administration.ViewModels;
    using FreeRiders.Web.Controllers;
    using FreeRiders.Web.Infrastructure;
    using FreeRiders.Web.Infrastructure.Services;

    public class AlbumController : AdminController
    {
        private DDLServices ddlServices;

        public AlbumController(IFreeRidersData data, DDLServices ddlServices)
            : base(data)
        {
            this.ddlServices = ddlServices;
        }

        public ActionResult Index()
        {
            var albumsResult = this.Data.Albums
                .All()
                .AsQueryable()
                .Project()
                .To<AdminAlbumModel>()
                .ToList();

            return this.View(albumsResult);
        }

        [HttpGet]
        public ActionResult Create()
        {
            this.ViewBag.Locations = this.ddlServices.LocationsDDL;
            this.ViewBag.Categories = this.ddlServices.AlbumsCategoriesDDL;

            var album = new InputAlbumModel();
            return this.View(album);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InputAlbumModel album)
        {

            if (ModelState.IsValid)
            {
                var picture = ImageUploader.SavePictureInDb(album.Picture, this.Data);
                var newAlbum = album.GetAlbumForDb();
                newAlbum.PictureID = picture.ID;
                newAlbum.Pictures.Add(picture);
                newAlbum.LocationID = album.LocationID;

                this.Data.Albums.Add(newAlbum);
                this.Data.SaveChanges();

                var location = this.Data.Locations.GetById(album.LocationID);
                location.Albums.Add(newAlbum);
                this.Data.Locations.Update(location);
                this.Data.SaveChanges();

                return this.RedirectToAction("Index", "Album", new { area = "Administration" });
            }

            return this.View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var album = this.Data.Albums
                .All()
                .Where(a => a.ID == id)
                .Select(EditAlbumModel.FromAlbum)
                .FirstOrDefault();

            if (album != null)
            {
                return this.View(album);
            }
            else
            {
                return this.RedirectToAction("Index", "Album", new { area = "Administration" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditAlbumModel album)
        {
            if (ModelState.IsValid)
            {
                var albumToUpdate = this.Data.Albums.GetById(id);
                foreach (var picture in album.Pictures)
                {
                    var albumPicture = ImageUploader.SavePictureInDb(picture, this.Data);
                    albumToUpdate.Pictures.Add(albumPicture);
                }

                this.Data.SaveChanges();
                return this.RedirectToAction("Index", "Album", new { area = "Administration" });
            }

            return this.View();
        }

        [HttpPost]
        public ActionResult DeleteAlbum(int albumID)
        {
            this.Data.Albums.Delete(albumID);
            this.Data.SaveChanges();

            return this.Index();
        }

        [HttpGet]
        public ActionResult LoadPicturesGrid(int albumID)
        {
            var album = this.Data.Albums.GetById(albumID);
            var collection = album.Pictures.Where(p => p.IsDeleted != true).ToList();

            this.ViewBag.CoverPictureID = album.PictureID;
            this.ViewBag.AlbumID = albumID;

            return this.PartialView("Album/_PicturesForEdit", collection);
        }

        [HttpPost]
        public ActionResult DeletePictureFromAlbum(int pictureID, int albumID)
        {
            this.Data.Pictures.Delete(pictureID);
            this.Data.SaveChanges();

            return this.LoadPicturesGrid(albumID);
        }

        [HttpPost]
        public ActionResult EditPictureToCover(int pictureID, int albumID)
        {
            var album = this.Data.Albums.GetById(albumID);

            album.PictureID = pictureID;
            this.Data.SaveChanges();

            return this.LoadPicturesGrid(albumID);
        }
    }
}