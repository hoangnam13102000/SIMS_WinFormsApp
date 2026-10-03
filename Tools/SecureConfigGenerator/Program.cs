using System;
using System.IO;
using System.Text;
using SIMS_WinFormsApp.Infrastructure.Security;

namespace SIMS_WinFormsApp.Tools.SecureConfigGenerator
{
    internal static class Program
    {
        private static int Main(string[] args)
        {
            Console.WriteLine("Tao secure-config.enc cho SIMS");
            Console.WriteLine("Tool ma hoa secure-config.txt bang DPAPI cua tai khoan Windows hien tai.");
            Console.WriteLine();

            string repositoryRoot = FindRepositoryRoot();
            if (repositoryRoot == null)
            {
                Console.Error.WriteLine("Khong tim thay thu muc project SIMS_WinFormsApp.");
                return 1;
            }

            string inputPath = args.Length > 0
                ? Path.GetFullPath(args[0])
                : Path.Combine(repositoryRoot, "secure-config.txt");
            string outputDirectory = args.Length > 1
                ? Path.GetFullPath(args[1])
                : Path.Combine(repositoryRoot, "SIMS_WinFormsApp", "bin", "Debug");

            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine("Khong tim thay file cau hinh dau vao: " + inputPath);
                Console.Error.WriteLine("Tao file secure-config.txt voi GEMINI_API_KEY va GEMINI_MODEL truoc.");
                return 1;
            }

            string plainText = File.ReadAllText(inputPath, Encoding.UTF8);
            if (!ContainsConfigValue(plainText, "GEMINI_API_KEY")
                || !ContainsConfigValue(plainText, "GEMINI_MODEL"))
            {
                Console.Error.WriteLine("File can co GEMINI_API_KEY va GEMINI_MODEL co gia tri.");
                return 1;
            }

            string outputPath = Path.Combine(outputDirectory, "secure-config.enc");
            if (File.Exists(outputPath))
            {
                Console.Write("File da ton tai. Ghi de? (y/N): ");
                if (!string.Equals(Console.ReadLine(), "y", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Da huy, file hien tai duoc giu nguyen.");
                    return 0;
                }
            }

            Directory.CreateDirectory(outputDirectory);
            byte[] encrypted = SecureConfigFile.Encrypt(plainText);
            string temporaryPath = outputPath + ".tmp";
            File.WriteAllBytes(temporaryPath, encrypted);
            if (File.Exists(outputPath))
                File.Replace(temporaryPath, outputPath, null);
            else
                File.Move(temporaryPath, outputPath);

            Console.WriteLine("Da ma hoa file cau hinh thanh cong: " + outputPath);
            Console.WriteLine("Ung dung SIMS se tu doc file nay khi khoi dong.");
            Console.WriteLine("Khong commit secure-config.txt hoac secure-config.enc len Git.");
            return 0;
        }

        private static bool ContainsConfigValue(string plainText, string key)
        {
            foreach (string line in plainText.Split(
                new[] { '\r', '\n' },
                StringSplitOptions.RemoveEmptyEntries))
            {
                string trimmed = line.Trim();
                if (trimmed.StartsWith("#", StringComparison.Ordinal)) continue;
                int separator = trimmed.IndexOf('=');
                if (separator <= 0) continue;
                if (string.Equals(trimmed.Substring(0, separator).Trim(), key, StringComparison.Ordinal)
                    && !string.IsNullOrWhiteSpace(trimmed.Substring(separator + 1)))
                    return true;
            }
            return false;
        }

        private static string FindRepositoryRoot()
        {
            string[] startingDirectories =
            {
                Directory.GetCurrentDirectory(),
                AppDomain.CurrentDomain.BaseDirectory
            };

            foreach (string startingDirectory in startingDirectories)
            {
                var directory = new DirectoryInfo(startingDirectory);
                while (directory != null)
                {
                    string solutionFile = Path.Combine(directory.FullName, "SIMS_WinFormsApp.slnx");
                    string projectFile = Path.Combine(
                        directory.FullName,
                        "SIMS_WinFormsApp",
                        "SIMS_WinFormsApp.csproj");
                    if (File.Exists(solutionFile) && File.Exists(projectFile))
                        return directory.FullName;
                    directory = directory.Parent;
                }
            }

            return null;
        }
    }
}
