using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SIMS_WinFormsApp.Services.Backup
{
    /// <summary>
    /// Tải file backup lên Cloudinary bằng Unsigned Upload Preset, resource_type=raw (giữ
    /// nguyên file nhị phân .bak, không cố xử lý như ảnh/video) - cổng HTTP tương đương
    /// CloudinaryService.uploadBackupFile bên bản Java gốc (cũng POST multipart tới
    /// https://api.cloudinary.com/v1_1/{cloud_name}/raw/upload với field "file" + "upload_preset").
    /// </summary>
    public sealed class CloudinaryUploader : ICloudinaryUploader
    {
        private static readonly HttpClient HttpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(180) };

        public async Task<string> UploadBackupFileAsync(string filePath, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (!File.Exists(filePath))
                throw new CloudinaryUploadException("Không tìm thấy file để tải lên: " + filePath);

            string cloudName = CloudinaryConfig.CloudName;
            string uploadPreset = CloudinaryConfig.BackupUploadPreset;

            if (string.IsNullOrWhiteSpace(cloudName))
                throw new CloudinaryUploadException("Thiếu cấu hình CloudinaryCloudName trong App.config.");
            if (string.IsNullOrWhiteSpace(uploadPreset))
                throw new CloudinaryUploadException("Thiếu cấu hình CloudinaryBackupUploadPreset trong App.config.");

            string uploadUrl = "https://api.cloudinary.com/v1_1/" + cloudName + "/raw/upload";

            using (var content = new MultipartFormDataContent())
            using (var fileStream = File.OpenRead(filePath))
            {
                var fileContent = new StreamContent(fileStream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                content.Add(fileContent, "file", Path.GetFileName(filePath));
                content.Add(new StringContent(uploadPreset), "upload_preset");

                HttpResponseMessage response;
                try
                {
                    response = await HttpClient.PostAsync(uploadUrl, content, cancellationToken).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    throw new CloudinaryUploadException("Không kết nối được Cloudinary. Hãy kiểm tra Internet.", ex);
                }

                string body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                    throw new CloudinaryUploadException("Cloudinary từ chối tải: " + ExtractErrorMessage(body, response.StatusCode.ToString()));

                return ExtractSecureUrl(body);
            }
        }

        private static string ExtractSecureUrl(string json)
        {
            try
            {
                var parsed = JObject.Parse(json);
                string url = parsed["secure_url"]?.ToString();
                if (string.IsNullOrEmpty(url))
                    throw new CloudinaryUploadException("Cloudinary không trả về secure_url.");
                return url;
            }
            catch (CloudinaryUploadException) { throw; }
            catch (Exception ex)
            {
                throw new CloudinaryUploadException("Không đọc được phản hồi từ Cloudinary.", ex);
            }
        }

        private static string ExtractErrorMessage(string json, string fallback)
        {
            try
            {
                var parsed = JObject.Parse(json);
                return parsed["error"]?["message"]?.ToString() ?? fallback;
            }
            catch { return fallback; }
        }
    }
}