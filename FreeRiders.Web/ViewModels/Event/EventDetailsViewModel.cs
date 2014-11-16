namespace FreeRiders.Web.ViewModels.Event
{
    using System;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using FreeRiders.Models;
    using FreeRiders.Web.Infrastructure.Mapping;
    using FreeRiders.Web.ViewModels.User;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class EventDetailsViewModel : IMapFrom<Event>, IHaveCustomMappings
    {
        public int ID { get; set; }

        [Required]
        [UIHint("SingleLineText")]
        public string Title { get; set; }

        [Required]
        [UIHint("MultiLineText")]
        public string Description { get; set; }

        public string CreatorID { get; set; }

        public string CreatorName { get; set; }

        [Required]
        public int LocationID { get; set; }

        public string LocationName { get; set; }

        [Required]
        [UIHint("DateTimePicker")]
        [DataType(DataType.Date)]
        public DateTime DateOfEvent { get; set; }

        public ICollection<UserBasicViewModel> Users { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Event, EventsIndexViewModel>()
                .ForMember(m => m.CreatorName, opt => opt.MapFrom(ev => ev.Creator.UserName))
                .ForMember(m => m.LocationName, opt => opt.MapFrom(ev => ev.Location.Name));
        }
    }
}