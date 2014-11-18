namespace FreeRiders.Web.ViewModels.Event
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;

    using FreeRiders.Models;
    using FreeRiders.Web.Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class EventsIndexViewModel : IHaveCustomMappings
    {
        public int ID { get; set; }

        public string Title { get; set; }

        [UIHint("Description300Symbols")]
        public string Description { get; set; }

        public string CreatorName { get; set; }

        public int LocationID { get; set; }

        public string LocationName { get; set; }

        public DateTime DateOfEvent { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Event, EventsIndexViewModel>()
                .ForMember(m => m.CreatorName, opt => opt.MapFrom(ev => ev.Creator.UserName))
                .ForMember(m => m.LocationName, opt => opt.MapFrom(ev => ev.Location.Name));
        }
    }
}