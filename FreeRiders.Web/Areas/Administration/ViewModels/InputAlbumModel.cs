namespace FreeRiders.Web.Areas.Administration.ViewModels
{
    using System.Web;

    using FreeRiders.Models;

    public class InputAlbumModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int LocationID { get; set; }

        public int CategoryID { get; set; }

        public HttpPostedFileBase Picture { get; set; }

        public Album GetAlbumForDb()
        {
            var result = new Album
            {
                Title = this.Title,
                Description = this.Description,
                CategoryID = this.CategoryID,
            };

            return result;
        }
    }
}