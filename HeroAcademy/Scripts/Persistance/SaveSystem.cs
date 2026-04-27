using System.IO;
using UnityEngine;

public class SaveSystem
{
    private static string PATH = Application.persistentDataPath + "/save.json";
    
    public static SaveData Load()
    {
        if (!File.Exists(PATH))
        {
            return new SaveData();
        }

        var json = File.ReadAllText(PATH);
        return JsonUtility.FromJson<SaveData>(json);
    }

    public static void Save(SaveData data)
    {
        var json = JsonUtility.ToJson(data, true);
        File.WriteAllText(PATH, json);
    }
}
