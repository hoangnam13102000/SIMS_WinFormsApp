using System;
using System.IO;

namespace SIMS_WinFormsApp.Services.Implementations.Catalog
{
    /// <summary>Lưu ảnh sản phẩm trên đĩa và trả đường dẫn để ghi vào Products.ImageUrl.</summary>
    public sealed class LocalProductImageStore
    {
        private const string ResourceFolderName = "Resources";
        private const string StorageFolderName = "ProductImages";
        private const string LegacyAppDataFolderName = "SIMS_WinFormsApp";
        private readonly string _directory;
        private readonly string _resourceRoot;

        public LocalProductImageStore(string directory = null)
        {
            _resourceRoot = FindProjectDirectory();
            _directory = string.IsNullOrWhiteSpace(directory)
                ? Path.Combine(_resourceRoot, ResourceFolderName, StorageFolderName)
                : directory;
        }

        public string SaveCopy(string sourcePath)
        {
            if (string.IsNullOrWhiteSpace(sourcePath) || !File.Exists(sourcePath))
                throw new FileNotFoundException("Không tìm thấy file ảnh.", sourcePath);

            Directory.CreateDirectory(_directory);
            if (IsInsideStore(sourcePath))
                return ToStoredPath(sourcePath);

            string extension = Path.GetExtension(sourcePath);
            if (string.IsNullOrWhiteSpace(extension)) extension = ".jpg";
            string destination = Path.Combine(_directory, Guid.NewGuid().ToString("N") + extension.ToLowerInvariant());
            File.Copy(sourcePath, destination, false);
            return ToStoredPath(destination);
        }

        public string Resolve(string storedPath)
        {
            if (string.IsNullOrWhiteSpace(storedPath)) return null;

            string normalizedPath = storedPath.Replace('/', Path.DirectorySeparatorChar)
                .Replace('\\', Path.DirectorySeparatorChar);
            string fileName = Path.GetFileName(normalizedPath);
            if (string.IsNullOrWhiteSpace(fileName)) return null;

            string persistentPath = Path.Combine(_directory, fileName);
            if (File.Exists(persistentPath)) return persistentPath;

            if (Path.IsPathRooted(normalizedPath) && File.Exists(normalizedPath))
                return MigrateLegacyImage(normalizedPath, persistentPath);

            string legacyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, normalizedPath);
            if (File.Exists(legacyPath))
                return MigrateLegacyImage(legacyPath, persistentPath);

            string oldAppDataDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                LegacyAppDataFolderName,
                StorageFolderName);
            string oldAppDataImage = Path.Combine(oldAppDataDirectory, fileName);
            if (File.Exists(oldAppDataImage))
                return MigrateLegacyImage(oldAppDataImage, persistentPath);

            string projectRelativePath = Path.Combine(_resourceRoot, normalizedPath);
            if (File.Exists(projectRelativePath))
                return projectRelativePath;

            return null;
        }

        public string GetStoredPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !Path.IsPathRooted(path) || !IsInsideStore(path))
                return null;

            return File.Exists(path) ? ToStoredPath(path) : null;
        }

        private bool IsInsideStore(string path)
        {
            string fullPath = Path.GetFullPath(path);
            string storePath = Path.GetFullPath(_directory)
                .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                + Path.DirectorySeparatorChar;
            return fullPath.StartsWith(storePath, StringComparison.OrdinalIgnoreCase);
        }

        private static string ToStoredPath(string path)
        {
            return Path.Combine(ResourceFolderName, StorageFolderName, Path.GetFileName(path));
        }

        private string MigrateLegacyImage(string legacyPath, string persistentPath)
        {
            Directory.CreateDirectory(_directory);
            File.Copy(legacyPath, persistentPath, false);
            return persistentPath;
        }

        private static string FindProjectDirectory()
        {
            var directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            while (directory != null)
            {
                if (File.Exists(Path.Combine(directory.FullName, "SIMS_WinFormsApp.csproj")))
                    return directory.FullName;

                directory = directory.Parent;
            }

            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
