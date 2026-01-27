using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    private Dictionary<Type, object> pools = new();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void CreatePool<T>(T prefab, int size) where T : Component, IPoolable
    {
        var type = typeof(T);

        if (pools.ContainsKey(type))
            return;

        var pool = new ObjectPool<T>(prefab, size, transform);
        pools.Add(type, pool);
    }

    public T Get<T>() where T : Component, IPoolable
    {
        var type = typeof(T);
        return ((ObjectPool<T>)pools[type]).Get();
    }

    public void Release<T>(T obj) where T : Component, IPoolable
    {
        var type = typeof(T);
        ((ObjectPool<T>)pools[type]).Release(obj);
    }
}