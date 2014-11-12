namespace FreeRiders.Web.ViewModels.Event
{
    using System;

    using FreeRiders.Models;
    using FreeRiders.Web.Infrastructure.Mapping;

    public class EventsViewModel : IMapFrom<Event>
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public DateTime DateOfEvent { get; set; }
    }
}