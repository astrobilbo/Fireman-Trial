using System;
using UnityEngine;
using System.IO;

namespace FiremanTrial
{
    public static class PermanentData
    {
        private static readonly string BasePath = Application.persistentDataPath;

        public static void Save<T>(T obj, string relativePath) // → Transforma um objeto em json e salva no repositório.
        {
            string fullPath = Path.Combine(BasePath, relativePath);
            string json = JsonUtility.ToJson(obj);
            File.WriteAllText(fullPath, json);
        }

        public static T Load<T>(T defaultObj,string relativePath) // → Carrega o json do repositório e transforma no objeto inicial.
        {
            string fullPath = Path.Combine(BasePath, relativePath);

            if (File.Exists(fullPath))
            {
                string json = File.ReadAllText(fullPath);
                return JsonUtility.FromJson<T>(json);
            }
            else
            {
                Save(defaultObj, fullPath);
                return defaultObj;
            }
        }
        
        public static void Clean()  // → Deleta a pasta base e a recria limpando todos os arquivos salvos
        {

            if (Directory.Exists(BasePath))
            {
                Directory.Delete(BasePath, true);
                Directory.CreateDirectory(BasePath);
                Debug.Log($"All save data has been deleted from: {BasePath}");
            }
            else
            {
                Debug.LogWarning($"No save data found to delete in: {BasePath}");
            }
        }
        
    }
}