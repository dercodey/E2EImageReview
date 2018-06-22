using System;
using System.Runtime.Serialization;

namespace MsqWorklistService
{
    [DataContract]
    public class MsqWorklistItem
    {
        [DataMember]
        public int MsqPatId1 { get; set; }

        [DataMember]
        public string PatientName { get; set; }

        [DataMember]
        public string PatientMedRc { get; set; }

        [DataMember]
        public int MsqImgId { get; set; }

        [DataMember]
        public DateTime AcquisitionDateTime { get; set; }
    }
}
