namespace BankApp.Api.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BankApp.Api.Models.BankEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BankApp.Api.Models.BankEntities context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            context.AccountType.AddOrUpdate(
                new AccountType() { Code = 1, Name = "Internal user", Description = "This is internal user of bank" },
                new AccountType() { Code = 2, Name = "External user", Description = "This is External user of bank" }
            );

            context.InterestType.AddOrUpdate(
               new InterestType() { Code = 1, Name = "Monthly", Description = "To take interest by monthly" },
               new InterestType() { Code = 2, Name = "Quaternary", Description = "To take interest by quaternary" },
               new InterestType() { Code = 3, Name = "Half year", Description = "To take interest by half year" },
               new InterestType() { Code = 4, Name = "Yearly", Description = "To take interest by yearly" }
           );

        }
    }
}
