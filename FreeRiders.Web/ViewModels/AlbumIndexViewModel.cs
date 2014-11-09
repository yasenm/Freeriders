namespace FreeRiders.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using FreeRiders.Models;

    public class AlbumIndexViewModel
    {
        public static Expression<Func<Album, AlbumIndexViewModel>> FromAlbum
        {
            get
            {
                return a => new AlbumIndexViewModel
                {
                    ID = a.ID,
                    Title = a.Title,
                    Description = a.Description,
                    Category = a.Category,
                    Picture = a.Picture,
                    Reviews = a.Reviews,
                    Pictures = a.Pictures,
                    Location = a.Location,
                };
            }
        }

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