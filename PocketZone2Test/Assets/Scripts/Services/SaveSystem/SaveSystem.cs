using System.IO;
using UnityEngine;

namespace Services.SaveSystem
{
    public static class SaveSystem
    {
        private static string savePath = Application.persistentDataPath + "/save.json";

        public static void SaveData(GameData data)
        {
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(savePath, json);
        }

        public static GameData LoadData()
        {
            if (File.Exists(savePath))
            {
                string json = File.ReadAllText(savePath);
                return JsonUtility.FromJson<GameData>(json);
            }
            return null;
        }
    }
}
