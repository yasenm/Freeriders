namespace FreeRiders.Web.Areas.Administration.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using FreeRiders.Models;

    public class AdminLocationModel
    {
        public Expression<Func<Location, AdminLocationModel>> FromLocation
        {
            get
            {
                return location => new AdminLocationModel
                {
                    ID = location.ID,
                    Name = location.Name,
                    Description = location.Description,
                    Picture = location.Picture,
                    Category = location.Category,
                    Albums = location.Albums,
                    Reviews = location.Reviews,
                };
            }
        }

        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual Picture Picture { get; set; }

        public virtual LocationCategory Category { get; set; }

        public ICollection<Album> Albums { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}