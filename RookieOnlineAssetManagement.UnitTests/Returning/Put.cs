using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Services.ReturningService.Command;
using RookieOnlineAssetManagement.UnitTests.FakeData;
using RookieOnlineAssetManagement.ViewModels.Return;
using System;
using Xunit;
using static RookieOnlineAssetManagement.Services.ReturningService.Command.UpdateReturnStateCommand;

namespace RookieOnlineAssetManagement.UnitTests.ReturningTest
{
    public class Put
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
            dbContext.Returning.AddRange(FakeDataObject.ReturningList());
            dbContext.SaveChanges();
            return dbContext;
        }

        [Fact]
        public async void PutReturning_Success ()
        {
            //Arrange           
            string UserId = "admin1";
            var updateModel = new ReturnStateUpdateModel { ReturnId = "RETURNING2" };
            var command = new UpdateReturnStateCommand { returnStateUpdateModel = updateModel, UserId = UserId };
            var handler = new UpdateReturnStateCommandHandler(CreateDbContext());
            //Act
            var result = await handler.Handle(command, new System.Threading.CancellationToken());
            //Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async void PutReturning_Fail_IfReturningIdNotExist ()
        {
            //Arrange           
            string UserId = "admin1";
            var updateModel = new ReturnStateUpdateModel { ReturnId = "alualu" };
            var command = new UpdateReturnStateCommand { returnStateUpdateModel = updateModel, UserId = UserId };
            var handler = new UpdateReturnStateCommandHandler(CreateDbContext());
            //Act
            var result = await handler.Handle(command, new System.Threading.CancellationToken());
            //Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async void PutReturning_Fail_IfReturningIsNotWaitingForReturning ()
        {
            //Arrange           
            string UserId = "admin1";
            var updateModel = new ReturnStateUpdateModel { ReturnId = "RETURNING1" };
            var command = new UpdateReturnStateCommand { returnStateUpdateModel = updateModel, UserId = UserId };
            var handler = new UpdateReturnStateCommandHandler(CreateDbContext());
            //Act
            var result = await handler.Handle(command, new System.Threading.CancellationToken());
            //Assert
            Assert.False(result.IsSuccess);
        }
    }
}
