using System;
using UnityEngine;

public class Data : MonoBehaviour
{
    public static Data Instance { get; set; }
    public JsonToFileStorageService DataSystem { get; private set; }
    public Action OnDataSave;

    private void Awake()
    => Init();

    private void Init()
    {
        if (Instance == null)
        {
            Instance = this;
            DataSystem = new JsonToFileStorageService();
            return;
        }

        Destroy(gameObject);
    }

    private void OnApplicationQuit()
    => OnDataSave?.Invoke();
}