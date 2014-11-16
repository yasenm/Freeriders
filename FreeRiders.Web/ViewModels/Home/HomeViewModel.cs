namespace FreeRiders.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using FreeRiders.Web.ViewModels.Album;
    using FreeRiders.Web.ViewModels.Event;

    public class HomeViewModel
    {
        public ICollection<AlbumIndexViewModel> Albums { get; set; }

        public ICollection<LocationViewModel> Locations { get; set; }

        public ICollection<EventsViewModel> Events { get; set; }
    }
}