using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Services.AssignmentService.Command;
using RookieOnlineAssetManagement.UnitTests.FakeData;
using System;
using Xunit;

namespace RookieOnlineAssetManagement.UnitTests.AssignmentTest
{
    public class Delete
    {
        private static ApplicationDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Users.AddRange(FakeDataObject.UserList());
            dbContext.Assets.AddRange(FakeDataObject.AsssetList());
            dbContext.Assignments.AddRange(FakeDataObject.AssignmentList());
            dbContext.SaveChanges();
            return dbContext;
        }

        [Fact]
        public async void DeleteAssignment_Success_WhenAssignmentIsWaitingForAcceptance ()
        {
            //Arrange           
            var assignmentId = "ASIGNMENT1";
            var command = new DeleteAssignmentCommand { Id = assignmentId };
            var handler = new DeleteAssignmentCommandHandler(CreateDbContext());
            //Act
            var result = await handler.Handle(command, new System.Threading.CancellationToken());
            //Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async void DeleteAssignment_Failed_WhenAdminLocationIsNotTheSameWithAsset()
        {
            //Arrange           
            var assignmentId = "ASIGNMENT2";
            var command = new DeleteAssignmentCommand { Id = assignmentId };
            var handler = new DeleteAssignmentCommandHandler(CreateDbContext());
            //Act
            var result = await handler.Handle(command, new System.Threading.CancellationToken());
            //Assert
            Assert.False(result.IsSuccess);
        }
    }
}
