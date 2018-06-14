using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace EmzWorklistInteractionManager
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IWorklistInteractionManager
    {
        [OperationContract]
        List<WorklistItem> GetWorklistForStaff(Guid staffId);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "EmzWorklistInteractionManager.ContractType".
    [DataContract]
    public class WorklistItem
    {
        [DataMember]
        public Guid PatientId { get; set; }

        [DataMember]
        public Guid ImageId { get; set; }
    }
}
