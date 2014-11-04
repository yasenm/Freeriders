namespace FreeRiders.Data
{
    using System.Data.Entity;

    using FreeRiders.Models;

    public interface IFreeRidersDbContext
    {
        IDbSet<Album> Albums { get; set; }

        IDbSet<AlbumCategory> AlbumCategories { get; set; }

        IDbSet<Location> Locations { get; set; }

        IDbSet<LocationCategory> LocationCategories { get; set; }

        IDbSet<Review> Reviews { get; set; }

        IDbSet<Picture> Pictures { get; set; }
    }
}
