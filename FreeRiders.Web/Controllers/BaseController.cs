namespace FreeRiders.Web.Controllers
{
    using System.Configuration;
    using System.Web.Mvc;
    using System.Threading;

    using Microsoft.AspNet.Identity;

    using FreeRiders.Data;
    using FreeRiders.Data.UnitsOfWork;
    using FreeRiders.Models;

    public abstract class BaseController : Controller
    {
        protected User CurrentUser
        {
            get
            {
                return this.Data.Users.GetById(this.GetUserId());
            }
        }

        protected IFreeRidersData Data;

        protected string FlickrAuth = ConfigurationManager.AppSettings["FlickrAuthtoken"];

        public BaseController(IFreeRidersData data)
        {
            this.Data = data;
        }

        private string GetUserId()
        {
            return Thread.CurrentPrincipal.Identity.GetUserId();
        }
    }
}