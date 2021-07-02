using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Services.AssignmentService.Query;
using RookieOnlineAssetManagement.UnitTests.FakeData;
using RookieOnlineAssetManagement.ViewModels.Asset;
using RookieOnlineAssetManagement.ViewModels.User;
using System;
using System.Collections.Generic;
using Xunit;

namespace RookieOnlineAssetManagement.UnitTests.AssignmentTest
{
    public class Get
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

        [Theory]
        [MemberData(nameof(PaginationList))]
        public async void GetAll (string location, PaginationModel userPagination)
        {
            //Arrange           
            var query = new GetAllAssignmentQuery { Location = location, PaginationModel = userPagination };
            var handler = new GetAllAssignmentQueryHandler(CreateDbContext());
            //Act
            var result = await handler.Handle(query, new System.Threading.CancellationToken());

            //Assert
            Assert.True(result.IsSuccess);
        }

        [Theory]
        [MemberData(nameof(FilterList))]
        public async void GetFiltered (AssignmentFilterModel filterModel)
        {
            //Arrange           
            var query = new GetFilteredAssignmentsQuery { FilterModel = filterModel };
            var handler = new GetFilteredAssignmentsQueryHandler(CreateDbContext());
            //Act
            var result = await handler.Handle(query, new System.Threading.CancellationToken());

            //Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Value.TotalItems <= filterModel.PageSize);
        }
        public static IEnumerable<object[]> FilterList =>
        new List<object[]>() {
        new object[]
        {
            new AssignmentFilterModel
            {
                QueryString = "LA000000",
                AssignedDate = DateTime.Now,
                States =  new EnumsObject.AssignmentState[] { EnumsObject.AssignmentState.Accepted},
                PageIndex = 1,
                PageSize = 10,
            }
        },
        new object[]
        {
            new AssignmentFilterModel
            {
                QueryString = "PC000000",
                PageIndex = 1,
                PageSize = 10,
            }
        },
    };
        public static IEnumerable<object[]> PaginationList =>
      new List<object[]>() {
        new object[]
        {   "HN",
            new PaginationModel
            {
                PageIndex = 1,
                PageSize = 10,
            }
        },
        new object[]
        {   "HCM",
            new PaginationModel
            {
                PageIndex = 1,
                PageSize = 10,
            }
        },
  };


        [Fact]
        public async void GetUserAssignments_SuccesAndReturnValues_IfUserHasAssignments ()
        {
            //Arrange           
            var query = new GetUserAssignmentsQuery { UserId = "user1" };
            var handler = new GetUserAssignmentsQueryHandler(CreateDbContext());
            //Act
            var result = await handler.Handle(query, new System.Threading.CancellationToken());
            //Assert
            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Value);
        }

        [Fact]
        public async void GetUserAssignments_ReturnEmpty_IfUserDoesntHasAnyAssignments ()
        {
            //Arrange           
            var query = new GetUserAssignmentsQuery { UserId = "user2" };
            var handler = new GetUserAssignmentsQueryHandler(CreateDbContext());
            //Act
            var result = await handler.Handle(query, new System.Threading.CancellationToken());
            //Assert
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Value);
        }

        [Fact]
        public async void GetUserAssignments_Fail_IfUserDoesntExist ()
        {
            //Arrange           
            var query = new GetUserAssignmentsQuery { UserId = "user2345" };
            var handler = new GetUserAssignmentsQueryHandler(CreateDbContext());
            //Act
            var result = await handler.Handle(query, new System.Threading.CancellationToken());
            //Assert
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Value);
        }


    }
}
