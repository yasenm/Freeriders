namespace FreeRiders.Web.ViewModels.Album
{
    using FreeRiders.Models;
    using FreeRiders.Web.Infrastructure.Mapping;

    public class AlbumIndexViewModel : IMapFrom<Album>
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public virtual Picture Picture { get; set; }
    }
}