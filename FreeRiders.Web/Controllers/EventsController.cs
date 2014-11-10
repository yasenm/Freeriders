using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreeRiders.Web.Controllers
{
    public class EventsController : Controller
    {
        // GET: Event
        public ActionResult Index()
        {
            return View();
        }
    }
}