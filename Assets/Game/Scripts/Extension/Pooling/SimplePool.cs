using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SimplePool<T>
{
    static Dictionary<T, Pool<T>> poolInstance = new Dictionary<T, Pool<T>>();

    // Khoi tao Pool moi
    public static void PreLoad(GameUnit<T> prefab, int amount, Transform parent)
    {
        if (prefab == null)
        {
            Debug.LogError("PREFABS IS EMPTY");
            return;
        }

        if (!poolInstance.ContainsKey(prefab.poolType) || poolInstance[prefab.poolType] == null)
        {
            Pool<T> p = new Pool<T>();
            p.PreLoad(prefab, amount, parent);
            poolInstance[prefab.poolType] = p;
        }
    }

    // Lay phan tu ra
    public static H Spawn<H>(T poolType, Vector3 pos, Quaternion rot) where H : GameUnit<T>
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.LogError(poolType + "IS NOT PRELOAD");
            return null;
        }
        return poolInstance[poolType].Spawn(pos, rot) as H;
    }

    // tra phan tu vao
    public static void Despawn(GameUnit<T> unit)
    {
        if (!poolInstance.ContainsKey(unit.poolType))
        {
            Debug.LogError(unit.poolType + "IS NOT PRELOAD");
        }
        poolInstance[unit.poolType].Despawn(unit);
    }

    //thu thap phan tu
    public static void Collect(T poolType)
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.LogError(poolType + "IS NOT PRELOAD");
        }
        poolInstance[poolType].Collect();
    }

    //thu thap tat ca
    public static void CollectAll()
    {
        foreach(var item in poolInstance.Values)
        {
            item.Collect();
        }
    }

    // Destroy 1 pool
    public static void Release(T poolType)
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.LogError(poolType + "IS NOT PRELOAD");
        }
        poolInstance[poolType].Release();
    }

    // Destroy tat ca
    public static void ReleaseAll()
    {
        foreach (var item in poolInstance.Values)
        {
            item.Release(); 
        }
    }
}


public class Pool<T> : MonoBehaviour
{
    Transform parent;
    GameUnit<T> prefab;
    // List chua cac Unit o trong Pool
    Queue<GameUnit<T>> inActives = new Queue<GameUnit<T>>();

    //List chua cac Unit dang su dung
    List<GameUnit<T>> actives = new List<GameUnit<T>>();

    // khoi tao Poll
    public void PreLoad(GameUnit<T> prefab, int amount, Transform parent)
    {
        this.parent = parent;
        this.prefab = prefab;
        
        for (int i = 0; i < amount; i++)
        {
            Despawn(Instantiate(prefab, parent));
        }
    }

    // Active phan tu tu Pool thay vi Instantiate
    public GameUnit<T> Spawn(Vector3 pos, Quaternion rot)
    {
        GameUnit<T> unit;
        if (inActives.Count <= 0)
        {
            unit = Instantiate(prefab, parent);
        }
        else
        {
            unit = inActives.Dequeue();
        }
        unit.TF.SetPositionAndRotation(pos, rot);
        actives.Add(unit);
        unit.gameObject.SetActive(true);
        return unit;
    }

    // Nap phan tu vao Pool thay vi Destroy
    public void Despawn(GameUnit<T> unit)
    {
        if (unit != null && unit.gameObject.activeSelf)
        {
            actives.Remove(unit);
            inActives.Enqueue(unit);
            unit.gameObject.SetActive(false);
        }
    }

    // thu thap phan tu Instantiate Clone ve Pool
    public void Collect()
    {
        while(actives.Count > 0)
        {
            Despawn(actives[0]);
        }
    }

    // Destroy tat ca phan tu Clone dang ton tai
    public void Release()
    {
        Collect();
        while(inActives.Count > 0)
        {
            Destroy(inActives.Dequeue().gameObject);
        }
        inActives.Clear();
    }
}
