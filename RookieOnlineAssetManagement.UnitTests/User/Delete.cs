using Microsoft.EntityFrameworkCore;
using Moq;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Services.UserService.Command;
using RookieOnlineAssetManagement.UnitTests.FakeData;
using System;
using System.Linq;
using Xunit;

namespace RookieOnlineAssetManagement.UnitTests.User
{
    public class Delete
    {
        private ApplicationDbContext CreateDbContext ()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Users.AddRange(FakeDataObject.UserList().AsQueryable());
            dbContext.SaveChanges();
            return dbContext;
        }

        [Fact]
        public async void DeleteUser_Success_IfAdminLocationIsTheSameWithTheUserBeDelete ()
        {
            //Arrange
            var mockUserManager = new Mock<FakeUserManager>();
            var mockRoleManager = new Mock<FakeRoleManager>();
            var _context = CreateDbContext();
            mockUserManager.Setup(x => x.Users).Returns(_context.Users);
            mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(_context.Users.FirstOrDefault(x => x.Id == "user1"));
            ////Act
            var command = new DeleteUserCommand { Location = "HCM", Id = "user1" };
            var handler = new DeleteUserCommandHandler(mockUserManager.Object, _context);
            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            ////Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async void DeleteUser_Fail_IfUserIsAlreadyAssignedToAssignment()
        {
            //Arrange
            var mockUserManager = new Mock<FakeUserManager>();
            var mockRoleManager = new Mock<FakeRoleManager>();
            var _context = CreateDbContext();
            mockUserManager.Setup(x => x.Users).Returns(_context.Users);
            mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(_context.Users.FirstOrDefault(x => x.Id == "user2"));
            ////Act
            var command = new DeleteUserCommand { Location = "HN", Id = "user2" };
            var handler = new DeleteUserCommandHandler(mockUserManager.Object, _context);
            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            ////Assert
            Assert.False(result.IsSuccess);
        }


        [Fact]
        public async void DeleteUser_Fail_IfAdminLocationIsNotTheSameWithTheUserBeDelete ()
        {
            //Arrange
            var mockUserManager = new Mock<FakeUserManager>();
            var mockRoleManager = new Mock<FakeRoleManager>();
            var _context = CreateDbContext();
            mockUserManager.Setup(x => x.Users).Returns(_context.Users);
            ////Act
            var command = new DeleteUserCommand { Location = "HN", Id = "user1" };
            var handler = new DeleteUserCommandHandler(mockUserManager.Object, _context);
            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            ////Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async void DeleteUser_False_UserIdNotExistInDB ()
        {
            //Arrange
            var mockUserManager = new Mock<FakeUserManager>();
            var mockRoleManager = new Mock<FakeRoleManager>();
            var _context = CreateDbContext();
            mockUserManager.Setup(x => x.Users).Returns(_context.Users);
            ////Act
            var command = new DeleteUserCommand { Location = "HCM", Id = "user1" };
            var handler = new DeleteUserCommandHandler(mockUserManager.Object, _context);
            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            ////Assert
            Assert.False(result.IsSuccess);
        }
    }
}
