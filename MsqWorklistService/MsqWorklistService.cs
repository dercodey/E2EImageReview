using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MsqWorklistService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class MsqWorklistService : IMsqWorklistService
    {
        public List<MsqWorklistItem> GetWorklistForStaff(int staffId)
        {
            return new List<MsqWorklistItem>
            {
                new MsqWorklistItem() { MsqPatId1 = 10001, MsqImgId = 9000, AcquisitionDateTime = DateTime.Now.AddDays(-10), },
                new MsqWorklistItem() { MsqPatId1 = 10001, MsqImgId = 9001, AcquisitionDateTime = DateTime.Now.AddDays(-9), },
                new MsqWorklistItem() { MsqPatId1 = 10001, MsqImgId = 9002, AcquisitionDateTime = DateTime.Now.AddDays(-8), },
                new MsqWorklistItem() { MsqPatId1 = 10001, MsqImgId = 9003, AcquisitionDateTime = DateTime.Now.AddDays(-7), },
            };
        }
    }
}
