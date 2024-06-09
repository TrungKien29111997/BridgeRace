using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public Transform wallPlayer;
    [field: SerializeField] public Door CurrentDoor { get; private set; }

    [field: SerializeField] public List<Stair> Stairs { get; private set; }

    Character player;
    bool stopPlayer;

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

    private void Awake()
    {
        for (int i = 0; i < Stairs.Count; i++)
        {
            Stairs[i].SetBridge(this);
        }
    }

    void Start()
    {
        player = null;
        stopPlayer = false;
    }

    void Update()
    {
        //TODO: Fix => OK
        if (player != null)
        {
            if (Vector3.Dot(player.TF.forward, TF.forward) > 0.5f)
            {
                if (stopPlayer)
                {
                    wallPlayer.position = player.TF.position + TF.forward * 0.3f;
                    stopPlayer = false;
                }
            }
            else
            {
                wallPlayer.localPosition = new Vector3(0, 5, 0);
            }
        }
    }

    public void BoolStopPlayer(bool tmpBool)
    {
        stopPlayer = tmpBool;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_PLAYER))
        {
            player = Cache.GetCharacter(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.TAG_PLAYER))
        {
            player = null;
            wallPlayer.localPosition = new Vector3(0, 5, 0);
        }
    }
}
