using System;
using System.Runtime.Serialization;

namespace ImagingTypes
{
    [DataContract]
    public class ImageInfo
    {
        [DataMember]
        public Guid PatientId { get; set; }

        [DataMember]
        public string PatientName { get; set; }

        [DataMember]
        public string PatientMedRc { get; set; }

        [DataMember]
        public Guid ImageId { get; set; }

        [DataMember]
        public DateTime AcquisitionDateTime { get; set; }
    }
}
