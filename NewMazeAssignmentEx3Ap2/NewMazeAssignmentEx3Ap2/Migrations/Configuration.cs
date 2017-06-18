namespace NewMazeAssignmentEx3Ap2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using NewMazeAssignmentEx3Ap2.Models.UserModel;
    using NewMazeAssignmentEx3Ap2.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<NewMazeAssignmentEx3Ap2Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NewMazeAssignmentEx3Ap2Context context)
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

            context.Users.AddOrUpdate(user => user.Name, new User() { Name = "yoav ", Password = "123456", Email = "yoav@yoav.com", Wins = 10, Losses = 4 });
        }
    }
}
