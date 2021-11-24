using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FFImageLoading.Helpers
{
    internal static class ExifHelper
    {
        //const int MOTOROLA_TIFF_MAGIC_NUMBER = 0x4D4D;
        //const int INTEL_TIFF_MAGIC_NUMBER = 0x4949;

        static readonly Exif.ExifReader _exifReader = new Exif.ExifReader();

        public static IList<Exif.Directory> Read(Stream stream)
        {
            if (stream.Position != 0)
                stream.Position = 0;


            var segmentTypes = _exifReader.SegmentTypes;
            var segments = Exif.JpegSegmentReader.ReadSegments(new Exif.SequentialStreamReader(stream), segmentTypes);

            var directories = new List<Exif.Directory>();

            var readerSegmentTypes = _exifReader.SegmentTypes;
            var readerSegments = segments.Where(s => readerSegmentTypes.Contains(s.Type));
            directories.AddRange(_exifReader.ReadJpegSegments(readerSegments));

            return directories;
        }
    }
}
