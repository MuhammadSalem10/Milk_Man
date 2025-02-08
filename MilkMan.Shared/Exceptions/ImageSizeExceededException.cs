

namespace MilkMan.Shared.Exceptions
{
    public class ImageSizeExceededException : Exception
    {
        public long MaxFileSize { get; }

        public ImageSizeExceededException(long maxFileSize)
            : base($"Image cannot exceed {maxFileSize} mega bytes. Please upload a smaller image.")
        {
            MaxFileSize = maxFileSize;
        }
    }

}
