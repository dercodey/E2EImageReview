using System.Runtime.Serialization;

namespace MsqWorklistService
{
    [DataContract]
    public class MsqImageData
    {
        [DataMember]
        public int MsqImgId { get; set; }

        [DataMember]
        public int Width { get; set; }

        [DataMember]
        public int Height { get; set; }

        [DataMember]
        public byte[] Pixels { get; set; }
    }
}
