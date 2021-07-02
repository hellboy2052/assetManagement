using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Services.ReturningService.Command;
using RookieOnlineAssetManagement.UnitTests.FakeData;
using System;
using Xunit;

namespace RookieOnlineAssetManagement.UnitTests.ReturningTest
{
    public class Delete
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
        public async void DeleteReturning_Success ()
        {
            //Arrange           
            var command = new DeleteReturnCommand { Id = "RETURNING2" };
            var handler = new DeleteReturnCommandHandler(CreateDbContext());
            //Act
            var result = await handler.Handle(command, new System.Threading.CancellationToken());
            //Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async void DeleteReturning_Failed_WhenReturningIdNotExist ()
        {
            //Arrange           
            var command = new DeleteReturnCommand { Id = "RETURNING123" };
            var handler = new DeleteReturnCommandHandler(CreateDbContext());
            //Act
            var result = await handler.Handle(command, new System.Threading.CancellationToken());
            //Assert
            Assert.False(result.IsSuccess);
        }
        [Fact]
        public async void DeleteReturning_Failed_WhenReturningIsNotWaitingForAccpetance ()
        {
            //Arrange           
            var command = new DeleteReturnCommand { Id = "RETURNING123" };
            var handler = new DeleteReturnCommandHandler(CreateDbContext());
            //Act
            var result = await handler.Handle(command, new System.Threading.CancellationToken());
            //Assert
            Assert.False(result.IsSuccess);
        }
    }
}
