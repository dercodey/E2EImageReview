using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ImageWorkListAggregatorManager;

namespace ImageWorkListAggregatorManager.Tests
{
    [TestClass()]
    public class WorklistAggregationManagerServiceTests
    {
        [TestMethod()]
        public void GetWorklistForStaffAsyncTest()
        {
            var client = new WorklistAggregationManagerService();
            var taskResult = client.GetWorklistForStaffAsync(Guid.Empty);

            Assert.Fail();
        }
    }
}