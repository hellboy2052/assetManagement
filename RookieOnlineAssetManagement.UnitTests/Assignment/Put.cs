using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Services.AssignmentService.Command;
using RookieOnlineAssetManagement.UnitTests.FakeData;
using RookieOnlineAssetManagement.ViewModels.Assignment;
using System;
using Xunit;

namespace RookieOnlineAssetManagement.UnitTests.AssignmentTest
{
    public class Put
    {
        private static ApplicationDbContext CreateDbContext ()
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
        public async void PutAssignment ()
        {
            //Arrange           
            string adminId = FakeDataObject.UserList()[1].Id;
            var staff = FakeDataObject.UserList()[0];
            string assignmentId = FakeDataObject.AssignmentList()[0].AssignmentId;
            AssignmentUpdateModel updateModel = new();
            updateModel.AssestCode = FakeDataObject.AsssetList()[0].AssestCode;
            updateModel.Note = "This is a Note from Post method";
            updateModel.AssignmentId = assignmentId;
            updateModel.UserId = staff.Id;
            updateModel.AssignedDate = DateTime.Now;
            var command = new UpdateAssignmentCommand { AdminId = adminId, UpdateModel = updateModel };
            var handler = new UpdateAssignmentCommandHandler(CreateDbContext());
            //Act
            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            //Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Value.AssignedTo == staff.UserName);
        }

        [Fact]
        public async void PutAssignment_Fail_WhenAssignmentIdNotExist ()
        {
            //Arrange           
            string adminId = FakeDataObject.UserList()[1].Id;
            string assignmentId = "NotExistInDB";
            AssignmentUpdateModel updateModel = new() { AssignmentId = assignmentId };
            var command = new UpdateAssignmentCommand { AdminId = adminId, UpdateModel = updateModel };
            var handler = new UpdateAssignmentCommandHandler(CreateDbContext());
            //Act
            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Contains($"Assignment id : {assignmentId} not exist", result.Error);
        }

        [Fact]
        public async void PutAssignment_Fail_WhenAssignmentStateIsNotWaitingForAcceptance ()
        {
            //Arrange           
            string adminId = "NotExistInDB";
            string assignmentId = FakeDataObject.AssignmentList()[1].AssignmentId;
            AssignmentUpdateModel updateModel = new() { AssignmentId = assignmentId };
            var command = new UpdateAssignmentCommand { AdminId = adminId, UpdateModel = updateModel };
            var handler = new UpdateAssignmentCommandHandler(CreateDbContext());
            //Act
            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Only assignments have state waiting for acceptance is able to update", result.Error);
        }

        [Fact]
        public async void PutAssignment_Fail_WhenUserIdNotExist ()
        {
            //Arrange           
            string adminId = FakeDataObject.UserList()[1].Id;
            var staffId = "NotExistInDB";
            string assignmentId = FakeDataObject.AssignmentList()[0].AssignmentId;
            AssignmentUpdateModel updateModel = new() { AssignmentId = assignmentId, UserId = staffId };
            var command = new UpdateAssignmentCommand { AdminId = adminId, UpdateModel = updateModel };
            var handler = new UpdateAssignmentCommandHandler(CreateDbContext());
            //Act
            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Contains($"User id : {staffId} not exist", result.Error);
        }
    }
}
