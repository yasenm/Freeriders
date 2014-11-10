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
                    Picture = a.Picture,
                };
            }
        }

        public int ID { get; set; }

        public string Title { get; set; }

        public virtual Picture Picture { get; set; }
    }
}