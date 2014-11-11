namespace FreeRiders.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity.EntityFramework;

    using FreeRiders.Data.Migrations;
    using FreeRiders.Models;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using FreeRiders.Data.Common.Models;

    public class FreeRidersDbContext : IdentityDbContext<ApplicationUser>, IFreeRidersDbContext
    {
        public FreeRidersDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FreeRidersDbContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
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
    }
}
