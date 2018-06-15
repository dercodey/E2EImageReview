using System;

using Prism.Events;

namespace EndToEndImageReviewApp.Events
{
    public class WorklistItemSelectedEvent 
        : PubSubEvent<WorklistItemSelectedEventArgs>
    {
    }

    public class WorklistItemSelectedEventArgs
    {
        public Guid ImageId { get; set; }

        public DateTime AcquisitionDateTime { get; set; }
    }
}