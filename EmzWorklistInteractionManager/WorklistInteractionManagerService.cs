﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace EmzWorklistInteractionManager
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class WorklistInteractionManagerService : IWorklistInteractionManager
    {
        public async Task<List<WorklistItem>> GetWorklistForStaff(Guid staffId)
        {
            var msqStaffId = MsqMappingHelper.MapStaff(staffId);

            var client = new MsqWorklistService.MsqWorklistServiceClient();
            var msqWorklist = await client.GetWorklistForStaffAsync(msqStaffId);
            client.Close();

            var worklist =
                msqWorklist.Select(item =>
                new WorklistItem()
                {
                    PatientId = MsqMappingHelper.MapPatient(item.MsqPatId1),
                    ImageId = Guid.Empty,
                    AcquisitionDateTime = item.AcquisitionDateTime
                });

            // convert staff Id to 
            return worklist.ToList();
        }
    }
}