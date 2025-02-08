
namespace MilkMan.Shared.Exceptions
{
    public class UnsupportedImageFormatException : Exception
    {
        public IEnumerable<string> SupportedExtensions { get; }

        public UnsupportedImageFormatException(IEnumerable<string> supportedExtensions)
            : base($"Unsupported image format. Only the following extensions are allowed: {string.Join(", ", supportedExtensions)}")
        {
            SupportedExtensions = supportedExtensions;
        }
    }
}
