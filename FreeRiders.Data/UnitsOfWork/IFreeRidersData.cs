namespace FreeRiders.Data.UnitsOfWork
{
    using FreeRiders.Models;

    using FreeRiders.Data.Common.Repository;
    using FreeRiders.Data.Repositories;

    public interface IFreeRidersData
    {
        IFreeRidersDbContext Context { get; }

        IRepository<ApplicationUser> Users { get; }

        IRepository<Album> Albums { get; }

        IRepository<AlbumCategory> AlbumCategories { get; }

        IRepository<Location> Locations { get; }

        IRepository<LocationCategory> LocationCategories { get; }

        IRepository<Picture> Pictures { get; }

        IRepository<Review> Reviews { get; }

        IRepository<Event> Events { get; }

        IRepository<Message> Messages { get; }

        int SaveChanges();
    }
}
