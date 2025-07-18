using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private T prefab;
    private List<T> objPool = new List<T>();

    public ObjectPool(T prefab, int initialSize, Transform parent)
    {
        this.prefab = prefab;

        for (int i = 0; i < initialSize; i++)
        {
            T obj = GameObject.Instantiate(prefab);
            obj.transform.SetParent(parent);
            obj.gameObject.SetActive(false);
            objPool.Add(obj);
        }
    }

    public T Get()
    {
        foreach (var obj in objPool)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                obj.gameObject.SetActive(true);
                return obj;
            }
        }
        return null;
    }
}
