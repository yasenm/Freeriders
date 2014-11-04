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

    public class FreeRidersDbContext : IdentityDbContext<ApplicationUser>, IFreeRidersDbContext
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
    }
}
