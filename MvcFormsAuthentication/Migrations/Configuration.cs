namespace MvcFormsAuthentication.Migrations
{
    using MvcFormsAuthentication.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MvcFormsAuthentication.Service;

    internal sealed class Configuration : DbMigrationsConfiguration<MvcFormsAuthentication.Data.AccounIndexContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MvcFormsAuthentication.Data.AccounIndexContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Accounts.AddOrUpdate(
                x=>x.Id,
                new Account { Id = 1, Name = "Kevin", Password = HashService.MD5Hash("12345678"), Email = "kevin@gmail.com", Mobile = "0925-155-224" },
                new Account { Id = 2, Name = "Mary", Password = HashService.MD5Hash("abc.123"), Email = "mary@gmail.com", Mobile = "0955-259-885" }
                );
        }

        
    }
}
