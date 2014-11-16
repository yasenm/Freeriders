namespace FreeRiders.Web.ViewModels.User
{
    using FreeRiders.Models;
    using FreeRiders.Web.Infrastructure.Mapping;

    public class UserBasicViewModel : IMapFrom<EventsUsers>, IHaveCustomMappings
    {
        public string UserName { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<EventsUsers, UserBasicViewModel>()
                .ForMember(m => m.UserName, opt => opt.MapFrom(ev => ev.User.UserName));
        }
    }
}