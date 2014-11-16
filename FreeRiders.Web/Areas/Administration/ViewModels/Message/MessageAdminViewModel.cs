namespace FreeRiders.Web.Areas.Administration.ViewModels.Message
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using FreeRiders.Models;
    using FreeRiders.Web.Areas.Administration.ViewModels.Base;
    using FreeRiders.Web.Infrastructure.Mapping;

    public class MessageAdminViewModel : AdministrationViewModel, IMapFrom<Message>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        public string AuthorID { get; set; }

        public string AuthorName { get; set; }

        public string Text { get; set; }

        public int EventID { get; set; }

        public string EventTitle { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Message, MessageAdminViewModel>()
                .ForMember(m => m.AuthorName, opt => opt.MapFrom(message => message.Author.UserName))
                .ForMember(m => m.EventTitle, opt => opt.MapFrom(message => message.Event.Title));
        }
    }
}