namespace FreeRiders.Web.Controllers
{
    using System.Web;
    using System.Web.Mvc;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using FreeRiders.Models;
    using FreeRiders.Web.ViewModels.Review;
    using FreeRiders.Data.UnitsOfWork;

    public class ReviewController : AuthorizeUserController
    {
        public ReviewController(IFreeRidersData data)
            : base(data)
        {
        }

        [AllowAnonymous]
        public ActionResult ReviewsForAlbum(int albumID)
        {
            var reviews = this.Data.Reviews
                .All()
                .Where(m => m.AlbumID == albumID)
                .OrderByDescending(m => m.CreatedOn)
                .Project()
                .To<ReviewCollectionViewModel>()
                .ToList();

            this.ViewBag.AlbumID = albumID;

            return this.PartialView("ReviewsForAlbum", reviews);
        }

        [HttpGet]
        public ActionResult Create(int albumID)
        {
            var model = new ReviewCreateModel();

            return View(model);
        }
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReviewCreateModel reviewModel)
        {
            if (this.ModelState.IsValid && reviewModel != null)
            {
                var dbReview = Mapper.Map<Review>(reviewModel);
                dbReview.AuthorID = this.GetUserId();

                this.Data.Reviews.Add(dbReview);
                this.Data.SaveChanges();

                return this.ReviewsForAlbum(dbReview.AlbumID);
            }

            throw new HttpException(404, "Review could not be created");
        }
    }
}