using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickControl : MonoBehaviour
{
    [SerializeField] PoolAmout<EBrickType>[] poolAmouts;
    [SerializeField] ColorData colorData;

    void Awake()
    {
        OnInit();
    }

    public void OnInit()
    {
        for (int i = 1; i < 7; i++)
        {
            GameUnit<EBrickType> tmpBrick = poolAmouts[0].prefab;
            tmpBrick.poolType = (EBrickType)i;
            tmpBrick.SetMaterial(colorData.GetMaterial((EBrickType)i));
            SimplePool<EBrickType>.PreLoad(tmpBrick, poolAmouts[0].amount, poolAmouts[0].parent);
        }
    }
}


