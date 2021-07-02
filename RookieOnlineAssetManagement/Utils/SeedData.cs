using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Entities;
using RookieOnlineAssetManagement.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Utils
{
    public static class ModelBuilderExtensions
    {
        public static void SeedData (this ModelBuilder builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            var admin1 = new ApplicationUser
            {
                Id = "adminhn",
                StaffCode = "SD0001",
                FirstName = "An",
                LastName = "Nguyen Thuy",
                UserName = "adminhn",
                JoinedDate = new DateTime(2019, 06, 20),
                Gender = true,
                IsDisabled = false,
                Location = "HN",
                IncrementId = 1,
                DateOfBirth = new DateTime(1999, 06, 20),
                Type = "Admin",
                IsDefaultPassword = true,
            };
            admin1.NormalizedUserName = admin1.UserName.ToUpper();
            admin1.PasswordHash = hasher.HashPassword(admin1, "adminhn@123");

            var admin2 = new ApplicationUser
            {
                Id = "adminhcm",
                StaffCode = "SD0002",
                FirstName = "An",
                LastName = "Tran Van",
                UserName = "adminhcm",
                JoinedDate = new DateTime(2019, 04, 09),
                Gender = false,
                IsDisabled = false,
                Location = "HCM",
                IncrementId = 2,
                DateOfBirth = new DateTime(1993, 02, 4),
                Type = "Admin",
                IsDefaultPassword = true,
            };
            admin2.NormalizedUserName = admin2.UserName.ToUpper();
            admin2.PasswordHash = hasher.HashPassword(admin2, "adminhcm@123");

            var user1 = new ApplicationUser
            {
                Id = "userhn1",
                StaffCode = "SD0003",
                FirstName = "Toai",
                LastName = "Nguyen Van",
                UserName = "binhnt",
                JoinedDate = new DateTime(2018, 04, 18),
                Gender = false,
                IsDisabled = true,
                Location = "HN",
                IncrementId = 3,
                DateOfBirth = new DateTime(1996, 03, 28),
                Type = "Staff",
                IsDefaultPassword = true,
            };
            user1.NormalizedUserName = user1.UserName.ToUpper();
            user1.PasswordHash = hasher.HashPassword(user1, "userhn@123");

            var user2 = new ApplicationUser
            {
                Id = "userhcm",
                StaffCode = "SD0004",
                FirstName = "Binh",
                LastName = "Nguyen Van",
                UserName = "binhnv",
                JoinedDate = new DateTime(2018, 04, 18),
                Gender = true,
                IsDisabled = false,
                Location = "HCM",
                IncrementId = 4,
                DateOfBirth = new DateTime(1996, 03, 28),
                Type = "Staff",
                IsDefaultPassword = true,
            };
            user2.NormalizedUserName = user2.UserName.ToUpper();
            user2.PasswordHash = hasher.HashPassword(user2, "user@123");

            var user3 = new ApplicationUser
            {
                Id = "userhn2",
                StaffCode = "SD0005",
                FirstName = "Binh",
                LastName = "Vo Nguyen",
                UserName = "binhnv1",
                JoinedDate = new DateTime(2018, 04, 18),
                Gender = true,
                IsDisabled = false,
                Location = "HN",
                IncrementId = 5,
                DateOfBirth = new DateTime(1996, 03, 28),
                Type = "Staff",
                IsDefaultPassword = true,
            };
            user3.NormalizedUserName = user3.UserName.ToUpper();
            user3.PasswordHash = hasher.HashPassword(user3, "user@123");

            builder.Entity<ApplicationUser>().HasData(admin1, admin2, user1, user2, user3);

            var admin1_role = new IdentityUserRole<string>
            {
                RoleId = "admin",
                UserId = "adminhn"
            };
            var admin2_role = new IdentityUserRole<string>
            {
                RoleId = "admin",
                UserId = "adminhcm"
            };
            var user1_role = new IdentityUserRole<string>
            {
                RoleId = "staff",
                UserId = "userhn1"
            };
            var user2_role = new IdentityUserRole<string>
            {
                RoleId = "staff",
                UserId = "userhcm"
            };
            var user3_role = new IdentityUserRole<string>
            {
                RoleId = "staff",
                UserId = "userhn2"
            };

            builder.Entity<IdentityUserRole<string>>().HasData(admin1_role, admin2_role, user1_role, user2_role, user3_role);

            var adminRole = new IdentityRole { Id = "admin", Name = "Admin", NormalizedName = "ADMIN" };
            var staffRole = new IdentityRole { Id = "staff", Name = "Staff", NormalizedName = "STAFF" };

            builder.Entity<IdentityRole>().HasData(adminRole, staffRole);

            var admin1_claim = new IdentityUserClaim<string>
            {
                Id = 1,
                UserId = admin1.Id,
                ClaimType = "Location",
                ClaimValue = "HN",
            };
            var admin2_claim = new IdentityUserClaim<string>
            {
                Id = 2,
                UserId = admin2.Id,
                ClaimType = "Location",
                ClaimValue = "HCM",
            };
            var user1_claim = new IdentityUserClaim<string>
            {
                Id = 3,
                UserId = user1.Id,
                ClaimType = "Location",
                ClaimValue = "HN",
            };
            var user2_claim = new IdentityUserClaim<string>
            {
                Id = 4,
                UserId = user2.Id,
                ClaimType = "Location",
                ClaimValue = "HCM",
            };
            var user3_claim = new IdentityUserClaim<string>
            {
                Id = 5,
                UserId = user3.Id,
                ClaimType = "Location",
                ClaimValue = "HN",
            };

            builder.Entity<IdentityUserClaim<string>>().HasData(admin1_claim, admin2_claim, user1_claim, user2_claim, user3_claim);

            var laptopCategory = new Category { CategoryId = "LA", CategoryName = "Laptop" };
            var pcCategory = new Category { CategoryId = "PC", CategoryName = "Personal Computer" };

            builder.Entity<Category>().HasData(laptopCategory, pcCategory);


            var laptop_hcm = new Asset
            {
                AssestCode = "LA000000",
                AssestName = "Lenovo Thinkpad T440p",
                InstallDate = new DateTime(2014, 04, 21),
                CategoryId = laptopCategory.CategoryId,
                Location = "HCM",
                Specification = "This is a simple Specification from HCM",
                State = EnumsObject.State.Available.ToString(),
            };
            var laptop_hn1 = new Asset
            {
                AssestCode = "LA000001",
                AssestName = "Lenovo Thinkpad X240",
                InstallDate = new DateTime(2014, 04, 21),
                CategoryId = laptopCategory.CategoryId,
                Location = "HN",
                Specification = "This is a simple Specification from HN",
                State = EnumsObject.State.Available.ToString(),
            };
            var laptop_hn2 = new Asset
            {
                AssestCode = "LA000002",
                AssestName = "Lenovo Thinkpad T440p",
                InstallDate = new DateTime(2016, 02, 10),
                CategoryId = laptopCategory.CategoryId,
                Location = "HN",
                Specification = "This is a simple Specification from HN",
                State = EnumsObject.State.Recycled.ToString(),
            };
            var pc_hcm1 = new Asset
            {
                AssestCode = "PC000000",
                AssestName = "PC GTX 2060",
                InstallDate = new DateTime(2020, 04, 21),
                CategoryId = pcCategory.CategoryId,
                Location = "HCM",
                Specification = "This is a simple Specification from HCM",
                State = EnumsObject.State.WaitingForRecycling.ToString(),
            };
            var pc_hcm2 = new Asset
            {
                AssestCode = "PC000001",
                AssestName = "PC GTX 1060",
                InstallDate = new DateTime(2018, 09, 21),
                CategoryId = pcCategory.CategoryId,
                Location = "HCM",
                Specification = "This is a simple Specification from HCM",
                State = EnumsObject.State.NotAvailable.ToString(),
            };
            var pc_hn = new Asset
            {
                AssestCode = "PC000002",
                AssestName = "PC GTX 660",
                InstallDate = new DateTime(2010, 04, 11),
                CategoryId = pcCategory.CategoryId,
                Location = "HN",
                Specification = "This is a simple Specification from HN",
                State = EnumsObject.State.Assigned.ToString(),
            };

            builder.Entity<Asset>().HasData(laptop_hcm, laptop_hn1, laptop_hn2, pc_hcm1, pc_hcm2, pc_hn);

        }
    }
}
