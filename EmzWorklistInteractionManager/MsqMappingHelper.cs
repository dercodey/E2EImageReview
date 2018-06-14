
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmzWorklistInteractionManager
{
    internal static class MsqMappingHelper
    {
        internal static Guid MapStaff(int staffId)
        {
            return Guid.NewGuid();
        }

        internal static int MapStaff(Guid staffId)
        {
            return 0;
        }

        internal static Guid MapPatient(int patientId)
        {
            return Guid.NewGuid();
        }

        internal static int MapPatient(Guid patientId)
        {
            return patientId.ToByteArray()[0] << 8 + patientId.ToByteArray()[1];
        }

        internal static Guid MapImage(int imageId)
        {
            return Guid.NewGuid();
        }

        internal static int MapImage(Guid imageId)
        {
            return imageId.ToByteArray()[0] << 8 + imageId.ToByteArray()[1];
        }
    }
}
