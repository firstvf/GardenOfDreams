using System.Collections.Generic;
using UnityEngine;

public class MonoBehPooler<T> where T : MonoBehaviour
{
    private T _prefab;
    private List<T> _poolList;
    private Transform _container;

    public MonoBehPooler(T prefab, int defaultCount, Transform container)
    {
        _prefab = prefab;
        _container = container;

        CreatePool(defaultCount);
    }

    public T GetFromPool()
    {
        if (HasFreeObject(out T prefab))
            return prefab;
        else return CreateObject(true);

        throw new System.Exception("No objects in pool");
    }

    public void AddExtraT(T prefab, int count, Transform container)
    {
        for (int i = 0; i < count; i++)
            CreateObject(false, prefab);
    }

    private bool HasFreeObject(out T prefab)
    {
        foreach (var obj in _poolList)
            if (!obj.gameObject.activeInHierarchy)
            {
                prefab = obj;
                prefab.gameObject.SetActive(true);
                return true;
            }

        prefab = null;
        return false;
    }

    private void CreatePool(int defaultCount)
    {
        _poolList = new List<T>();

        for (int i = 0; i < defaultCount; i++)
            CreateObject();
    }

    private T CreateObject(bool isActiveByDefault = false, T extraPrefab = null)
    {
        T prefab;

        if (extraPrefab == null)
            prefab = Object.Instantiate(_prefab, _container);
        else prefab = Object.Instantiate(extraPrefab, _container);

        prefab.gameObject.SetActive(isActiveByDefault);
        _poolList.Add(prefab);

        return prefab;
    }
}