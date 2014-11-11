namespace FreeRiders.Data
{
    using System.Data.Entity;

    using FreeRiders.Models;
    using System.Data.Entity.Infrastructure;

    public interface IFreeRidersDbContext
    {
        IDbSet<ApplicationUser> Users { get; set; }

        IDbSet<Album> Albums { get; set; }

        IDbSet<AlbumCategory> AlbumCategories { get; set; }

        IDbSet<Location> Locations { get; set; }

        IDbSet<LocationCategory> LocationCategories { get; set; }

        IDbSet<Review> Reviews { get; set; }

        IDbSet<Picture> Pictures { get; set; }

        IDbSet<Event> Events { get; set; }

        IDbSet<Message> Messages { get; set; }

        DbContext DbContext { get; }

        int SaveChanges();

        void Dispose();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
