using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GameUnit<EWeaponType>
{
    public void OnDespawn()
    {
        SimplePool<EWeaponType>.Despawn(this);
    }
}
