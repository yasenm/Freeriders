namespace FreeRiders.Data.Migrations
{
    using FreeRiders.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FreeRidersDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(FreeRidersDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            this.SeedAdmin(context);
            this.SeedEvents(context);
        }

        private void SeedEvents(FreeRidersDbContext context)
        {
            
        }

        private void SeedAdmin(FreeRidersDbContext context)
        {
            //context.Events.AddOrUpdate(e => e.ID,
            //    new Event
            //    {

            //    });
        }
    }
}
