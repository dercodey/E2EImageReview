using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndToEndImageReviewApp
{
    public class WorklistItem : BindableBase
    {
        public Guid PatientId { get; set; }

        public string PatientName { get; set; }

        public string MedRc { get; set; }

        public Guid ImageId { get; set; }

        public DateTime AcquisitionDateTime { get; set; }
    }
}
