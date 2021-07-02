using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Services.AssetService.Command;
using RookieOnlineAssetManagement.UnitTests.FakeData;
using System;
using System.Diagnostics;
using Xunit;
using static RookieOnlineAssetManagement.Services.AssetService.Command.DeleteAssetCommand;

namespace RookieOnlineAssetManagement.UnitTests.AssetTest
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
            dbContext.Assets.AddRange(FakeDataObject.AsssetList());
            dbContext.SaveChanges();
            return dbContext;
        }

        [Fact]
        public async void DeleteAsset_Success ()
        {
            //Arrange           
            var location = "HCM";
            var command = new DeleteAssetCommand { Location = location, AssetCode = FakeDataObject.AsssetList()[0].AssestCode };
            var handler = new DeleteAssetCommandHandler(CreateDbContext());
            //Act
            var result = await handler.Handle(command, new System.Threading.CancellationToken());
            //Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async void DeleteAsset_Failed ()
        {
            Trace.WriteLine("asdasdjaslkdj");
            //Arrange           
            var location = "HN"; // Trường hợp Admin khác Location với Asset 
            var command = new DeleteAssetCommand { Location = location, AssetCode = FakeDataObject.AsssetList()[0].AssestCode };
            var handler = new DeleteAssetCommandHandler(CreateDbContext());
            //Act
            var result = await handler.Handle(command, new System.Threading.CancellationToken());
            //Assert
            Assert.False(result.IsSuccess);
        }
    }
}
