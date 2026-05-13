using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dream;

public sealed class DreamUserData
{
    public static Guid ID => Instance.ID;


    private static Data Instance { get; set; } = null!;
    static DreamUserData() => Load();
    [JsonConstructor]
    private DreamUserData() { }

    private static void Save() => File.WriteAllText(DreamData.UserDataFilePath, JsonSerializer.Serialize(Instance));
    private static void Load()
    {
        if (File.Exists(DreamData.UserDataFilePath))
        {
            string json = File.ReadAllText(DreamData.UserDataFilePath);
            Instance = JsonSerializer.Deserialize<Data>(json) ?? new();
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


    private sealed class Data
    {
        public Guid ID { get; set; }
    }
}
