using System;
using System.Runtime.Serialization;

namespace ImagingTypes
{
    [DataContract]
    public class ImageData
    {
        [DataMember]
        public Guid ImageId { get; set; }

        [DataMember]
        public int Width { get; set; }

        [DataMember]
        public int Height { get; set; }

        [DataMember]
        public byte[] Pixels { get; set; }
    }

}
