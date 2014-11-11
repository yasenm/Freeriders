namespace FreeRiders.Data.UnitsOfWork
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using FreeRiders.Data.Repositories;
    using FreeRiders.Models;
    using FreeRiders.Data.Common.Repository;

    public class FreeRidersData : IFreeRidersData
    {
        private IDictionary<Type, object> repositories;

        public FreeRidersData(IFreeRidersDbContext context)
        {
            this.Context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IFreeRidersDbContext Context { get; set; }

        public IRepository<ApplicationUser> Users
        {
            get
            {
                return this.GetRepository<ApplicationUser>();
            }
        }

        public IRepository<Album> Albums
        {
            get
            {
                return this.GetRepository<Album>();
            }
        }

        public IRepository<AlbumCategory> AlbumCategories
        {
            get
            {
                return this.GetRepository<AlbumCategory>();
            }
        }

        public IRepository<Location> Locations
        {
            get
            {
                return this.GetRepository<Location>();
            }
        }

        public IRepository<LocationCategory> LocationCategories
        {
            get
            {
                return this.GetRepository<LocationCategory>();
            }
        }

        public IRepository<Picture> Pictures
        {
            get
            {
                return this.GetRepository<Picture>();
            }
        }

        public IRepository<Review> Reviews
        {
            get
            {
                return this.GetRepository<Review>();
            }
        }

        public IRepository<Event> Events
        {
            get
            {
                return this.GetRepository<Event>();
            }
        }

        public IRepository<Message> Messages
        {
            get
            {
                return this.GetRepository<Message>();
            }
        }

        public int SaveChanges()
        {
            return this.Context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(GenericRepository<T>), Context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }

    }
}
