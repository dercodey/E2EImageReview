
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmzImagingInteractionManager
{
    internal enum EntityType
    {
        Staff,
        Patient,
        Image
    }

    internal static class MsqMappingHelper
    {
        internal static Guid MapIdFromMsq(EntityType forEntityType, int msqEntityId)
        {
            lock (_lock)
            {
                var dict = _mappings[forEntityType];
                if (!dict.ContainsKey(msqEntityId))
                    dict.Add(msqEntityId, Guid.NewGuid());
                return dict[msqEntityId];
            }
        }
        
        // this is an extraneous comment added by me
        // it doesn't add anything

        internal static int MapIdToMsq(EntityType forEntityType, Guid entityId)
        {
            lock (_lock)
            {
                var dict = _mappings[forEntityType];
                var key =
                    from entry in dict
                    where entry.Value.CompareTo(entityId) == 0
                    select entry.Key;
                return key.FirstOrDefault();
            }
        }

        static object _lock = new object();

        static Dictionary<EntityType, Dictionary<int, Guid>> _mappings =
            new Dictionary<EntityType, Dictionary<int, Guid>>()
            {
                { EntityType.Staff, new Dictionary<int,Guid>() },
                { EntityType.Patient, new Dictionary<int,Guid>() },
                { EntityType.Image, new Dictionary<int,Guid>() },
            };
    }
}
