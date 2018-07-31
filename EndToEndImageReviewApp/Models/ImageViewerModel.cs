using System;
using System.Text;
using System.Threading.Tasks;

using ImageReviewManager.Contracts;
using ImagingTypes;

namespace EndToEndImageReviewApp
{
    public class ImageViewerModel
    {
        public async Task<string> GetImageInfoAsync(Guid imageId, DateTime acquisitionDateTime)
        {
            var client = ProxyManager.ProxyManager.Instance.CreateProxy<IImageReviewManager>();
            var imageInfo = await client.GetImageInfoAsync(imageId);
            ProxyManager.ProxyManager.Instance.CloseProxy(client);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Image loaded:");
            sb.AppendLine($"ImageId = {imageId}");
            sb.AppendLine($"When = {acquisitionDateTime}");
            sb.AppendLine($"Patient Name = {imageInfo.PatientName}");
            return sb.ToString();
        }

        public async Task<ImageData> ReviewImageAsync(Guid imageId)
        {
            var client = ProxyManager.ProxyManager.Instance.CreateProxy<IImageReviewManager>();
            var response = await client.ReviewImageAsync(new ImageReviewRequest() { ImageId = imageId });
            ProxyManager.ProxyManager.Instance.CloseProxy(client);

            return response.DailyImage;
        }
    }
}
