using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

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
