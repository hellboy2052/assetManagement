using Microsoft.EntityFrameworkCore;
using Moq;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Services.UserService.Command;
using RookieOnlineAssetManagement.UnitTests.FakeData;
using RookieOnlineAssetManagement.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RookieOnlineAssetManagement.UnitTests.User
{
    public class Put
    {
        private ApplicationDbContext CreateDbContext ()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Users.AddRange(FakeDataObject.UserList().AsQueryable());
            dbContext.Roles.AddRange(FakeDataObject.RoleList().AsQueryable());
            dbContext.UserRoles.AddRange(FakeDataObject.UserRoleList().AsQueryable());
            dbContext.SaveChanges();
            return dbContext;
        }

        public static IEnumerable<object[]> UpdateList =>
       new List<object[]>() {
        new object[]
        {   "HCM",
            new UserUpdateModel
            {   Id= "user1",
                Gender = true,
                JoinedDate = DateTime.Now,
                DateOfBirth = new DateTime(2014,04,21),
                Type = EnumsObject.Type.Admin,
            }
        },
        new object[]
        {  "HCM",
            new UserUpdateModel
            {   Id= "admin1",
                Gender = true,
                Type = EnumsObject.Type.Staff,
            }
        }
   };

        [Theory]
        [MemberData(nameof(UpdateList))]
        public async void PutUser (string adminLocation, UserUpdateModel updateModel)
        {
            //Arrange
            var mockUserManager = new Mock<FakeUserManager>();
            var mockRoleManager = new Mock<FakeRoleManager>();
            var _context = CreateDbContext();

            mockUserManager.Setup(x => x.Users).Returns(_context.Users);
            mockRoleManager.Setup(x => x.Roles).Returns(_context.Roles);

            var existUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == updateModel.Id);
            var updateRole = await _context.Roles.FirstOrDefaultAsync(x => x.Name == updateModel.Type.ToString());
            var existUserRoles = _context.UserRoles.Where(x => x.UserId == updateModel.Id);

            mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(existUser);
            mockRoleManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(updateRole);
            mockUserManager.Setup(x => x.GetRolesAsync(existUser)).ReturnsAsync(new List<string> { existUser.Type });

            ////Act
            var query = new UpdateUserCommand { Location = adminLocation, UpdateModel = updateModel };
        var handler = new UpdateUserCommandHandler(mockUserManager.Object, mockRoleManager.Object);
        var result = await handler.Handle(query, new System.Threading.CancellationToken());

        ////Assert
        Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
        }
}
}
