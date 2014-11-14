namespace FreeRiders.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using FreeRiders.Data.Common;
    using FreeRiders.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<FreeRidersDbContext>
    {
        private UserManager<User> userManager;
        private UserStore<User> userStore;

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(FreeRidersDbContext context)
        {
            userStore = new UserStore<User>(context);
            userManager = new UserManager<User>(userStore);

            this.SeedRoles(context);
            this.SeedUsers(context);
            this.SeedLocationCategories(context);
            this.SeedAlbumCategories(context);
        }

        private void SeedRoles(FreeRidersDbContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }

            context.Roles.AddOrUpdate(new IdentityRole(GlobalConstants.AdminRole));
            context.Roles.AddOrUpdate(new IdentityRole(GlobalConstants.ModeratorRole));
            context.Roles.AddOrUpdate(new IdentityRole(GlobalConstants.TrustedUserRole));
            context.Roles.AddOrUpdate(new IdentityRole(GlobalConstants.RegularUserRole));
            context.SaveChanges();
        }

        private void SeedUsers(FreeRidersDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            this.SeedAdmin(context);
            this.SeedModerators(context);
            this.SeedTrustedUsers(context);
        }

        private void SeedModerators(FreeRidersDbContext context)
        {
            this.SeedUsersByRole(context, GlobalConstants.ModeratorRole, 10);
        }

        private void SeedTrustedUsers(FreeRidersDbContext context)
        {
            this.SeedUsersByRole(context, GlobalConstants.TrustedUserRole, 10);
        }

        private void SeedAdmin(FreeRidersDbContext context)
        {
            var admin = new User
            {
                Email = GlobalConstants.AdminEmailUsername,
                UserName = GlobalConstants.AdminEmailUsername,
            };

            userManager.Create(admin, GlobalConstants.AdminPassword);
            userManager.AddToRole(admin.Id, GlobalConstants.AdminRole);
            context.SaveChanges();
        }

        private void SeedUsersByRole(FreeRidersDbContext context, string role, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var usernameAndEmail = string.Format("{0}@{0}.com", RandomGenerator.RandomString(2, 10));
                var user = new User
                {
                    Email = usernameAndEmail,
                    UserName = usernameAndEmail,
                };

                userManager.Create(user, GlobalConstants.AdminPassword);
                userManager.AddToRole(user.Id, role);
                context.SaveChanges();
            }
        }

        private void SeedLocationCategories(FreeRidersDbContext context)
        {
            if (context.LocationCategories.Any())
            {
                return;
            }

            for (int i = 0; i < 10; i++)
            {
                var category = new LocationCategory
                {
                    Name = RandomGenerator.RandomString(5, 40),
                };

                context.LocationCategories.AddOrUpdate(category);
            }

            context.SaveChanges();
        }

        private void SeedAlbumCategories(FreeRidersDbContext context)
        {
            if (context.AlbumCategories.Any())
            {
                return;
            }

            for (int i = 0; i < 10; i++)
            {
                var category = new AlbumCategory
                {
                    Name = RandomGenerator.RandomString(5, 40),
                };

                context.AlbumCategories.AddOrUpdate(category);
            }

            context.SaveChanges();
        }
    }
}
