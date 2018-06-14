
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmzWorklistInteractionManager
{
    internal static class MsqMappingHelper
    {
        static Guid MapStaff(int staffId)
        {
            return Guid.NewGuid();
        }

        static int MapStaff(Guid staffId)
        {
            return 0;
        }

        static Guid MapPatient(int patientId)
        {
            return Guid.NewGuid();
        }

        static int MapPatient(Guid patientId)
        {
            return patientId.ToByteArray()[0] << 8 + patientId.ToByteArray()[1];
        }
    }
}
