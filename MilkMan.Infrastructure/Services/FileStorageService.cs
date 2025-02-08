using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MilkMan.Shared.Common;
using MilkMan.Shared.Exceptions;
using MilkMan.Domain.Repositories;

namespace MilkMan.Infrastructure.Services;


public class FileStorageService(IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor, ILogger<FileStorageService> logger) : IFileStorageService
{
    private readonly IWebHostEnvironment _environment = environment;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly ILogger<FileStorageService> _logger = logger;

    public async Task<Result<string>> SaveFileAsync(byte[] fileBytes, string fileName, string folder)
    {
      
            ValidateFileContent(fileBytes);
            //var rootPath = "F:\\milk-man-images";

            var uploadsFolderPath = Path.Combine(_environment.WebRootPath, folder);
            Directory.CreateDirectory(uploadsFolderPath);

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
            var filePath = Path.Combine(uploadsFolderPath, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await fileStream.WriteAsync(fileBytes);
                await fileStream.FlushAsync();
            }

            var urlFilePath = $"{_httpContextAccessor?.HttpContext?.Request.Scheme}://{_httpContextAccessor?.HttpContext?.Request.Host}{_httpContextAccessor?.HttpContext?.Request.PathBase}/{folder}/{uniqueFileName}";

            if(urlFilePath is not null)
            return Result<string>.Success(urlFilePath);
     
            return Result<string>.Failure("File failed to be saved!");
        

    }

    public Task<Result> DeleteFileAsync(string filePath)
    {
        try
        {
            // Ensure filePath is relative
            var relativePath = filePath.Replace($"{_httpContextAccessor?.HttpContext?.Request.Scheme}://{_httpContextAccessor?.HttpContext?.Request.Host}{_httpContextAccessor?.HttpContext?.Request.PathBase}/", "");
            //var rootPath = "F:\\milk-man-images";

            var fullPath = Path.Combine(_environment.WebRootPath, relativePath);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            return Task.FromResult(Result.Success());
        }
        catch (Exception ex)
        {
            _logger.LogError($"ErrorMessage deleting file: {ex.Message}");
            return Task.FromResult(Result.Failure("Failed to delete the file!"));
        }

    }


    public async Task<Result<string>> UpdateFileAsync(byte[] newFileBytes, string fileName, string oldFilePath, string folder)
    {
        try
        {
            ValidateFileContent(newFileBytes);
            // DeleteAsync the old file
            var deleteResult = await DeleteFileAsync(oldFilePath);
            if (deleteResult.IsFailure)
            {
                return Result<string>.Failure(deleteResult.ErrorMessage);
            }


            // Save the new file
            var saveResult = await SaveFileAsync(newFileBytes, fileName, folder);
            if (saveResult.IsFailure)
            {
                return Result<string>.Failure(saveResult.ErrorMessage);
            }

            return saveResult;
        }
        catch (Exception ex)
        {
            _logger.LogError($"ErrorMessage updating file: {ex.Message}");
            // Consider retrying deletion of the potentially partially uploaded new file here
            return Result<string>.Failure("Failed to delete");
        }
    }
    private void ValidateFileContent(byte[] fileBytes)
    {
        // You can add validation for file size and potentially file type based on magic numbers here
        if (fileBytes.Length > 10485760) // 10MB limit
        {
            throw new ImageSizeExceededException(10);
        }
    }
}


