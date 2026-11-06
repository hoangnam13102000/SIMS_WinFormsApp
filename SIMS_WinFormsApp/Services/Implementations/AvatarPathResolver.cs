using System;
using System.IO;

namespace SIMS_WinFormsApp.Services.Implementations
{
    public static class AvatarPathResolver
    {
        public static string GetAvatarDirectory()
        {
            return Path.Combine(FindProjectDirectory(), "Resources", "Avatars");
        }

        public static string ToStoredPath(string absolutePath)
        {
            return Path.Combine("Resources", "Avatars", Path.GetFileName(absolutePath));
        }

        public static string Resolve(string storedPath)
        {
            if (string.IsNullOrWhiteSpace(storedPath))
                return null;

            if (Path.IsPathRooted(storedPath))
            {
                if (File.Exists(storedPath))
                    return storedPath;

                return Path.Combine(GetAvatarDirectory(), Path.GetFileName(storedPath));
            }

            string projectPath = Path.Combine(FindProjectDirectory(), storedPath);
            if (File.Exists(projectPath))
                return projectPath;

            string applicationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, storedPath);
            if (File.Exists(applicationPath))
                return applicationPath;

            return projectPath;
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