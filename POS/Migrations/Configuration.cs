using POS.Models;

namespace POS.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<POS.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(POS.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Items.AddOrUpdate(
                p => p.Id,
                new Item { Id = 1, Name = "Xiaomi", Price = 13500},
                new Item { Id = 2, Name = "Symphony", Price = 10500},
                new Item { Id = 3, Name = "Samsung", Price = 20500},
                new Item { Id = 4, Name = "Iphone", Price = 40500}
            );

            context.Stocks.AddOrUpdate(
                q => q.Id,
                new Stock { ItemId = 1, Quantity = 50},
                new Stock { ItemId = 2, Quantity = 500},
                new Stock { ItemId = 3, Quantity = 200},
                new Stock { ItemId = 4, Quantity = 100}
            );
        }
    }
}
