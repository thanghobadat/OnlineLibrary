using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data.Extension
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //Product
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Computer",
                    Description = "This is computer"
                },
            new Product
            {
                Id = 2,
                Name = "Iphone",
                Description = "This is Iphone"
            });

            modelBuilder.Entity<MailSetting>().HasData(
               new MailSetting
               {
                   Id = 1,
                   EMail = "hoangthanh01022000@gmail.com",
                   DisplayName = "hoangthanh01022000@gmail.com",
                   Host = "smtp.gmail.com",
                   Password = "1233211234",
                   Port = 587
               });



            modelBuilder.Entity<Document>().HasData(
               new Document
               {
                   Id = 1,
                   Name = "How to Win Friends and Influence People",
                   Description = "This is How to Win Friends and Influence People",
                   View = 0,
                   DateCreated = DateTime.Now,
                   Author = "Dale Carnegie"
               },
               new Document
               {
                   Id = 2,
                   Name = "The Alchemist",
                   Description = "This is The Alchemist",
                   View = 0,
                   DateCreated = DateTime.Now,
                   Author = "Paulo Coelho"
               });

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Self Help",
                    Description = "Help Yourself"
                },
                new Category
                {
                    Id = 2,
                    Name = "Comic",
                    Description = "Comic"
                });


            modelBuilder.Entity<DocumentCategory>().HasData(
                new DocumentCategory
                {
                    DocumentId = 1,
                    CategoryId = 1
                },
                new DocumentCategory
                {
                    DocumentId = 2,
                    CategoryId = 1
                });

            modelBuilder.Entity<Chapter>().HasData(
               new Chapter
               {
                   Id = 1,
                   DocumentId = 1,
                   Name = "Chapter 1",
                   Content = "Content chapter 1",
                   SortOrder = 1
               },
              new Chapter
              {
                  Id = 2,
                  DocumentId = 1,
                  Name = "Chapter 2",
                  Content = "Content chapter 2",
                  SortOrder = 2
              });



            // any guid
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "hoangthanh01022000@gmail.com",
                NormalizedEmail = "hoangthanh01022000@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Admin@123"),
                SecurityStamp = string.Empty,
                FirstName = "Thanh",
                LastName = "Ngo",
                DoB = new DateTime(2000, 02, 01)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });


            var roleId1 = new Guid("76dc6e94-6b8c-4468-8452-88456b1ab5f5");
            var userId = new Guid("7b06f9fd-e0c8-47e3-a2c6-acb7a307de7d");
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId1,
                Name = "user",
                NormalizedName = "user",
                Description = "User role"
            });

            var hasher1 = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = userId,
                UserName = "user",
                NormalizedUserName = "user",
                Email = "hoangthanh01022000@gmail.com",
                NormalizedEmail = "hoangthanh01022000@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Admin@123"),
                SecurityStamp = string.Empty,
                FirstName = "Vi",
                LastName = "Truong",
                DoB = new DateTime(2000, 12, 21)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId1,
                UserId = userId
            });




        }
    }
}
