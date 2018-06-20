using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ImageWorkListAggregatorManager
{
    [ServiceContract]
    public interface IWorklistAggregationManager
    {
        [OperationContract]
        Task<List<WorklistItem>> GetWorklistForStaffAsync(Guid staffId);
    }

    public interface IWorklistAggregationManagerWithSync : IWorklistAggregationManager
    {
        List<WorklistItem> GetWorklistForStaff(Guid staffId);
    }

    [DataContract]
    public class WorklistItem
    {
        [DataMember]
        public Guid PatientId { get; set; }

        [DataMember]
        public string PatientName { get; set; }

        [DataMember]
        public Guid ImageId { get; set; }

        [DataMember]
        public DateTime AcquisitionDateTime { get; set; }
    }
}
