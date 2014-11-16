namespace FreeRiders.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using FreeRiders.Data.UnitsOfWork;
    using FreeRiders.Models;
    using FreeRiders.Web.Areas.Administration.Controllers.Base;
    using FreeRiders.Web.Areas.Administration.ViewModels;
    using FreeRiders.Web.Controllers;

    using Model = FreeRiders.Models.Message;
    using ViewModel = FreeRiders.Web.Areas.Administration.ViewModels.Message.MessageAdminViewModel;

    public class MessageController : KendoGridAdministrationController
    {
        public MessageController(IFreeRidersData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        protected override IQueryable GetData()
        {
            return this.Data.Messages
                .All()
                .Project()
                .To<ViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Messages.GetById(id) as T;
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            var dbModel = base.Create<Model>(model);

            if (dbModel != null)
            {
                model.ID = dbModel.ID;
            }

            return this.GridOperation(model, request);
        }


        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            base.Update<Model, ViewModel>(model, model.ID);
            return this.GridOperation(model, request);
        }


        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                this.Data.Events.Delete(model.ID);
                this.Data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }
    }
}