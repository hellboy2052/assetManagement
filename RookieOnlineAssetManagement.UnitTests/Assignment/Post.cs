using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Services.AssignmentService.Command;
using RookieOnlineAssetManagement.UnitTests.FakeData;
using RookieOnlineAssetManagement.ViewModels.Assignment;
using System;
using Xunit;

namespace RookieOnlineAssetManagement.UnitTests.AssignmentTest
{
    public class Post
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
        public async void PostAssignment ()
        {
            //Arrange           
            string adminId = FakeDataObject.UserList()[1].Id;
            string staffId = FakeDataObject.UserList()[0].Id;
            AssignmentCreateModel createModel = new();
            createModel.AssestCode = FakeDataObject.AsssetList()[0].AssestCode;
            createModel.Note = "This is a Note from Post method";
            createModel.UserId = staffId;
            createModel.AssignedDate = DateTime.Now;
            var command = new CreateAssignmentCommand { AdminId = adminId, CreateModel = createModel };
            var handler = new CreateAssignmentCommandHandler(CreateDbContext());
            //Act
            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            //Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async void PostAssignment_Fail_WhenAdminIdNotExist ()
        {
            //Arrange           
            string adminId = "NotExistInDB";
            string staffId = FakeDataObject.UserList()[0].Id;
            AssignmentCreateModel createModel = new();
            createModel.AssestCode = FakeDataObject.AsssetList()[0].AssestCode;
            createModel.Note = "This is a Note from Post method";
            createModel.UserId = staffId;
            createModel.AssignedDate = DateTime.Now;
            var command = new CreateAssignmentCommand { AdminId = adminId, CreateModel = createModel };
            var handler = new CreateAssignmentCommandHandler(CreateDbContext());
            //Act
            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Contains($"Admin id : {adminId} not exist", result.Error);
        }

        [Fact]
        public async void PostAssignment_Fail_WhenStaffIdNotExist ()
        {
            //Arrange           
            string adminId = FakeDataObject.UserList()[1].Id;
            string staffId = "NotExistInDB";
            AssignmentCreateModel createModel = new();
            createModel.AssestCode = FakeDataObject.AsssetList()[0].AssestCode;
            createModel.Note = "This is a Note from Post method";
            createModel.UserId = staffId;
            createModel.AssignedDate = DateTime.Now;
            var command = new CreateAssignmentCommand { AdminId = adminId, CreateModel = createModel };
            var handler = new CreateAssignmentCommandHandler(CreateDbContext());
            //Act
            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Contains($"User id : {staffId} not exist", result.Error);
        }

        [Fact]
        public async void PostAssignment_Fail_WhenAssignmentIdNotExist ()
        {
            //Arrange           
            string adminId = FakeDataObject.UserList()[1].Id;
            string staffId = FakeDataObject.UserList()[0].Id;
            AssignmentCreateModel createModel = new();
            createModel.AssestCode = "NotExistInDB";
            createModel.Note = "This is a Note from Post method";
            createModel.UserId = staffId;
            createModel.AssignedDate = DateTime.Now;
            var command = new CreateAssignmentCommand { AdminId = adminId, CreateModel = createModel };
            var handler = new CreateAssignmentCommandHandler(CreateDbContext());
            //Act
            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Contains($"Asset code : { createModel.AssestCode} not exist", result.Error);
        }
    }
}
