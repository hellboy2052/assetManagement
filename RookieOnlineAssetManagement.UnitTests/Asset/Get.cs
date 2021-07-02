using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Services.AssetService.Query;
using RookieOnlineAssetManagement.UnitTests.FakeData;
using RookieOnlineAssetManagement.ViewModels.Asset;
using RookieOnlineAssetManagement.ViewModels.User;
using System;
using System.Collections.Generic;
using Xunit;
using static RookieOnlineAssetManagement.Services.AssetService.Query.GetAllAssetsQuery;
using static RookieOnlineAssetManagement.Services.AssetService.Query.GetFilteredAssetsQuery;

namespace RookieOnlineAssetManagement.UnitTests.AssetTest
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
            dbContext.SaveChanges();
            return dbContext;
        }

        [Theory]
        [MemberData(nameof(PaginationList))]
        public async void GetAll (string location, PaginationModel userPagination)
        {
            //Arrange           
            var query = new GetAllAssetsQuery { Location = location, PaginationModel = userPagination };
            var handler = new GetAllAssetsQueryHandler(CreateDbContext());
            //Act
            var result = await handler.Handle(query, new System.Threading.CancellationToken());

            //Assert
            Assert.True(result.IsSuccess);
        }

        [Theory]
        [MemberData(nameof(FilterList))]
        public async void GetFiltered (string location, AssetFilterModel filterModel)
        {
            //Arrange           
            var query = new GetFilteredAssetsQuery { Location = location, FilterModel = filterModel };
            var handler = new GetFilteredAssetsQueryHandler(CreateDbContext());
            //Act
            var result = await handler.Handle(query, new System.Threading.CancellationToken());

            //Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Value.TotalItems <= filterModel.PageSize);
        }
        public static IEnumerable<object[]> FilterList =>
        new List<object[]>() {
        new object[]
        {   "HCM",
            new AssetFilterModel
            {
                QueryString = "LA000000",
                Category = new string []{ "Laptop","Personal Computer" },
                States =  new EnumsObject.State[] { EnumsObject.State.Assigned },
                PageIndex = 1,
                PageSize = 10,
            }
        },
        new object[]
        {   "HCM",
            new AssetFilterModel
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

    }
}
