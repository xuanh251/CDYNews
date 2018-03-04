namespace CDYNews.Data.Migrations
{
    using CDYNews.Model.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CDYNews.Data.CDYNewsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CDYNewsDbContext context)
        {
            //addPage(context);
            //CreateContactDetail(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new CDYNewsDbContext()));

            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new CDYNewsDbContext()));

            //var user = new ApplicationUser()
            //{
            //    UserName = "admin",
            //    Email = "lexuanhoang1997@gmail.com",
            //    EmailConfirmed = true,
            //    FullName = "Le Xuan Hoang"

            //};

            //manager.Create(user, "Xuanhoang!23");

            //if (!roleManager.Roles.Any())
            //{
            //    roleManager.Create(new ApplicationRole { Name = "ViewUsers" });
            //    roleManager.Create(new ApplicationRole { Name = "AddUsers" });
            //    roleManager.Create(new ApplicationRole { Name = "UpdateUsers" });
            //    roleManager.Create(new ApplicationRole { Name = "DeleteUsers" });
            //}
            //if (!context.ApplicationGroups.Any())
            //{
            //    context.ApplicationGroups.Add(new ApplicationGroup { Name = "Admin" });
            //    context.ApplicationGroups.Add(new ApplicationGroup { Name = "User" });
            //}
            //var adminUser = manager.FindByEmail("lexuanhoang1997@gmail.com");

            //manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
        }
        public void addPage(CDYNewsDbContext context)
        {
            var page = new Page()
            {
                Name = "Giới thiệu",
                Alias = "gioi-thieu",
                Content = "Nội dung trang giới thiệu",
                Status = true,
                CreatedDate = DateTime.Now
            };
            context.Pages.Add(page);
            context.SaveChanges();
        }
        public void CreateContactDetail(CDYNewsDbContext context)
        {
            if (context.ContactDetails.Count()==0)
            {
                try
                {
                    var contactDetail = new ContactDetail()
                    {
                        Name="Trang tin tức trường Cao đẳng Y tế Quảng Nam.",
                        Address= "3 Nguyễn Du, Phường An Mỹ, Tam Kỳ, Quảng Nam",
                        Email="cdy@gmail.com",
                        Lat= 15.5723344,
                        Lng= 108.4746795,
                        Phone= "+84 235 3828 267",
                        Website= "cdytqn.edu.vn",
                        Other="",
                        Status=true
                    };
                    context.ContactDetails.Add(contactDetail);
                    context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
