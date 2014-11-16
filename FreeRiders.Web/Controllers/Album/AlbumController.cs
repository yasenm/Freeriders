namespace FreeRiders.Web.Controllers
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
            var albumResult = this.Data.Albums.All().FirstOrDefault(a => a.ID == id);

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
    }
}