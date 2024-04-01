using Assets.GardenOfDreams.Scripts.Interfaces;
using System;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class JsonToFileStorageService : IStorageService
{
    public void Save(string key, object data, Action<bool> callback = null)
    {
        string path = BuildPath(key);        
        string json = JsonConvert.SerializeObject(data);
        
        using (var fileStream = new StreamWriter(path))
        {
            fileStream.WriteAsync(json);
        }

        callback?.Invoke(true);
    }

    public void Load<T>(string key, Action<T> callback)
    {
        string path = BuildPath(key);

        if (File.Exists(path))
            using (StreamReader fileStream = new StreamReader(path))
            {
                string json = fileStream.ReadToEnd();
                var data = JsonConvert.DeserializeObject<T>(json);
                callback.Invoke(data);
            }
    }

    private string BuildPath(string key)
    => Path.Combine(Application.persistentDataPath, key);
}