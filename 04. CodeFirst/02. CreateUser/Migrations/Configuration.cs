namespace _02.CreateUser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using Models;



    internal sealed class Configuration : DbMigrationsConfiguration<_02.CreateUser.UserContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(UserContext context)
        {
            context.Towns.AddOrUpdate(town => town.Name,
               new Town()
               {
                   Name = "Asenovgrad",
                   CountryName = "Bulgaria"
               },
               new Town()
               {
                   Name = "Sofia",
                   CountryName = "Bulgaria"
               }, new Town()
               {
                   Name = "Karlovo",
                   CountryName = "Bulgaria"
               }, new Town()
               {
                   Name = "Seattle",
                   CountryName = "USA"
               });
            context.SaveChanges();

            context.Users.AddOrUpdate(user => user.Username, new User
            {
                Age = 15,
                Email = "daspf@abv.com",
                IsDeleted = true,
                LastTimeLoggedIn = new DateTime(1992, 11, 29),
                Password = "hel$A-_99lo",
                RegisteredOn = new DateTime(1992, 11, 28),
                Username = "Bai Stenly",
                FirstName = "Pesho",
                LastName = "Peshev"
            },
            new User
            {
                Age = 110,
                Email = "bdas@gam.com",
                IsDeleted = true,
                LastTimeLoggedIn = new DateTime(1993, 1, 20),
                Password = "heds-alA2_lo",
                RegisteredOn = new DateTime(1992, 11, 28),
                Username = "Bai Nasko",
                FirstName = "ASA",
                LastName = "Sdaad"
            });

            context.Users.AddOrUpdate();
            context.SaveChanges();
        }
    }
}
