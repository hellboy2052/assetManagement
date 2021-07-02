using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Services.UserService.Query;
using RookieOnlineAssetManagement.UnitTests.FakeData;
using RookieOnlineAssetManagement.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RookieOnlineAssetManagement.UnitTests.User
{
    public class Get
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



        [Theory]
        [MemberData(nameof(PaginationList))]
        public async void GetAll (string location, PaginationModel userPagination)
        {
            //Arrange           
            var _context = CreateDbContext();

            //Act
            var query = new GetAllUsersQuery { Location = location, PaginationModel = userPagination };
            var handler = new GetAllUsersQueryHandler(_context);
            var result = await handler.Handle(query, new System.Threading.CancellationToken());

            //Assert
            Assert.True(result.IsSuccess);
        }

        public static IEnumerable<object[]> FilterList =>
        new List<object[]>() {
        new object[]
        {   FakeDataObject.UserList()[0].Id,
            new UserFilterModel
            {
                QueryString = "Tran Van",
                Type = new string[] { "Admin", "Staff"},
                PageIndex = 1,
                PageSize = 10,
            }
        },
        new object[]
        {   FakeDataObject.UserList()[1].Id,
            new UserFilterModel
            {
                QueryString = "SD0001",
                Type = new string[] { "Staff" },
                PageIndex = 1,
                PageSize = 10,
            }
        },
        new object[]
        {   FakeDataObject.UserList()[2].Id,
            new UserFilterModel
            {
                QueryString = null,
                Type = new string[] { "Admin", "Staff"},
                PageIndex = 1,
                PageSize = 10,
            } ,
        }
    };
        public static IEnumerable<object[]> PaginationList =>
       new List<object[]>() {
        new object[]
        {   FakeDataObject.UserList()[0].Id,
            new PaginationModel
            {
                PageIndex = 1,
                PageSize = 10,
            }
        },
        new object[]
        {   FakeDataObject.UserList()[2].Id,
            new PaginationModel
            {
                PageIndex = 1,
                PageSize = 10,
            }
        },
   };


        [Theory]
        [MemberData(nameof(FilterList))]
        public async void GetFiltered (string userId, UserFilterModel filterModel)
        {
            //Arrange
            var _context = CreateDbContext();
            //Act
            var query = new GetFilteredUsersQuery { UserId = userId, FilterModel = filterModel };
            var handler = new GetFilteredUsersQueryHandler(_context);
            var result = await handler.Handle(query, new System.Threading.CancellationToken());

            //Assert
            Assert.True(result.IsSuccess);
        }
    }
}
