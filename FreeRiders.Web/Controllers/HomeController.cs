namespace FreeRiders.Web.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using FreeRiders.Models;
    using FreeRiders.Web.Infrastructure;
    using FreeRiders.Web.ViewModels;
    using FreeRiders.Data.UnitsOfWork;

    public class HomeController : BaseController
    {
        public HomeController(IFreeRidersData data)
            :base(data)
        {
        }

        public ActionResult Index()
        {
            var locations = this.Data.Locations
                .All()
                .Select(LocationViewModel.FromLocation)
                .ToList();

            return View(locations);
        }

        public ActionResult About()
        {
            ViewBag.Message = this.CurrentUser.UserName;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        // GET: /Album/Create
        public ActionResult Create()
        {
            ViewBag.ErrorMsg = "Something went wrong!";

            return View();
        }

        // POST: /Album/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? id)
        {
            if (ModelState.IsValid)
            {
                if (null != Request.Files["NewImageName"])
                {
                    HttpPostedFileBase file = Request.Files["NewImageName"];
                    int fileLen = file.ContentLength;
                    if (fileLen > 0)
                    {
                        // For pictures of albums and covers in imgur
                        using (var memory = new MemoryStream())
                        {
                            file.InputStream.CopyTo(memory);
                            var image = memory.GetBuffer();
                            var url = ImageUploader.UploadImageToImgur(image);

                            if (url != "error")
                            {
                                var uploadedPicture = new Picture
                                {
                                    DateCreated = DateTime.Now,
                                    ImageUrl = url,
                                };

                                this.Data.Pictures.Add(uploadedPicture);
                                this.Data.SaveChanges();
                                return RedirectToAction("Index");
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("Create");
                    }
                }
            }

            return View();
        }
    }
}