using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Services.ReturningService.Command;
using RookieOnlineAssetManagement.UnitTests.FakeData;
using RookieOnlineAssetManagement.ViewModels.Return;
using System;
using Xunit;
using static RookieOnlineAssetManagement.Services.ReturningService.Command.CreateReturnCommand;

namespace RookieOnlineAssetManagement.UnitTests.ReturningTest
{
    public class Post
    {
        private static ApplicationDbContext CreateDbContext ()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Categories.AddRange(FakeDataObject.CategoryList());
            dbContext.Assignments.AddRange(FakeDataObject.AssignmentList());
            dbContext.Assets.AddRange(FakeDataObject.AsssetList());
            dbContext.Users.AddRange(FakeDataObject.UserList());
            dbContext.SaveChanges();
            return dbContext;
        }

        [Fact]
        public async void PostReturning_Success ()
        {
            //Arrange           
            string adminId = "admin1";
            var createModel = new ReturnCreateModel { AssignmentId = "ASIGNMENT2" };
            var command = new CreateReturnCommand { CreateModel = createModel, UserId = adminId };
            var handler = new CreateReturnCommandHandler(CreateDbContext());
            //Act
            var result = await handler.Handle(command, new System.Threading.CancellationToken());
            //Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async void PostReturning_Fail_IfAssignmentIdNotExist ()
        {
            //Arrange           
            string adminId = "admin1";
            ReturnCreateModel createModel = new ReturnCreateModel { AssignmentId = "ALUALU" };
            var command = new CreateReturnCommand { CreateModel = createModel, UserId = adminId };
            var handler = new CreateReturnCommandHandler(CreateDbContext());
            //Act
            var result = await handler.Handle(command, new System.Threading.CancellationToken());
            //Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async void PostReturning_Fail_IfUserIdNotExist ()
        {
            //Arrange           
            string adminId = "alualu";
            ReturnCreateModel createModel = new ReturnCreateModel { AssignmentId = "ASIGNMENT1" };
            var command = new CreateReturnCommand { CreateModel = createModel, UserId = adminId };
            var handler = new CreateReturnCommandHandler(CreateDbContext());
            //Act
            var result = await handler.Handle(command, new System.Threading.CancellationToken());
            //Assert
            Assert.False(result.IsSuccess);
        }
    }
}
