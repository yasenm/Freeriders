﻿namespace FreeRiders.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using FreeRiders.Data.UnitsOfWork;
    using FreeRiders.Models;
    using FreeRiders.Web.ViewModels.Message;

    public class MessageController : AuthorizeUserController
    {
        public MessageController(IFreeRidersData data)
            : base(data)
        {
        }

        public ActionResult MessagesOfEvent(int eventID)
        {
            var messages = this.Data.Messages
                .All()
                .Where(m => m.EventID == eventID)
                .OrderByDescending(m => m.CreatedOn)
                .Project()
                .To<MessageViewModel>()
                .ToList() ;

            ViewBag.EventID = eventID;

            return PartialView("MessagesForEvent", messages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MessageViewModel message)
        {
            if (message != null && ModelState.IsValid)
            {
                var dbMessage = Mapper.Map<Message>(message);
                dbMessage.Author = this.CurrentUser;
                var ev = this.Data.Events.GetById(message.EventID);

                if (ev == null)
                {
                    throw new HttpException(404, "Event was not found");
                }

                ev.Messages.Add(dbMessage);
                this.Data.SaveChanges();

                return this.MessagesOfEvent(message.EventID);
            }

            throw new HttpException(400, "Invalid message");
        }
    }
}