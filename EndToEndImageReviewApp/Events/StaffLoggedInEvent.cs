using System;
using Prism.Events;

namespace EndToEndImageReviewApp.Events
{
    public class StaffLoggedInEvent 
        : PubSubEvent<StaffLoggedInEventArgs>
    {
    }

    public class StaffLoggedInEventArgs
    {
        public DateTime When { get; set; }

        public Guid StaffId { get; set; }
    }
}
