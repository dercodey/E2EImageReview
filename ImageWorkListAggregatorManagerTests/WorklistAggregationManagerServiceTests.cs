using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ImageWorkListAggregatorManager;
using System.ServiceModel;

namespace ImageWorkListAggregatorManager.Tests
{
    [TestClass()]
    public class WorklistAggregationManagerServiceTests
    {
        [ClassInitialize]
        public void StartServices()
        {
            var sh = new ServiceHost(new WorklistAggregationManagerService(), null);
        }

        [TestMethod()]
        public void GetWorklistForStaffAsyncTest()
        {
            var client = new WorklistAggregationManagerService();
            var taskResult = client.GetWorklistForStaffAsync(Guid.Empty);
        }
    }
}