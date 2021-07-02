using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Entities;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Services.UserService.Command;
using RookieOnlineAssetManagement.ViewModels.User;
using System;
using System.Collections.Generic;
using Xunit;

namespace RookieOnlineAssetManagement.UnitTests.User
{
    public class Post
    {
        private ApplicationDbContext CreateDbContext ()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            var dbContext = new ApplicationDbContext(options);
            return dbContext;
        }

        public static IEnumerable<object[]> CreateList =>
       new List<object[]>() {
        new object[]
        {   "HCM",
            new UserCreateModel
            {
                FirstName = "Minh",
                LastName = "Vu Pham Cong",
                Gender = true,
                JoinedDate = DateTime.Now,
                DoB = new DateTime(1996,12,15),
                Type = EnumsObject.Type.Admin
            }
        },
        new object[]
        {   "HCM",
            new UserCreateModel
            {
                FirstName = "Minh",
                LastName = "Vu Pham Cong",
                Gender = true,
                JoinedDate = DateTime.Now,
                DoB = new DateTime(1997,11,17),
                Type = EnumsObject.Type.Admin
            }
        },
        new object[]
        {   "HN",
            new UserCreateModel
            {
                FirstName = "Minh",
                LastName = "Vu Pham Cong",
                Gender = true,
                JoinedDate = DateTime.Now,
                DoB = new DateTime(1996,10,05),
                Type = EnumsObject.Type.Admin,
                Location =EnumsObject.Location.HCM,
            }
        }
   };

        [Theory]
        [MemberData(nameof(CreateList))]
        public async void PostUser (string adminLocation, UserCreateModel createModel)
        {
            //Arrange
            var mockUserManager = new Mock<FakeUserManager>();
            var mockRoleManager = new Mock<FakeRoleManager>();
            var _context = CreateDbContext();

            mockUserManager.Setup(x => x.Users).Returns(_context.Users);
            mockRoleManager.Setup(x => x.Roles).Returns(_context.Roles);

            mockRoleManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(new IdentityRole { Name = createModel.Type.ToString() });
            mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            mockUserManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser { FirstName = createModel.FirstName, LastName = createModel.LastName, Location = adminLocation ?? createModel.Location.ToString() });

            //Act
            var query = new CreateUserCommand { Location = adminLocation, CreateModel = createModel };
            var handler = new CreateUserCommandHandler(mockUserManager.Object, mockRoleManager.Object);
            var result = await handler.Handle(query, new System.Threading.CancellationToken());

            //Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
        }
    }
}
