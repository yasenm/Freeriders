namespace FreeRiders.Web.ViewModels
{
    using System;
    using System.Linq.Expressions;

    using FreeRiders.Models;

    public class LocationViewModel
    {
        public static Expression<Func<Location, LocationViewModel>> FromLocation
        {
            get
            {
                return location => new LocationViewModel
                {
                    Name = location.Name,
                    Latitude = location.Latitude,
                    Longitude = location.Longitude,
                    Description = location.Description,
                    Category = location.Category,
                    Picture = location.Picture,
                };
            }
        }

        public string Name { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Description { get; set; }

        public LocationCategory Category { get; set; }

        public Picture Picture { get; set; }
    }
}