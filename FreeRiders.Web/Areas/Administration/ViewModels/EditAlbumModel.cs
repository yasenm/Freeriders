namespace FreeRiders.Web.Areas.Administration.ViewModels
{
    using System.Collections.Generic;
    using System.Web;

    using FreeRiders.Models;
    using System.Linq.Expressions;
    using System;

    public class EditAlbumModel
    {
        public static Expression<Func<Album, EditAlbumModel>> FromAlbum
        {
            get
            {
                return a => new EditAlbumModel
                {
                    ID = a.ID,
                    Title = a.Title,
                    Description = a.Description,
                    CategoryID = a.CategoryID,
                    CurrentPictures = a.Pictures
                };
            }
        }

        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int LocationID { get; set; }

        public int CategoryID { get; set; }

        public ICollection<HttpPostedFileBase> Pictures { get; set; }

        public ICollection<Picture> CurrentPictures { get; set; }

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