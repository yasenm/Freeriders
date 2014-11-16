namespace FreeRiders.Web.ViewModels.Album
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    using AutoMapper;

    using FreeRiders.Models;
    using FreeRiders.Web.Infrastructure.Mapping;

    public class EditAlbumViewModel : IMapFrom<Album>, IHaveCustomMappings
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int LocationID { get; set; }

        public string LocationName { get; set; }

        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public string CreatorID { get; set; }

        public string CreatorName { get; set; }

        public Picture Picture { get; set; }

        public ICollection<HttpPostedFileBase> PicturesAdded { get; set; }

        public ICollection<Picture> Pictures { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Album, EditAlbumViewModel>()
                .ForMember(m => m.CreatorName, opt => opt.MapFrom(ev => ev.Creator.UserName))
                .ForMember(m => m.LocationName, opt => opt.MapFrom(ev => ev.Location.Name))
                .ForMember(m => m.LocationName, opt => opt.MapFrom(ev => ev.Location.Name));
        }
    }
}