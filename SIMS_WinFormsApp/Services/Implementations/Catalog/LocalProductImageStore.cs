using System;
using System.IO;

namespace SIMS_WinFormsApp.Services.Implementations.Catalog
{
    /// <summary>Lưu ảnh sản phẩm trên đĩa và trả đường dẫn để ghi vào Products.ImageUrl.</summary>
    public sealed class LocalProductImageStore
    {
        private readonly string _directory;

        public LocalProductImageStore(string directory = null)
        {
            _directory = string.IsNullOrWhiteSpace(directory)
                ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProductImages")
                : directory;
        }

        public string SaveCopy(string sourcePath)
        {
            if (string.IsNullOrWhiteSpace(sourcePath) || !File.Exists(sourcePath))
                throw new FileNotFoundException("Không tìm thấy file ảnh.", sourcePath);

            Directory.CreateDirectory(_directory);
            string extension = Path.GetExtension(sourcePath);
            if (string.IsNullOrWhiteSpace(extension)) extension = ".jpg";
            string destination = Path.Combine(_directory, Guid.NewGuid().ToString("N") + extension.ToLowerInvariant());
            File.Copy(sourcePath, destination, false);
            return destination;
        }

        public string Resolve(string storedPath)
        {
            if (string.IsNullOrWhiteSpace(storedPath)) return null;
            if (File.Exists(storedPath)) return storedPath;
            string combined = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, storedPath);
            return File.Exists(combined) ? combined : null;
        }
    }
}
