using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] State state;
    public State State { set => state = value; }
    [SerializeField] GameObject brickPrefab;
    [SerializeField] ColorData colorData;
    GameObject brickObj;
    public GameObject BrickObj => brickObj;

    [SerializeField] ColorType brickType;
    public ColorType BrickType => brickType;

    void Start()
    {
        SpawnBrick(RandomBrickType(), transform);
    }

    void ReduceBrick()
    {
        state.BrickNum(brickType, -1);
        Destroy(brickObj);
    }
    void ReSpawn()
    {
        SpawnBrick(state.BrickMissing, transform);
    }
    ColorType RandomBrickType()
    {
        ColorType type = ColorType.None;
        switch (Random.Range(0, 6))
        {
            case 0:
                type = ColorType.Red;
                break;
            case 1:
                type = ColorType.Green;
                break;
            case 2:
                type = ColorType.Blue;
                break;
            case 3:
                type = ColorType.Yellow;
                break;
            case 4:
                type = ColorType.Black;
                break;
            case 5:
                type = ColorType.Pink;
                break;
            case 6:
                type = ColorType.Red;
                break;
        }
        return type;
    }

    void SpawnBrick(ColorType colorType, Transform parent)
    {
        brickObj = Instantiate(brickPrefab, parent);
        switch (colorType)
        {
            case ColorType.Red:
                brickType = ColorType.Red;
                break;
            case ColorType.Green:
                brickType = ColorType.Green;
                break;
            case ColorType.Blue:
                brickType = ColorType.Blue;
                break;
            case ColorType.Yellow:
                brickType = ColorType.Yellow;
                break;
            case ColorType.Black:
                brickType = ColorType.Black;
                break;
            case ColorType.Pink:
                brickType = ColorType.Pink;
                break;
        }
        brickObj.GetComponent<Renderer>().material = colorData.GetMaterial(brickType);
        state.BrickNum(brickType, +1);
    }

    private void OnTriggerStay(Collider other)
    {
        if (brickObj != null)
        {
            if (other.CompareTag("Player") || other.CompareTag("Bot"))
            {
                Character tempChar = other.GetComponent<Character>();

                if (tempChar.BrickType == brickType)
                {
                    Invoke(nameof(ReSpawn), 3f);
                    tempChar.AddBrick();
                    ReduceBrick();
                }
            }
        }
    }
}
