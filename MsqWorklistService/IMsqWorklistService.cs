using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MsqWorklistService
{
    [ServiceContract]
    public interface IMsqWorklistService
    {
        [OperationContract]
        List<MsqWorklistItem> GetWorklistForStaff(int staffId);

        [OperationContract]
        MsqImageInfo GetImageInfo(int imgId);

        [OperationContract]
        MsqImageData LoadImageData(int imgId);
    }

    [DataContract]
    public class MsqWorklistItem
    {
        [DataMember]
        public int MsqPatId1 { get; set; }

        [DataMember]
        public string PatientName { get; set; }

        [DataMember]
        public int MsqImgId { get; set; }

        [DataMember]
        public DateTime AcquisitionDateTime { get; set; }
    }

    [DataContract]
    public class MsqImageInfo
    {
        [DataMember]
        public int MsqPatId1 { get; set; }

        [DataMember]
        public string PatientName { get; set; }

        [DataMember]
        public int MsqImgId { get; set; }

        [DataMember]
        public DateTime AcquisitionDateTime { get; set; }

        [DataMember]
        public int? MsqReferenceImgId { get; set; }

        [DataMember]
        public int? MsqRegistrationId { get; set; }
    }


    [DataContract]
    public class MsqImageData
    {
        [DataMember]
        public int MsqImgId { get; set; }

        [DataMember]
        public int Width { get; set; }

        [DataMember]
        public int Height { get; set; }

        [DataMember]
        public byte[] Pixels { get; set; }


    }
}
