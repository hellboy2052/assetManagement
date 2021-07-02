using Microsoft.AspNetCore.Identity;
using RookieOnlineAssetManagement.Entities;
using RookieOnlineAssetManagement.Enums;
using System;
using System.Collections.Generic;

namespace RookieOnlineAssetManagement.UnitTests.FakeData
{
    public static class FakeDataObject
    {
        public static List<ApplicationUser> UserList ()
        {
            var users = new List<ApplicationUser>
            {   new ApplicationUser
                {
                    Id = "user1",
                    StaffCode = "SD0001",
                    FirstName = "Nguyen Thuy",
                    LastName = "An",
                    FullName = "Nguyen Thuy An",
                    UserName = "annt",
                    JoinedDate = new DateTime(2019, 06, 20),
                    Gender = true,
                    IsDisabled = false,
                    Location = "HCM",
                    IncrementId = 1,
                    IsAssigned = false,
                    DateOfBirth = new DateTime(1999, 06, 20),
                    Type =EnumsObject.Type.Staff.ToString(),
                },
                new ApplicationUser
                {
                    Id = "admin1",
                    StaffCode = "SD0002",
                    FirstName = "Tran Van",
                    FullName = "Tran Van An",
                    LastName = "An",
                    UserName = "antv",
                    JoinedDate = new DateTime(2019, 04, 09),
                    Gender = false,
                    IsDisabled = false,
                    Location = "HCM",
                    IncrementId = 2,
                    IsAssigned = false,
                    DateOfBirth = new DateTime(1993, 02, 4),
                    Type=EnumsObject.Type.Admin.ToString(),
                },
                new ApplicationUser
                {
                    Id = "user2",
                    StaffCode = "SD0003",
                    FirstName = "Nguyen Van",
                    LastName = "Binh",
                    FullName = "Nguyen Van Binh",
                    UserName = "binhnv1",
                    JoinedDate = new DateTime(2018, 04, 18),
                    Gender = true,
                    IsDisabled = true,
                    Location = "HN",
                    IncrementId = 3,
                    IsAssigned = true,
                    DateOfBirth = new DateTime(1996, 03, 28),
                    Type = EnumsObject.Type.Staff.ToString(),
                },
            };
            return users;
        }

        public static List<IdentityUserRole<string>> UserRoleList ()
        {
            var userRoles = new List<IdentityUserRole<string>>
            {    new IdentityUserRole<string>
                {
                    RoleId = "admin",
                    UserId = "admin1"
                 },
                new IdentityUserRole<string>
                {
                    RoleId = "staff",
                    UserId = "user1"
                }, new IdentityUserRole<string>
                {
                    RoleId = "staff",
                    UserId = "user2"
                }
            };
            return userRoles;
        }

        public static List<Category> CategoryList ()
        {
            var categoryList = new List<Category>
            {    new Category
                {
                    CategoryId = "LA",
                    CategoryName = "Laptop",
                 },
                new Category
                {
                    CategoryId = "PC",
                    CategoryName = "Personal Computer",
                 },
            };
            return categoryList;
        }

        public static List<Assignment> AssignmentList ()
        {
            var assignmentList = new List<Assignment>
            {   new Assignment
                {
                    AssignmentId = "ASIGNMENT1",
                    State = EnumsObject.AssignmentState.WaitingForAcceptance.ToString(),
                    AssignedDate = DateTime.Now,
                    Note = "This is A Note 1",
                    StaffId = "user1",
                    AssetCode= "LA000000",
                },
                new Assignment
                {
                    AssignmentId = "ASIGNMENT2",
                    State = EnumsObject.AssignmentState.Accepted.ToString(),
                    AssignedDate = DateTime.Now,
                    Note = "This is A Note 2",
                    StaffId = "admin1",
                    AssetCode= "PC000000",
                },
            };
            return assignmentList;
        }
        public static List<Asset> AsssetList ()
        {
            var assetList = new List<Asset>
            {    new Asset
                {
                    AssestCode = "LA000000",
                    AssestName = "Laptop 1",
                    Location = "HCM",
                    State = EnumsObject.State.Available.ToString(),
                    CategoryId="LA",
                 },
                new Asset
                {
                    AssestCode = "PC000000",
                    AssestName = "Personal Computer 1",
                    IsAssigned = true,
                    Location = "HN",
                    State = EnumsObject.State.Available.ToString(),
                    CategoryId = "PC",
                 },
            };
            return assetList;
        }

        public static List<Returning> ReturningList ()
        {
            var returningList = new List<Returning>
            {
                new Returning
                {
                    ReturnId = "RETURNING1",
                    AssignedById = "admin1",
                    RequestById = "user1",
                    AssignmentId = "ASIGNMENT1",
                    ReturnedDate = new DateTime(2021, 05, 23),
                    State = EnumsObject.Returning.Completed.ToString(),
                },
                new Returning
                {
                    ReturnId = "RETURNING2",
                    AssignedById = "admin1",
                    RequestById = "user2",
                    AssignmentId = "ASIGNMENT2",
                    State = EnumsObject.Returning.WaitingForReturning.ToString(),
                },
            };
            return returningList;
        }
        public static List<IdentityRole> RoleList ()
        {
            var roles = new List<IdentityRole>
            {
                new IdentityRole { Id = "admin", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "staff", Name = "Staff", NormalizedName = "STAFF" }
            };
            return roles;
        }
    }
}
