namespace FreeRiders.Web.Areas.Administration.ViewModels.User
{
    using AutoMapper;

    using FreeRiders.Models;
    using FreeRiders.Web.Areas.Administration.ViewModels.Base;
    using FreeRiders.Web.Infrastructure.Mapping;
    using System.Web.Security;

    public class UserAdminViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        //public string Role { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
        }
    }
}