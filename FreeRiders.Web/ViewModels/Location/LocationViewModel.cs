namespace FreeRiders.Web.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    using FreeRiders.Models;
    using FreeRiders.Web.Infrastructure.Mapping;

    public class LocationViewModel : IMapFrom<Location>
    {
        public int ID { get; set; }

        public string Name { get; set; }

        [UIHint("Description100Symbols")]
        public string Description { get; set; }

        public Picture Picture { get; set; }
    }
}