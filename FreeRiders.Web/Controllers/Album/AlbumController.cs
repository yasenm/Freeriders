﻿namespace FreeRiders.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using FreeRiders.Data.UnitsOfWork;
    using FreeRiders.Web.ViewModels.Album;
    using FreeRiders.Web.Infrastructure.Services;
    using FreeRiders.Models;
    using FreeRiders.Web.Infrastructure;
    using FreeRiders.Data.Common;
    using System.Web;

    public class AlbumController : BaseController
    {
        private DDLServices ddlServices;

        public AlbumController(IFreeRidersData data, DDLServices ddlServices)
            : base(data)
        {
            this.ddlServices = ddlServices;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var viewResult = this.Data.Albums
                .All()
                .AsQueryable()
                .Project()
                .To<AlbumIndexViewModel>()
                .ToList();

            return View(viewResult);
        }

        [HttpGet]
        [Authorize]
        public ActionResult AlbumDetails(int id)
        {
            var albumResult = this.Data.Albums
                .All()
                .Where(a => a.ID == id)
                .Project()
                .To<EditAlbumViewModel>()
                .FirstOrDefault();

            return View(albumResult);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AllRolesExceptRegularUser)]
        public ActionResult Create()
        {
            var albumModel = new AlbumCreateViewModel();

            ViewBag.Locations = this.ddlServices.LocationsDDL;
            ViewBag.Categories = this.ddlServices.AlbumsCategoriesDDL;

            return View(albumModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = GlobalConstants.AllRolesExceptRegularUser)]
        public ActionResult Create(AlbumCreateViewModel albumModel)
        {
            if (albumModel != null && ModelState.IsValid)
            {
                var picture = ImageUploader.SavePictureInDb(albumModel.PicturePosted, this.Data);
                var dbAlbum = Mapper.Map<Album>(albumModel);
                dbAlbum.CreatorID = this.GetUserId();
                dbAlbum.Picture = picture;
                dbAlbum.PictureID = picture.ID;

                foreach (var postedPicture in albumModel.PicturesPosted)
                {
                    var albumPicture = ImageUploader.SavePictureInDb(postedPicture, this.Data);
                    dbAlbum.Pictures.Add(albumPicture);
                }

                this.Data.Albums.Add(dbAlbum);
                this.Data.SaveChanges();

                return RedirectToAction("AlbumDetails", "Album", new { area = string.Empty, id = dbAlbum.ID });
            }

            return View(albumModel);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var album = this.Data.Albums
                .All()
                .Where(a => a.ID == id)
                .Project()
                .To<EditAlbumViewModel>()
                .FirstOrDefault();

            ViewBag.Locations = this.ddlServices.LocationsDDL;
            ViewBag.AlbumCategories = this.ddlServices.AlbumsCategoriesDDL;

            if (album.CreatorID != this.CurrentUser.Id)
            {
                throw new HttpException(404, "Forbiden address you are not the author of the album");
            }

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
        public ActionResult Edit(int id, EditAlbumViewModel album)
        {
            if (album != null)
            {
                var albumToUpdate = this.Data.Albums.GetById(id);

                if (albumToUpdate.CreatorID != this.CurrentUser.Id)
                {
                    throw new HttpException(404, "Forbiden address you are not the author of the album");
                }

                if (album.PicturesAdded != null)
                {
                    foreach (var picture in album.PicturesAdded)
                    {
                        var albumPicture = ImageUploader.SavePictureInDb(picture, this.Data);
                        albumToUpdate.Pictures.Add(albumPicture);
                    }
                }

                albumToUpdate.CategoryID = album.CategoryID;
                albumToUpdate.LocationID = album.LocationID;
                albumToUpdate.Description = album.Description;
                albumToUpdate.Title = album.Title;

                this.Data.SaveChanges();
                return this.RedirectToAction("Details", "Album", new { area = string.Empty, id = album.ID });
            }

            return View();
        }

        [HttpPost]
        public ActionResult DeleteAlbum(int albumID)
        {
            var albumToDelete = this.Data.Albums.GetById(albumID);

            if (albumToDelete.CreatorID != this.CurrentUser.Id)
            {
                throw new HttpException(404, "Forbiden address you are not the author of the album");
            }

            this.Data.Albums.Delete(albumToDelete);
            this.Data.SaveChanges();

            return this.Index();
        }

        [HttpGet]
        [OutputCache(Duration = 10 * 60)]
        public ActionResult LoadPicturesGrid(int albumID)
        {
            var album = this.Data.Albums.GetById(albumID);
            var collection = album.Pictures.Where(p => p.IsDeleted != true).ToList();

            ViewBag.CoverPictureID = album.PictureID;
            ViewBag.AlbumID = albumID;

            return PartialView("EditingAlbumPicturesGrid", collection);
        }

        [HttpPost]
        public ActionResult DeletePictureFromAlbum(int pictureID, int albumID)
        {
            var albumToDelete = this.Data.Albums.GetById(albumID);

            if (albumToDelete.CreatorID != this.CurrentUser.Id)
            {
                throw new HttpException(404, "Forbiden address you are not the author of the album");
            }

            this.Data.Pictures.Delete(pictureID);
            this.Data.SaveChanges();

            return this.LoadPicturesGrid(albumID);
        }

        [HttpPost]
        public ActionResult EditPictureToCover(int pictureID, int albumID)
        {
            var album = this.Data.Albums.GetById(albumID);

            if (album.CreatorID != this.CurrentUser.Id)
            {
                throw new HttpException(404, "Forbiden address you are not the author of the album");
            }

            album.PictureID = pictureID;
            this.Data.SaveChanges();

            return this.LoadPicturesGrid(albumID);
        }
    }
}