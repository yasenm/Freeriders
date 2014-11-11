namespace FreeRiders.Web.Areas.Administration.ViewModels
{
    using System.Collections.Generic;

    using FreeRiders.Models;
    using FreeRiders.Web.Infrastructure.Mapping;

    public class AdminAlbumModel : IMapFrom<Album>
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public virtual AlbumCategory Category { get; set; }

        public virtual Picture Picture { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<Picture> Pictures { get; set; }
    }
}