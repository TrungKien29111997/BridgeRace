using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPos : MonoBehaviour
{
    [SerializeField] ColorData colorData;
    [field: SerializeField] public Brick Brick { get; private set; }
    [field: SerializeField] public EBrickType BrickType { get; private set; }

    State currentState;

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

    Character tempChar;

    void Start()
    {
        SpawnBrick((EBrickType)Random.Range(1,7), TF);
    }

    public void SetState(State tmpState)
    {
        currentState = tmpState;
    }
    void ReduceBrick()
    {
        currentState.BrickNum(this.BrickType, -1);
        if (Brick != null)
        {
            Brick.OnDespawn();
            Brick = null;
        }
    }
    void ReSpawn()
    {
        SpawnBrick(currentState.BrickMissing, transform);
    }

    void SpawnBrick(EBrickType colorType, Transform parent)
    {
        Brick = SimplePool<EBrickType>.Spawn<Brick>(colorType, parent.position, parent.rotation);
        Brick.poolType = colorType;
        switch (colorType)
        {
            case EBrickType.Red:
                BrickType = EBrickType.Red;
                break;
            case EBrickType.Green:
                BrickType = EBrickType.Green;
                break;
            case EBrickType.Blue:
                BrickType = EBrickType.Blue;
                break;
            case EBrickType.Yellow:
                BrickType = EBrickType.Yellow;
                break;
            case EBrickType.Black:
                BrickType = EBrickType.Black;
                break;
            case EBrickType.Pink:
                BrickType = EBrickType.Pink;
                break;
        }
        Brick.SetMaterial(colorData.GetMaterial(BrickType));
        currentState.BrickNum(BrickType, +1);
    }

    private void OnTriggerStay(Collider other)
    {
        tempChar = Cache.GetCharacter(other);

        if (Brick != null && tempChar.BrickType == this.BrickType)
        {
            Invoke(nameof(ReSpawn), 3f);
            tempChar.AddBrick();
            ReduceBrick();
        }
    }
}
