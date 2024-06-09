using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    [Header("BrickSpawnPosSetting")]
    [SerializeField] BrickPos brickPosPrefab;
    [SerializeField] Vector2 offsetBricks;
    [SerializeField] Vector2Int numberBricks;
    [field: SerializeField] public BrickPos[] SpawnPosArray { get; private set; }
    [field: SerializeField] public Bridge[] Bridges { get; private set; }
    [field: SerializeField] public EBrickType BrickMissing { get; private set; }

    [SerializeField] ColorData colorData;

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

    [Header("BrickControlSetting")]
    [SerializeField] int[] bricksNum;
    [SerializeField] int redBricks;
    [SerializeField] int greenBricks;
    [SerializeField] int blueBricks;
    [SerializeField] int yellowBricks;
    [SerializeField] int blackBricks;
    [SerializeField] int pinkBricks;

    void Start()
    {
        SpawnPosArray = new BrickPos[numberBricks.x * numberBricks.y];
        bricksNum = new int[6];
        OnInit();
    }

    void OnInit()
    {
        SpawnPos();
    }
    void SpawnPos()
    {
        int arrayLenght = 0;
        for (int i = 0; i < numberBricks.x; i++)
        {
            for (int j = 0; j < numberBricks.y; j++)
            {
                BrickPos tmpBrickPos = Instantiate(brickPosPrefab, TF);
                tmpBrickPos.TF.localPosition = new Vector3(i * offsetBricks.x, 0, j * offsetBricks.y);
                SpawnPosArray[arrayLenght] = tmpBrickPos;
                SpawnPosArray[arrayLenght].SetState(this);
                arrayLenght++;
            }
        }
    }
    void BrickLeast()
    {
        bricksNum[0] = redBricks;
        bricksNum[1] = greenBricks;
        bricksNum[2] = blueBricks;
        bricksNum[3] = yellowBricks;
        bricksNum[4] = blackBricks;
        bricksNum[5] = pinkBricks;

        Array.Sort(bricksNum);

        if (bricksNum[0] == redBricks)
        {
            BrickMissing = EBrickType.Red;
        }
        else if (bricksNum[0] == greenBricks)
        {
            BrickMissing = EBrickType.Green;
        }
        else if (bricksNum[0] == blueBricks)
        {
            BrickMissing = EBrickType.Blue;
        }
        else if (bricksNum[0] == yellowBricks)
        {
            BrickMissing = EBrickType.Yellow;
        }
        else if (bricksNum[0] == blackBricks)
        {
            BrickMissing = EBrickType.Black;
        }
        else if (bricksNum[0] == pinkBricks)
        {
            BrickMissing = EBrickType.Pink;
        }
    }

    public void BrickNum(EBrickType colorType, int parameter)
    {
        switch(colorType)
        {
            case EBrickType.Red:
                redBricks += parameter;
                break;
            case EBrickType.Green:
                greenBricks += parameter;
                break;
            case EBrickType.Blue:
                blueBricks += parameter;
                break;
            case EBrickType.Yellow:
                yellowBricks += parameter;
                break;
            case EBrickType.Black:
                blackBricks += parameter;
                break;
            case EBrickType.Pink:
                pinkBricks += parameter;
                break;
        }
        BrickLeast();
    }
}
