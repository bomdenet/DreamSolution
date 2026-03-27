using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dream;

public sealed class DreamUserData
{
    [JsonInclude]
    public Guid ID { get; private set; }

    public static DreamUserData Instance { get; private set; } = null!;
    static DreamUserData() => Load();
    private DreamUserData() { }

    public static void Save() => File.WriteAllText(DreamData.UserDataFilePath, JsonSerializer.Serialize(Instance));
    public static void Load()
    {
        if (File.Exists(DreamData.UserDataFilePath))
        {
            string json = File.ReadAllText(DreamData.UserDataFilePath);
            Instance = JsonSerializer.Deserialize<DreamUserData>(json) ?? new();
            if (Instance.ID == Guid.Empty)
            {
                Instance.ID = Guid.NewGuid();
                Save();
            }
        }
        else
        {
            Instance = new()
            {
                ID = Guid.NewGuid()
            };
            Save();
        }
    }
}
