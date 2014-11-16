namespace FreeRiders.Web.ViewModels.User
{
    using System.Collections.Generic;
    using System.IO;
    using System.Web;

    using FreeRiders.Models;
    using FreeRiders.Web.Infrastructure.Mapping;
    using FreeRiders.Web.ViewModels.Event;
    using FreeRiders.Web.ViewModels.Album;

    public class UserProfileViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public byte[] Avatar { get; set; }

        public HttpPostedFileBase PostedAvatar { get; set; }

        public ICollection<EventUserProfileViewModel> EventsJoined { get; set; }

        public ICollection<AlbumIndexViewModel> AlbumsCreated { get; set; }

        public ICollection<EventsViewModel> EventsCreated { get; set; }
    }
}