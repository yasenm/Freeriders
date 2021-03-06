﻿namespace FreeRiders.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Recaptcha.Web;
    using Recaptcha.Web.Mvc;

    using FreeRiders.Data.UnitsOfWork;
    using FreeRiders.Models;
    using FreeRiders.Web.ViewModels.Message;
    using System.Configuration;

    public class MessageController : AuthorizeUserController
    {
        public MessageController(IFreeRidersData data)
            : base(data)
        {
        }

        [AllowAnonymous]
        public ActionResult MessagesOfEvent(int eventID)
        {
            var messages = this.Data.Messages
                .All()
                .Where(m => m.EventID == eventID)
                .OrderByDescending(m => m.CreatedOn)
                .Project()
                .To<MessageViewModel>()
                .ToList();

            this.ViewBag.EventID = eventID;

            return this.PartialView("MessagesForEvent", messages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MessageViewModel message)
        {
            var privateKey = ConfigurationManager.AppSettings;
            RecaptchaVerificationHelper recaptchaHelper = this.GetRecaptchaVerificationHelper("6LeNDv8SAAAAAIz_P8n62F45Iw-CV7KIPZFlXVRH");

            if (String.IsNullOrEmpty(recaptchaHelper.Response))
            {
                this.ModelState.AddModelError("Captcha", "Invalid captcha");
            }

            RecaptchaVerificationResult recaptchaResult = recaptchaHelper.VerifyRecaptchaResponse();

            if (recaptchaResult != RecaptchaVerificationResult.Success)
            {
                ModelState.AddModelError("", "Incorrect captcha answer.");
            }

            if (message != null && this.ModelState.IsValid)
            {
                var dbMessage = Mapper.Map<Message>(message);
                dbMessage.AuthorID = this.GetUserId();
                var ev = this.Data.Events.GetById(message.EventID);

                if (ev == null)
                {
                    throw new HttpException(404, "Event was not found");
                }

                this.Data.Messages.Add(dbMessage);
                this.Data.SaveChanges();

                return this.MessagesOfEvent(message.EventID);
            }

            throw new HttpException(400, "Invalid message");
        }
    }
}