namespace FreeRiders.Web.Areas.Administration.ViewModels
{
    using System;
    using System.Linq.Expressions;

    using FreeRiders.Models;

    public class InputAlbumCategoryModel
    {
        public static Expression<Func<AlbumCategory, InputAlbumCategoryModel>> FromAlbumCategory
        {
            get
            {
                return ac => new InputAlbumCategoryModel
                {
                    Name = ac.Name,
                };
            }
        }

        public string Name { get; set; }
    }
}