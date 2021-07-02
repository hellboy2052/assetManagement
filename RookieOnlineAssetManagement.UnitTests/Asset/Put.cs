using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Services.AssetService.Command;
using RookieOnlineAssetManagement.UnitTests.FakeData;
using RookieOnlineAssetManagement.ViewModels.Asset;
using System;
using Xunit;
using static RookieOnlineAssetManagement.Services.AssetService.Command.UpdateAssetCommand;

namespace RookieOnlineAssetManagement.UnitTests.AssetTest
{
    public class Put
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
        public async void PutAsset ()
        {
            //Arrange           
            string location = "HCM";
            AssetUpdateModel updateModel = new();
            updateModel.AssetCode = FakeDataObject.AsssetList()[0].AssestCode;
            updateModel.AssestName = "Laptop Gaming v2";
            updateModel.Specification = "GTX 1060ti";
            updateModel.State = EnumsObject.State.NotAvailable;
            updateModel.InstallDate = DateTime.Now;
            var command = new UpdateAssetCommand { Location = location, UpdateModel = updateModel };
            var handler = new UpdateAssetCommandHandler(CreateDbContext());
            //Act
            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            //Assert
            Assert.True(result.IsSuccess);
        }
    }
}
