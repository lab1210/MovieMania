namespace Movie_Mania.Migrations
{
    using Movie_Mania.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Movie_Mania.Models.MovieContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Movie_Mania.Models.MovieContext context)
        {
            if (!context.registers.Any())
            {
                context.registers.AddOrUpdate(
                new Registers() { UserName = "Oladeji", Email = "oladejiomolabake14@gmail.com", Password = "Admin1234", Role = "Admin" },

            new Registers() { UserName = "Olaniran", Email = "Olaniran4@gmail.com", Password = "Admin12345", Role = "Admin" }
                );
            }
            
        }
    }
}
