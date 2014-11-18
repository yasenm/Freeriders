namespace FreeRiders.Web.ViewModels.Album
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using System.Web;

    using FreeRiders.Models;
    using FreeRiders.Web.Infrastructure.Mapping;

    public class AlbumCreateViewModel : IMapFrom<Album>
    {
        [UIHint("CustomSingleLineText")]
        public string Title { get; set; }

        [UIHint("CustomMultiLineText")]
        public string Description { get; set; }

        public int LocationID { get; set; }

        public int CategoryID { get; set; }

        public HttpPostedFileBase PicturePosted { get; set; }

        public ICollection<HttpPostedFileBase> PicturesPosted { get; set; }
    }
}