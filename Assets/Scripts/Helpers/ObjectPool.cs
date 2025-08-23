using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private T prefab;
    private List<T> objPool = new List<T>();
    private Transform parent;

    public ObjectPool(T prefab, int initialSize, Transform parent)
    {
        this.prefab = prefab;
        this.parent = parent;

        for (int i = 0; i < initialSize; i++)
        {
            T obj = Object.Instantiate(prefab);
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

        T newObj = Object.Instantiate(prefab);

        if (!newObj.gameObject.activeInHierarchy)
        {
            newObj.gameObject.SetActive(true);
        }

        newObj.transform.SetParent(parent);
        objPool.Add(newObj);

        return newObj;
    }
}
