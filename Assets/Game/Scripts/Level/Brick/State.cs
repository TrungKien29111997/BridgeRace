using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    [Header("BrickSpawnPosSetting")]
    [SerializeField] GameObject spawnPosPrefab;
    [SerializeField] Vector2 offsetBricks;
    [SerializeField] Vector2Int numberBricks;
    [SerializeField] Brick[] spawnPosArray;
    public Brick[] SpawnPosArray => spawnPosArray;

    [SerializeField] Bridge[] bridges;
    public Bridge[] Bridges => bridges;

    [SerializeField] ColorType brickMissing;
    public ColorType BrickMissing => brickMissing;
    [SerializeField] ColorData colorData;

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
        spawnPosArray = new Brick[numberBricks.x * numberBricks.y];
        bricksNum = new int[6];
        OnInit();
    }

    void Update()
    {
        BrickLeast();
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
                GameObject brickPos = Instantiate(spawnPosPrefab, transform);
                brickPos.transform.localPosition = new Vector3(i * offsetBricks.x, 0, j * offsetBricks.y);
                spawnPosArray[arrayLenght] = brickPos.GetComponent<Brick>();
                spawnPosArray[arrayLenght].State = this;
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
            brickMissing = ColorType.Red;
        }
        else if (bricksNum[0] == greenBricks)
        {
            brickMissing = ColorType.Green;
        }
        else if (bricksNum[0] == blueBricks)
        {
            brickMissing = ColorType.Blue;
        }
        else if (bricksNum[0] == yellowBricks)
        {
            brickMissing = ColorType.Yellow;
        }
        else if (bricksNum[0] == blackBricks)
        {
            brickMissing = ColorType.Black;
        }
        else if (bricksNum[0] == pinkBricks)
        {
            brickMissing = ColorType.Pink;
        }
    }

    public void BrickNum(ColorType colorType, int parameter)
    {
        switch(colorType)
        {
            case ColorType.Red:
                redBricks += parameter;
                break;
            case ColorType.Green:
                greenBricks += parameter;
                break;
            case ColorType.Blue:
                blueBricks += parameter;
                break;
            case ColorType.Yellow:
                yellowBricks += parameter;
                break;
            case ColorType.Black:
                blackBricks += parameter;
                break;
            case ColorType.Pink:
                pinkBricks += parameter;
                break;
        }
    }
}
