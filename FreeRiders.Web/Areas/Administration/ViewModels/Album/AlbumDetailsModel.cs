namespace FreeRiders.Web.Areas.Administration.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using FreeRiders.Models;

    public class AlbumDetailsModel
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Location Location { get; set; }

        public virtual AlbumCategory Category { get; set; }

        public virtual Picture Picture { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<Picture> Pictures { get; set; }
    }
}