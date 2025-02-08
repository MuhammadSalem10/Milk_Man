

using MilkMan.Shared.Common;

namespace MilkMan.Domain.Repositories
{
    public interface IFileStorageService
    {
        Task<Result<string>> SaveFileAsync(byte[] fileBytes, string fileName, string folder);
        Task<Result> DeleteFileAsync(string filePath);
        Task<Result<string>> UpdateFileAsync(byte[] newFileBytes, string fileName, string oldFilePath, string folder);
    }
}
