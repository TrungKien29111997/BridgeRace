using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPool : MonoBehaviour
{
    [SerializeField] PoolAmout<EWeaponType>[] poolAmouts;
    public bool clearWeapon;

    void Awake()
    {
        OnInit();
        clearWeapon = false;
    }

    private void Update()
    {
        if (clearWeapon)
        {
            SimplePool<EWeaponType>.ReleaseAll();
            clearWeapon = false;
        }
    }

    public void OnInit()
    {
        for (int i = 0; i < poolAmouts.Length; i++)
        {
            SimplePool<EWeaponType>.PreLoad(poolAmouts[i].prefab, poolAmouts[0].amount, poolAmouts[0].parent);
        }
    }
}
