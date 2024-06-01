using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] Transform wallPlayer;
    [SerializeField] GameObject stairsGroup;
    [SerializeField] List<Stairs> stairsList;
    public List<Stairs> StairsList => stairsList;
    Player player;

    void Start()
    {
        player = null;
        foreach (Transform t in stairsGroup.transform)
        {
            stairsList.Add(t.GetComponent<Stairs>());
        }
    }

    void Update()
    {
        if (player != null)
        {
            if (Vector3.Dot(player.transform.forward, transform.forward) > 0.5f)
            {
                for (int i = stairsList.Count - 1; i >= 0; i--)
                {
                    if (stairsList[i].BrickType == player.BrickType)
                    {
                        if (player.BrickLeft == 0)
                        {
                            wallPlayer.position = stairsList[i].transform.position;
                            break;
                        }
                        else
                        {
                            wallPlayer.position = new Vector3(0, 5, 0);
                            break;
                        }
                    }
                }
            }
        }

        if (stairsList[stairsList.Count - 1].ChangeColor)
        {
            for (int i = 0; i < LevelManager.instance.Bots.Count; i++)
            {
                if (stairsList[stairsList.Count - 1].BrickType == LevelManager.instance.Bots[i].BrickType)
                {
                    LevelManager.instance.Bots[i].NextState();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<Player>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
        }
    }
}
