namespace FreeRiders.Data.UnitsOfWork
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using FreeRiders.Data.Repositories;
    using FreeRiders.Models;
    using FreeRiders.Data.Common.Repository;
    using FreeRiders.Data.Common.Models;

    public class FreeRidersData : IFreeRidersData
    {
        private IDictionary<Type, object> repositories;

        public FreeRidersData(IFreeRidersDbContext context)
        {
            this.Context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IFreeRidersDbContext Context { get; set; }

        public IRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        public IRepository<Album> Albums
        {
            get
            {
                return this.GetDeletableEntityRepository<Album>();
            }
        }

        public IRepository<AlbumCategory> AlbumCategories
        {
            get
            {
                return this.GetDeletableEntityRepository<AlbumCategory>();
            }
        }

        public IRepository<Location> Locations
        {
            get
            {
                return this.GetDeletableEntityRepository<Location>();
            }
        }

        public IRepository<LocationCategory> LocationCategories
        {
            get
            {
                return this.GetDeletableEntityRepository<LocationCategory>();
            }
        }

        public IRepository<Picture> Pictures
        {
            get
            {
                return this.GetDeletableEntityRepository<Picture>();
            }
        }

        public IRepository<Review> Reviews
        {
            get
            {
                return this.GetDeletableEntityRepository<Review>();
            }
        }

        public IRepository<Event> Events
        {
            get
            {
                return this.GetDeletableEntityRepository<Event>();
            }
        }

        public IRepository<Message> Messages
        {
            get
            {
                return this.GetDeletableEntityRepository<Message>();
            }
        }

        public IRepository<EventsUsers> EventsUsers
        {
            get
            {
                return this.GetRepository<EventsUsers>();
            }
        }

        public int SaveChanges()
        {
            return this.Context.SaveChanges();
        }
        
        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.Context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }

        private IDeletableEntityRepository<T> GetDeletableEntityRepository<T>() where T : class, IDeletableEntity
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(DeletableEntityRepository<T>), this.Context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IDeletableEntityRepository<T>)this.repositories[typeOfRepository];
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.Context != null)
                {
                    this.Context.Dispose();
                }
            }
        }

    }
}
