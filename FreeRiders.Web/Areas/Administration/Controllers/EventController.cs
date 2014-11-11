namespace FreeRiders.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using FreeRiders.Web.Controllers;
    using FreeRiders.Data.UnitsOfWork;

    public class EventController : AdminController
    {
        public EventController(IFreeRidersData data)
            :base(data)
        {
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}