namespace FreeRiders.Web.Areas.Administration.ViewModels
{
    using System;
    using System.Linq.Expressions;

    using FreeRiders.Models;

    public class LocationCategoryModel
    {
        public static Expression<Func<LocationCategory, LocationCategoryModel>>
            FromLocationCategory
        {
            get
            {
                return cat => new LocationCategoryModel
                {
                    ID = cat.ID,
                    Text = cat.Name,
                };
            }
        }

        public int ID { get; set; }

        public string Text { get; set; }
    }
}