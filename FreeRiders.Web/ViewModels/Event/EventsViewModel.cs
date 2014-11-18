namespace FreeRiders.Web.ViewModels.Event
{
    using System;

    using FreeRiders.Models;
    using FreeRiders.Web.Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class EventsViewModel : IMapFrom<Event>
    {
        public int ID { get; set; }

        [UIHint("Description100Symbols")]
        public string Description { get; set; }

        public string Title { get; set; }

        public DateTime DateOfEvent { get; set; }
    }
}