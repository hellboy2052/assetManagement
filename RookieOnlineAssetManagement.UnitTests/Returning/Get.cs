using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Services.ReturningService.Query;
using RookieOnlineAssetManagement.UnitTests.FakeData;
using System;
using Xunit;
using static RookieOnlineAssetManagement.Services.ReturningService.Query.GetAllReturnsQuery;

namespace RookieOnlineAssetManagement.UnitTests.ReturningTest
{
    public class Get
    {
        private static ApplicationDbContext CreateDbContext ()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Categories.AddRange(FakeDataObject.CategoryList());
            dbContext.Assets.AddRange(FakeDataObject.AsssetList());
            dbContext.Users.AddRange(FakeDataObject.UserList());
            dbContext.Roles.AddRange(FakeDataObject.RoleList());
            dbContext.UserRoles.AddRange(FakeDataObject.UserRoleList());
            dbContext.Returning.AddRange(FakeDataObject.ReturningList());
            dbContext.SaveChanges();
            return dbContext;
        }

        [Fact]
        public async void GetAll ()
        {
            //Arrange           
            var query = new GetAllReturnsQuery();
            var handler = new GetAllReturnsQueryHandler(CreateDbContext());
            //Act
            var result = await handler.Handle(query, new System.Threading.CancellationToken());

            //Assert
            Assert.True(result.IsSuccess);
        }
    }
}
