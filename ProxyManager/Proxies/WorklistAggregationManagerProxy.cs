using ImageWorkListAggregatorManager.Contracts;
using ImagingTypes;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace ProxyManager
{
    internal class WorklistAggregationManagerProxy : ClientBase<IWorklistAggregationManager>, IWorklistAggregationManager
    {
        protected static Binding _binding = new BasicHttpBinding();
        protected static EndpointAddress _endpointAddress = new EndpointAddress("http://localhost:8733/Design_Time_Addresses/ImageWorkListAggregatorManager/Service1/");
        
        public WorklistAggregationManagerProxy()
            : base(_binding, _endpointAddress)
        {
        }

        public Task<List<WorklistItem>> GetWorklistForStaffAsync(Guid staffId) =>
            Channel.GetWorklistForStaffAsync(staffId);
    }
}