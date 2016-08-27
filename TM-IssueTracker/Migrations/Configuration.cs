namespace TM_IssueTracker.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using System;
    using System.Collections.Generic;
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

            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(
                new ApplicationDbContext()));

            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(
                new ApplicationDbContext()));

            roleManager.Create(new IdentityRole { Name = "admin" });

            userManager.Users.ToList().ForEach(p => userManager.Delete(p));



            // Create 4 test users:
            for (int i = 1; i < 4; i++)
            {
                var usr = new ApplicationUser()
                {
                    UserName = string.Format("User{0}@foo.com", i.ToString())
                };
                userManager.Create(usr, "P@ssw0rd");
            }

            // Create 4 test admin users:
            for (int i = 1; i < 4; i++)
            {
                var usr = new ApplicationUser()
                {
                    UserName = string.Format("Admin{0}@foo.com", i.ToString())
                };
                userManager.Create(usr, "P@ssw0rd");
                userManager.AddToRole(usr.Id, "admin");
            }



            context.IssueStates.AddOrUpdate(new[] {
                new Models.IssueState() { Id = 1, Name = "New" },
                new Models.IssueState() { Id = 2, Name = "Open" },
                new Models.IssueState() { Id = 3, Name = "Fixed" },
                new Models.IssueState() { Id = 4, Name = "Closed" }
            });


            context.SaveChanges();

            foreach (ApplicationUser u in context.Users.ToList())
            {
                if (userManager.GetRoles(u.Id).Contains("admin"))
                {
                    context.Projects.AddOrUpdate(new[] { new Models.Project() { CreatedOn = DateTime.Now, CreatedBy = u, Name = string.Format("Test Project - {0}", u.UserName), Issues = null, Description = string.Format("Description for project 'Test Project - {0}'", u.UserName) } });
                }
                else
                {

                }
            }




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
