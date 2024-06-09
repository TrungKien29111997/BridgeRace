using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    Bridge bridge;
    [SerializeField] ColorData colorData;
    [SerializeField] Renderer stairRen;
    [field: SerializeField] public EBrickType currentBrickType { get; private set; }

    [field: SerializeField] public Character CurrentChar { get; private set; }

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

    void Start()
    {
        stairRen.material = colorData.GetMaterial(currentBrickType);
    }

    public void SetBridge(Bridge tmpBridge)
    {
        bridge = tmpBridge;
    }

    private void OnTriggerEnter(Collider other)
    {
        //TODO: fix => OK
        CurrentChar = Cache.GetCharacter(other);

        if (CurrentChar.BrickType != currentBrickType)
        {
            if (CurrentChar.BrickLeft > 0)
            {
                CurrentChar.RemoveBrick();
                stairRen.material = colorData.GetMaterial(CurrentChar.BrickType);
                currentBrickType = CurrentChar.BrickType;
            }
        }

        if (CurrentChar.CompareTag(Constant.TAG_PLAYER))
        {
            if (CurrentChar.BrickType != currentBrickType)
            {
                bridge.BoolStopPlayer(true);
            }
            else
            {
                bridge.BoolStopPlayer(false);
            }
        }
    }
}
