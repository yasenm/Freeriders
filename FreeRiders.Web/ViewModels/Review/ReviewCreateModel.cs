namespace FreeRiders.Web.ViewModels.Review
{
    using FreeRiders.Models;
    using FreeRiders.Web.Infrastructure.Mapping;

    public class ReviewCreateModel : IMapFrom<Review>
    {
        public string Content { get; set; }

        public string AuthorID { get; set; }

        public int AlbumID { get; set; }

        public double Stars { get; set; }
    }
}