using System;
using System.Runtime.Serialization;

namespace ImagingTypesNetStandard
{
    [DataContract]
    public class ImageData
    {
        [DataMember]
        public Guid ImageId { get; set; }

        /// <summary>
        /// Width of image - may be replace by single ImagePixel structure
        /// </summary>
        [DataMember]
        public int Width { get; set; }

        /// <summary>
        /// Height of image - may be replace by single ImagePixel structure
        /// </summary>
        [DataMember]
        public int Height { get; set; }

        /// <summary>
        /// The images pixels in easily serialized format
        /// </summary>
        [DataMember]
        public byte[] Pixels { get; set; }

        /// <summary>
        /// Helper to form multi-line text string of pixel values
        /// </summary>
        /// <returns></returns>
        public string GetPixelString()
        {
            var sb = new System.Text.StringBuilder();
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    sb.AppendFormat($"{Pixels[y * Width + x]}|");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
