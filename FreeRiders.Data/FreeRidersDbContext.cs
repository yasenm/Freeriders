namespace FreeRiders.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity.EntityFramework;

    using FreeRiders.Data.Migrations;
    using FreeRiders.Models;
    using FreeRiders.Data.Common.Models;
    using FreeRiders.Data.Common.Contracts.CodeFirstConventions;

    public class FreeRidersDbContext : IdentityDbContext<User>, IFreeRidersDbContext
    {
        public FreeRidersDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FreeRidersDbContext, Configuration>());
        }

        public static FreeRidersDbContext Create()
        {
            return new FreeRidersDbContext();
        }

        public IDbSet<Album> Albums { get; set; }

        public IDbSet<AlbumCategory> AlbumCategories { get; set; }

        public IDbSet<Location> Locations { get; set; }

        public IDbSet<LocationCategory> LocationCategories { get; set; }

        public IDbSet<Review> Reviews { get; set; }

        public IDbSet<Picture> Pictures { get; set; }

        public IDbSet<Event> Events { get; set; }

        public IDbSet<Message> Messages { get; set; }

        public DbContext DbContext
        {
            get
            {
                return this;
            }
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            this.ApplyDeletableEntityRules();
            return base.SaveChanges();
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Add(new IsUnicodeAttributeConvention());

            base.OnModelCreating(modelBuilder); // Without this call EntityFramework won't be able to configure the identity model
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (!entity.PreserveCreatedOn)
                    {
                        entity.CreatedOn = DateTime.Now;
                    }
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        private void ApplyDeletableEntityRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (
                var entry in
                    this.ChangeTracker.Entries()
                        .Where(e => e.Entity is IDeletableEntity && (e.State == EntityState.Deleted)))
            {
                var entity = (IDeletableEntity)entry.Entity;

                entity.DeletedOn = DateTime.Now;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }
    }
}
