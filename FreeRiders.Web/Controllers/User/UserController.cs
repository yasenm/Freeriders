
namespace FreeRiders.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using FreeRiders.Data.UnitsOfWork;
    using FreeRiders.Web.ViewModels.User;
    using FreeRiders.Web.ViewModels.Event;
    using FreeRiders.Web.Infrastructure;
    using FreeRiders.Web.ViewModels.Album;

    public class UserController : AuthorizeUserController
    {
        public UserController(IFreeRidersData data)
            : base(data)
        {
        }

        public ActionResult Avatar(string username)
        {
            var user = this.Data.Users
                .All()
                .FirstOrDefault(u => u.UserName == username);

            var avatar = user.Avatar;

            return this.File(avatar, "image/jpeg");
        }

        [HttpGet]
        public ActionResult PostAvatar(string username)
        {
            var model = new UserProfileViewModel
            {
                UserName = username,
            };

            return this.PartialView("PostAvatarForUser", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostAvatar(UserProfileViewModel userModel)
        {
            HttpPostedFileBase file = Request.Files[0];

            if (userModel.PostedAvatar != null)
            {
                ImageUploader.UploadAvatarToUser(userModel.PostedAvatar, this.CurrentUser, this.Data);
            }

            return this.RedirectToAction("Profile", "User", new { Area = string.Empty, username = this.CurrentUser.UserName });
        }

        public ActionResult Profile(string username)
        {
            var user = this.Data.Users
                .All()
                .Where(u => u.UserName == username)
                .Project()
                .To<UserProfileViewModel>()
                .FirstOrDefault();

            user.EventsCreated = this.Data.Events
                .All()
                .AsQueryable()
                .Where(ev => ev.CreatorID == user.Id)
                .Project()
                .To<EventsViewModel>()
                .ToList();

            user.EventsJoined = this.Data.EventsUsers
                .All()
                .Where(eu => eu.User.UserName == username)
                .AsQueryable()
                .Project()
                .To<EventUserProfileViewModel>()
                .ToList();

            user.AlbumsCreated = this.Data.Albums
                .All()
                .Where(a => a.CreatorID == user.Id)
                .AsQueryable()
                .Project()
                .To<AlbumIndexViewModel>()
                .ToList();

            return this.View(user);
        }
    }
}