
namespace FreeRiders.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using FreeRiders.Data.UnitsOfWork;
    using FreeRiders.Web.ViewModels.User;
    using FreeRiders.Web.ViewModels.Event;

    public class UserController : AuthorizeUserController
    {
        public UserController(IFreeRidersData data)
            :base(data)
        {
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

            return View(user);
        }
    }
}