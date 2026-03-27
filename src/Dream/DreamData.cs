using System;
using System.IO;

namespace Dream;

public static class DreamData
{
    public static string RoamingPath { get; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    public static string DreamPath { get; } = GetExistsPath(Path.Combine(RoamingPath, "Dream"));
    public static string UserDataFolderPath { get; } = GetExistsPath(Path.Combine(DreamPath, "UserData"));
    public static string UserDataFilePath { get; } = Path.Combine(UserDataFolderPath, "UserData.json");

    public static string GetExistsPath(string? path)
    {
        if (string.IsNullOrEmpty(path))
            return string.Empty;
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        return path;
    }
}
