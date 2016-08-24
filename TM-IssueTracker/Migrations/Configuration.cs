namespace TM_IssueTracker.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;
    using TM_IssueTracker.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TM_IssueTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TM_IssueTracker.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            context.IssueStates.AddOrUpdate(new [] {
                new Models.IssueState() { Id = 1, Name = "New" },
                new Models.IssueState() { Id = 2, Name = "Open" },
                new Models.IssueState() { Id = 3, Name = "Fixed" },
                new Models.IssueState() { Id = 4, Name = "Closed" }
            });

            ApplicationUser user = new ApplicationUser()
            {
                Id = "c4f5dc34-811e-4ab7-b24a-57215db888fd",
                Email = "peshence@gmail.com",
                PasswordHash = "AHoRa83b3vHNTrKgfOOf79xWKcZ1Q17ZlbsC2bTVNWkyz3wjdYQxpuR5oVvdbHj/jw==",
                SecurityStamp = "231657ba-2a37-4552-807b-2688490dffd5",
                UserName = "peshence@gmail.com",
                EmailConfirmed = false,
                PhoneNumber = null,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                LockoutEndDateUtc = null,
                AccessFailedCount = 0,

            };

            context.Users.AddOrUpdate(new[] {
                user
            });

            context.SaveChanges();

            user = context.Users.AsEnumerable().FirstOrDefault();

            
            context.Projects.AddOrUpdate(new[] {
                new Models.Project() { Id = 7, CreatedOn = DateTime.Now, CreatedBy = user, Name = "Test Project 1", Issues = null },
                new Models.Project() { Id = 8, CreatedOn = DateTime.Now, CreatedBy = user, Name = "Test Project 2", Issues = null }
            });


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
        }
    }
}
