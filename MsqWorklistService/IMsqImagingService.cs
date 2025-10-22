using System.Collections.Generic;
using System.Runtime.Serialization;
using CoreWCF;

namespace MsqWorklistService
{
    [ServiceContract]
    public interface IMsqImagingService
    {
        [OperationContract]
        List<MsqWorklistItem> GetWorklistForStaff(int staffId);

        [OperationContract]
        MsqImageInfo GetImageInfo(int imgId);

        [OperationContract]
        MsqImageData LoadImageData(int imgId);
    }
}
