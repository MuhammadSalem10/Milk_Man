namespace MilkMan.API.Utils
{
    public static class ValidateFileUpload
    {
        public static void ValidateImage(IFormFile file)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png", ".svg" };

            if (!allowedExtensions.Contains(Path.GetExtension(file.FileName)))
            {
                throw new ArgumentException($"Unsupported image format. Only the following extensions are allowed: {string.Join(", ", allowedExtensions)}");
            }

            if (file.Length > 10485760)
            {
                throw new ArgumentException($"Image cannot exceed {10485760} mega bytes. Please upload a smaller image.");
            }
        }
    }
}
