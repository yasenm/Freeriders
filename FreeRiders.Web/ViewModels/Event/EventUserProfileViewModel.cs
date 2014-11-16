namespace FreeRiders.Web.ViewModels.Event
{
    using System;

    using FreeRiders.Models;
    using FreeRiders.Web.Infrastructure.Mapping;

    public class EventUserProfileViewModel : IMapFrom<EventsUsers>, IHaveCustomMappings
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public DateTime DateOfEvent { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<EventsUsers, EventUserProfileViewModel>()
                .ForMember(m => m.ID, opt => opt.MapFrom(eu => eu.EventID))
                .ForMember(m => m.Title, opt => opt.MapFrom(eu => eu.Event.Title))
                .ForMember(m => m.DateOfEvent, opt => opt.MapFrom(eu => eu.Event.DateOfEvent));
        }
    }
}