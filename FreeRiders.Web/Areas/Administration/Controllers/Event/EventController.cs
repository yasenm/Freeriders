namespace FreeRiders.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Kendo.Mvc.UI;

    using FreeRiders.Data.UnitsOfWork;
    using FreeRiders.Web.Areas.Administration.Controllers.Base;
    using FreeRiders.Web.Areas.Administration.ViewModels;

    using Model = FreeRiders.Models.Event;
    using ViewModel = FreeRiders.Web.Areas.Administration.ViewModels.AdminEventViewModel;

    public class EventController : KendoGridAdministrationController
    {
        public EventController(IFreeRidersData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return this.View();
        }

        protected override IQueryable GetData()
        {
            return this.Data.Events.All().Project().To<AdminEventViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Events.GetById(id) as T;
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