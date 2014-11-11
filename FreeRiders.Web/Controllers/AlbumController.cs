namespace FreeRiders.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using FreeRiders.Data.UnitsOfWork;
    using FreeRiders.Web.ViewModels;

    public class AlbumController : BaseController
    {
        public AlbumController(IFreeRidersData data)
            : base(data)
        {
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
        public ActionResult AlbumDetails(int id)
        {
            var albumResult = this.Data.Albums.All().FirstOrDefault(a => a.ID ==id);

            return View(albumResult);
        }
    }
}