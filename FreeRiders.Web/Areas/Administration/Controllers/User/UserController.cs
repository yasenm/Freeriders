namespace FreeRiders.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using FreeRiders.Data.UnitsOfWork;
    using FreeRiders.Web.Controllers;
    using FreeRiders.Web.Areas.Administration.Controllers.Base;
    using System.Linq;
    using FreeRiders.Web.Areas.Administration.ViewModels.User;
    using Kendo.Mvc.UI;

    using Model = FreeRiders.Models.User;
    using ViewModel = FreeRiders.Web.Areas.Administration.ViewModels.User.UserAdminViewModel;
    using FreeRiders.Data.Common;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class UserController : KendoGridAdministrationController
    {
        public UserController(IFreeRidersData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        protected override IQueryable GetData()
        {
            var users = this.Data.Users
                .All()
                .Where(u => u.Roles.Any(x => x.RoleId != "144d5374-608b-4aa0-8a9e-005a5c1fd4f7"))
                .Project()
                .To<UserAdminViewModel>();

            //var users1 = this.Data
            //   .Users
            //   .All().Join(this.Data.Roles.All(),
            //   user => user.Roles.FirstOrDefault().RoleId,
            //   role => role.Id,
            //   (user, role) =>
            //       new UserViewModel()
            //       {
            //           FirstName = user.FirstName,
            //           MiddleName = user.MiddleName,
            //           LastName = user.LastName,
            //           Age = user.Age,
            //           PersonalNumber = user.PersonalNumber,
            //           Id = user.Id,
            //           Role = role.Name
            //       }
            //   ).ToDataSourceResult(request, ModelState);

            return users;
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Users.GetById(id) as T;
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            var dbModel = this.GetById<Model>(model.Id);

            this.Data.Users.Update(dbModel);
            this.Data.SaveChanges();

            return this.GridOperation(model, request);
        }
    }
}