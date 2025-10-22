using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using CoreWCF;

namespace ImageWorkListAggregatorManager.Tests
{
    [TestClass]
    public class WorklistAggregationManagerServiceTests
    {
        private WebApplicationFactory<Program> _factory;

        [TestInitialize]
        public void Initialize()
        {
            _factory = new WebApplicationFactory<Program>()
        .WithWebHostBuilder(builder =>
         {
          builder.ConfigureServices(services =>
          {
 services.AddServiceModelServices();
         services.AddServiceModelMetadata();
       services.AddSingleton<WorklistAggregationManagerService>();
   });
     });
        }

 [TestCleanup]
        public void Cleanup()
     {
    _factory?.Dispose();
        }

        [TestMethod]
        public async Task GetWorklistForStaffAsyncTest()
        {
            // Arrange
            var service = _factory.Services.GetRequiredService<WorklistAggregationManagerService>();

            // Act
            var result = await service.GetWorklistForStaffAsync(Guid.Empty);

            // Assert
            Assert.IsNotNull(result);
     }
    }
}