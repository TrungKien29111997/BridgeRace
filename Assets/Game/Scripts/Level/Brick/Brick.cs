using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : GameUnit<EBrickType>
{
    [field: SerializeField] public Renderer BrickRen { get; private set; }

    public void OnDespawn()
    {
        SimplePool<EBrickType>.Despawn(this);
    }

    public override void SetMaterial(Material mat)
    {
        BrickRen.material = mat;
    }
}
