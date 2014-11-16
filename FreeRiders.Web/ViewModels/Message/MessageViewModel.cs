namespace FreeRiders.Web.ViewModels.Message
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using FreeRiders.Models;
    using FreeRiders.Web.Infrastructure.Mapping;

    public class MessageViewModel : IMapFrom<Message>, IHaveCustomMappings
    {
        public MessageViewModel()
        {
        }

        public MessageViewModel(int eventID)
        {
            this.EventID = eventID;
        }

        public int EventID { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 100)]
        [UIHint("MultiLineText")]
        public string Text { get; set; }

        [UIHint("UserBasicViewModelMediumLink")]
        public string AuthorName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Message, MessageViewModel>()
                .ForMember(m => m.AuthorName, opt => opt.MapFrom(message => message.Author.UserName));
        }
    }
}