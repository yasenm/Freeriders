namespace FreeRiders.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using FreeRiders.Web.Areas.Administration.ViewModels;
    using FreeRiders.Web.Controllers;
    using FreeRiders.Web.Infrastructure;

    public class AlbumController : AuthorizeUserController
    {
        public ActionResult Index()
        {
            var albumsResult = this.Data.Albums
                .All()
                .Select(AdminAlbumModel.FromAlbum)
                .ToList();

            return View(albumsResult);
        }

        [HttpGet]
        public ActionResult AlbumDetails(int id)
        {
            var result = this.Data.Albums
                .All()
                .Where(a => a.ID == id)
                .Select(AlbumDetailsModel.FromAlbum)
                .FirstOrDefault();

            if (result != null)
            {
                return View(result);
            }
            else
            {
                return RedirectToAction("Index", "Album", new { area = "Administration" });
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            var locations = this.Data
                .Locations
                .All()
                .ToList();

            List<SelectListItem> ddlLocations = new List<SelectListItem>();
            ddlLocations.AddRange(new SelectList(locations, "ID", "Name"));

            var albumCategories = this.Data
                .AlbumCategories
                .All()
                .ToList();

            List<SelectListItem> ddlCategories = new List<SelectListItem>();
            ddlCategories.AddRange(new SelectList(albumCategories, "ID", "Name"));

            ViewBag.Locations = new SelectList(ddlLocations, "Value", "Text");
            ViewBag.Categories = new SelectList(ddlCategories, "Value", "Text");

            var album = new InputAlbumModel();
            return View(album);
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

                this.Data.Albums.Add(newAlbum);
                this.Data.SaveChanges();
                
                var location = this.Data.Locations.Find(album.LocationID);
                location.Albums.Add(newAlbum);
                this.Data.Locations.Update(location);
                this.Data.SaveChanges();

                return RedirectToAction("Index", "Album", new { area = "Administration" });
            }

            return View();
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
                return View(album);
            }
            else
            {
                return RedirectToAction("Index", "Album", new { area = "Administration" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditAlbumModel album)
        {
            if (ModelState.IsValid)
            {
                var albumToUpdate = this.Data.Albums.Find(id);
                foreach (var picture in album.Pictures)
                {
                   var albumPicture = ImageUploader.SavePictureInDb(picture, this.Data);
                   albumToUpdate.Pictures.Add(albumPicture);
                }

                this.Data.SaveChanges();
                return RedirectToAction("Index", "Album", new { area = "Administration" });
            }

            return View();
        }
    }
}