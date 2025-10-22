using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using CoreWCF;

namespace ImageWorkListAggregatorManager.Tests
{
    [TestClass]
    public class WorklistAggregationManagerServiceTests
    {
        private WorklistAggregationManagerService _service;

        [TestInitialize]
        public void Initialize()
        {
            _service = new WorklistAggregationManagerService();
        }

        [TestMethod]
        public async Task GetWorklistForStaffAsyncTest()
        {
            // Act
            var result = await _service.GetWorklistForStaffAsync(Guid.Empty);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}