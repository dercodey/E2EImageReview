using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

using ImagingTypes;

namespace ImageWorkListAggregatorManager
{
    [ServiceContract]
    public interface IWorklistAggregationManager
    {
        [OperationContract]
        Task<List<WorklistItem>> GetWorklistForStaffAsync(Guid staffId);
    }
}
