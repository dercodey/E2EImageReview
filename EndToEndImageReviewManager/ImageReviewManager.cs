using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace EndToEndImageReviewManager
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ImageReviewManager : IImageReviewManager
    {
        public ImageInfo GetImageInfo(Guid imageId)
        {
            throw new NotImplementedException();
        }

        public ImageReviewResponse ReviewImage(ImageReviewRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
