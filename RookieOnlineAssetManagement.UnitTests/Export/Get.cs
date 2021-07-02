using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Services.Report.Query;
using RookieOnlineAssetManagement.UnitTests.FakeData;
using System;
using Xunit;

namespace RookieOnlineAssetManagement.UnitTests.Export
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
            dbContext.SaveChanges();
            return dbContext;
        }

        [Fact]
        public async void Export_Success ()
        {
            //Arrange           
            var queryReport = new GetReportQuery();
            var context = CreateDbContext();
            context.Returning.AddRange(FakeDataObject.ReturningList());
            var queryReportHandler = new GetReportQueryHandler(context);
            var queryReportFileHandler = new GetReportFileQueryHandler();
            var reportList = await queryReportHandler.Handle(queryReport, new System.Threading.CancellationToken());
            var query = new GetReportFileQuery { reportList = reportList.Value };
            //Act
            var result = await queryReportFileHandler.Handle(query, new System.Threading.CancellationToken());
            //Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async void Export_Fail ()
        {
            //Arrange           
            var context = CreateDbContext();
            var queryReportFileHandler = new GetReportFileQueryHandler();
            var query = new GetReportFileQuery { reportList = null };
            //Act
            var result = await queryReportFileHandler.Handle(query, new System.Threading.CancellationToken());
            //Assert
            Assert.False(result.IsSuccess);
        }
    }
}
