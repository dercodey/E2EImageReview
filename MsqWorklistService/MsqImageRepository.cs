using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsqWorklistService
{
    static class MsqImageRepository
    {
        static MsqImageRepository()
        {
            // randomly select how many patients
            int patientCount = _rand.Next(3, 10);

            IEnumerable<int> patientIds =
                Enumerable.Range(1, patientCount)
                    .Select(i => 90000 + i);   // patient ID = i + 90000

            _patientData =
                (from patientId in patientIds
                select new MsqPatientData()
                {
                    PatientId = patientId,
                    PatientName = GeneratePatientName(),
                    MedRc = $"MRN{patientId}",
                    OnTreatment = DateTime.Now.AddDays(-_rand.Next(40)),
                }).ToList();

            _imageData =
                _patientData
                    .SelectMany(patient =>
                        Enumerable.Range(1, _rand.Next(1, 30))
                            .Select(i => new MsqImageRepositoryData()
                            {
                                PatientId = patient.PatientId,
                                AcquisitionDateTime = DateTime.Now.AddDays(-i),
                                Pixels = _defaultPixels,
                            }))
                            .ToList();

            int imageId = 10001;
            foreach (var imageData in _imageData)
            {
                imageData.ImageId = imageId;
                imageId++;
            }

            foreach (var imageData in _imageData)
            {
                // find earliest image for patient
                var imagesForPatientOrdered =
                    from rid in _imageData
                    where (rid.PatientId == imageData.PatientId)
                    orderby rid.AcquisitionDateTime
                    select rid;

                var referenceImageData = 
                    imagesForPatientOrdered.First();

                if (imageData.ImageId != referenceImageData.ImageId)
                {
                    imageData.ReferenceImageId = referenceImageData.ImageId;
                }                
            }
        }

        public static IEnumerable<MsqPatientData> GetAllPatientData()
        {
            return _patientData;
        }

        public static IEnumerable<MsqImageRepositoryData> GetAllImageData()
        {
            return _imageData;
        }

        static byte[,] _defaultPixels = new byte[,] { { 0, 1, 1 }, { 1, 0, 1 }, { 1, 0, 0 } };

        static Random _rand = new Random();

        static string[] _randomFirstNames = new string[] { "Sally", "Mary", "Sammy", "Johnny", "Billy", "Audrey" };
        static string[] _randomLastNames = new string[] { "O'Mally", "O'Leary", "O'Wammy", "Jones", "Merstad", "Vennidad" };

        static string GeneratePatientName() =>
            $"{_randomLastNames[_rand.Next(0, _randomFirstNames.Length - 1)]}, " +
                $"{_randomFirstNames[_rand.Next(0, _randomFirstNames.Length - 1)]}";

        static IEnumerable<MsqPatientData> _patientData
#if STATIC_DATA_POPULATION
            = new List<MsqPatientData>()
            {
                new MsqPatientData() { PatientId = 10001, PatientName = "Sally, OMally"},
                new MsqPatientData() { PatientId = 10002, PatientName = "Johnny, OTonny"},
                new MsqPatientData() { PatientId = 10003, PatientName = "Mary, OLeary"},
                new MsqPatientData() { PatientId = 10004, PatientName = "Sammy, OWammy"},
            }
#endif
            ;

        static IEnumerable<MsqImageRepositoryData> _imageData
#if STATIC_DATA_POPULATION
            = new List<MsqImageRepositoryData>()
            {
                new MsqImageRepositoryData() { PatientId = 10001, ImageId = 90001, AcquisitionDateTime = DateTime.Now.AddDays(-10), Pixels = _defaultPixels },
                new MsqImageRepositoryData() { PatientId = 10001, ImageId = 90002, AcquisitionDateTime = DateTime.Now.AddDays(-9), Pixels = _defaultPixels, ReferenceImageId = 90001, Reviewed = DateTime.Now.AddDays(-8) },
                new MsqImageRepositoryData() { PatientId = 10001, ImageId = 90003, AcquisitionDateTime = DateTime.Now.AddDays(-8), Pixels = _defaultPixels, ReferenceImageId = 90001, Reviewed = DateTime.Now.AddDays(-7) },
                new MsqImageRepositoryData() { PatientId = 10001, ImageId = 90004, AcquisitionDateTime = DateTime.Now.AddDays(-7), Pixels = _defaultPixels, ReferenceImageId = 90001, Reviewed = DateTime.Now.AddDays(-5) },
                new MsqImageRepositoryData() { PatientId = 10001, ImageId = 90005, AcquisitionDateTime = DateTime.Now.AddDays(-6), Pixels = _defaultPixels, ReferenceImageId = 90001  },
                new MsqImageRepositoryData() { PatientId = 10001, ImageId = 90006, AcquisitionDateTime = DateTime.Now.AddDays(-5), Pixels = _defaultPixels, ReferenceImageId = 90001  },
                new MsqImageRepositoryData() { PatientId = 10001, ImageId = 90007, AcquisitionDateTime = DateTime.Now.AddDays(-4), Pixels = _defaultPixels, ReferenceImageId = 90001  },
                new MsqImageRepositoryData() { PatientId = 10001, ImageId = 90008, AcquisitionDateTime = DateTime.Now.AddDays(-3), Pixels = _defaultPixels, ReferenceImageId = 90001  },

                new MsqImageRepositoryData() { PatientId = 10002, ImageId = 90009, AcquisitionDateTime = DateTime.Now.AddDays(-6), Pixels = _defaultPixels },
                new MsqImageRepositoryData() { PatientId = 10002, ImageId = 90010, AcquisitionDateTime = DateTime.Now.AddDays(-5), Pixels = _defaultPixels, ReferenceImageId = 90009, Reviewed = DateTime.Now.AddDays(-4) },
                new MsqImageRepositoryData() { PatientId = 10002, ImageId = 90011, AcquisitionDateTime = DateTime.Now.AddDays(-4), Pixels = _defaultPixels, ReferenceImageId = 90009, Reviewed = DateTime.Now.AddDays(-3) },
                new MsqImageRepositoryData() { PatientId = 10002, ImageId = 90012, AcquisitionDateTime = DateTime.Now.AddDays(-3), Pixels = _defaultPixels, ReferenceImageId = 90009  },
                new MsqImageRepositoryData() { PatientId = 10002, ImageId = 90013, AcquisitionDateTime = DateTime.Now.AddDays(-2), Pixels = _defaultPixels, ReferenceImageId = 90009  },

                new MsqImageRepositoryData() { PatientId = 10003, ImageId = 90014, AcquisitionDateTime = DateTime.Now.AddDays(-8), Pixels = _defaultPixels },
                new MsqImageRepositoryData() { PatientId = 10003, ImageId = 90015, AcquisitionDateTime = DateTime.Now.AddDays(-7), Pixels = _defaultPixels, ReferenceImageId = 90014  },
                new MsqImageRepositoryData() { PatientId = 10003, ImageId = 90016, AcquisitionDateTime = DateTime.Now.AddDays(-6), Pixels = _defaultPixels, ReferenceImageId = 90014  },
                new MsqImageRepositoryData() { PatientId = 10003, ImageId = 90017, AcquisitionDateTime = DateTime.Now.AddDays(-5), Pixels = _defaultPixels, ReferenceImageId = 90014  },
                new MsqImageRepositoryData() { PatientId = 10003, ImageId = 90018, AcquisitionDateTime = DateTime.Now.AddDays(-4), Pixels = _defaultPixels, ReferenceImageId = 90014  },
                new MsqImageRepositoryData() { PatientId = 10003, ImageId = 90019, AcquisitionDateTime = DateTime.Now.AddDays(-3), Pixels = _defaultPixels, ReferenceImageId = 90014  },
                new MsqImageRepositoryData() { PatientId = 10003, ImageId = 90020, AcquisitionDateTime = DateTime.Now.AddDays(-2), Pixels = _defaultPixels, ReferenceImageId = 90014  },
                new MsqImageRepositoryData() { PatientId = 10003, ImageId = 90021, AcquisitionDateTime = DateTime.Now.AddDays(-1), Pixels = _defaultPixels, ReferenceImageId = 90014  },
            }
#endif
            ;
    }

    class MsqPatientData
    {
        public int PatientId { get; set; }

        public string PatientName { get; set; }

        public string MedRc { get; set; }

        public DateTime? OnTreatment { get; set; }

        public DateTime? OffTreatment { get; set; }
    }

    class MsqImageRepositoryData
    {
        public int PatientId { get; set; }

        public int ImageId { get; set; }

        public DateTime AcquisitionDateTime { get; set; }

        public int? ReferenceImageId { get; set; }

        public byte[,] Pixels { get; set; }

        public DateTime? Reviewed { get; set; }
    }
}
