using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUnit<T> : MonoBehaviour
{
    public T poolType;
    Transform tf;
    public Transform TF
    {
        get
        {
            if (tf == null)
            {
                tf = transform;
            }
            return tf;
        }
    }

    public virtual void SetMaterial(Material mat)
    {

    }
}

[System.Serializable]
public class PoolAmout<T>
{
    public GameUnit<T> prefab;
    public Transform parent;
    public int amount;
}
