using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Services.AssetService.Command;
using RookieOnlineAssetManagement.UnitTests.FakeData;
using RookieOnlineAssetManagement.ViewModels.Asset;
using System;
using Xunit;
using static RookieOnlineAssetManagement.Services.AssetService.Command.CreateAssetCommand;

namespace RookieOnlineAssetManagement.UnitTests.AssetTest
{
    public class Post
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
        public async void PostAsset ()
        {
            //Arrange           
            string location = "HCM";
            AssetCreateModel createModel = new();
            createModel.AssestName = "Laptop Gaming";
            createModel.Specification = "GTX 1060";
            createModel.State = EnumsObject.State.Available;
            createModel.InstallDate = DateTime.Now;
            createModel.CategoryId = "LA";
            var command = new CreateAssetCommand { Location = location, CreateModel = createModel };
            var handler = new CreateAssetCommandHandler(CreateDbContext());
            //Act
            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            //Assert
            Assert.True(result.IsSuccess);
        }
    }
}
