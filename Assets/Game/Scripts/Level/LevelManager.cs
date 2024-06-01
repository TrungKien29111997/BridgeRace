using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [Header("GeneralSettings")]
    [SerializeField] Character player;
    [SerializeField] GameObject botPrefabs;
    [SerializeField] Transform environmentTransform;

    [Header("MapSettings")]
    [SerializeField] List<MapManager> maps;
    [SerializeField] MapManager currentMap;
    [SerializeField] int levelCount;
    public int CurrentLevel => levelCount;

    [Header("MapData")]
    [SerializeField] List<State> mapState;
    public List<State> MapState => mapState;
    [SerializeField] List<Bot> bots;
    public List<Bot> Bots => bots;
    [SerializeField] List<Transform> startBotTransforms;
    [SerializeField] WinPos winPos;
    [SerializeField] Transform startPlayerTransform;
    public Transform StartPlayerTransform => startPlayerTransform;
    [SerializeField] GameObject startBotPrefab;

    // bot state machine
    public static IStateBot idleState = new IdleBot();
    public static IStateBot findBrickState = new FindBrickBot();
    public static IStateBot buildBrickState = new BuildBrickBot();

    bool endGameBool;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        levelCount = 0;
        NewLevel(levelCount);
    }

    void Update()
    {
        if (mapState.Count > 0)
        {
            if (mapState[mapState.Count - 1].Bridges[0].StairsList.Count > 0)
            {
                if (!endGameBool && mapState[mapState.Count - 1].Bridges[0].StairsList[mapState[mapState.Count - 1].Bridges[0].StairsList.Count - 1].BrickType != ColorType.None)
                {
                    EndGame();
                    endGameBool = true;
                }
            }
        }
    }

    void OnInit()
    {
        UIManager.instance.OnInit();
        endGameBool = false;
        player.OnInit();

        for (int i = 0; i < bots.Count; i++)
        {
            bots[i].transform.position = startBotTransforms[i].position;
            bots[i].OnInit();
        }
    }
    void NewLevel(int levelIndex)
    {
        if (currentMap != null)
        {
            Destroy(currentMap.gameObject);
            for (int i = 0; i < bots.Count; i++)
            {
                Destroy(bots[i].gameObject);
            }
            bots.Clear();
            mapState.Clear();
            startBotTransforms.Clear();
        }
        ReadMap(levelIndex);
        OnInit();
    }

    void ReadMap(int mapIndex)
    {
        currentMap = Instantiate(maps[mapIndex], environmentTransform);
        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(maps[mapIndex].NavMeshData);
        mapState = currentMap.MapState;
        winPos = currentMap.WinPos;
        startPlayerTransform = currentMap.StartPlayerTransform;
        startBotPrefab = currentMap.StartBotPrefab;

        foreach (Transform t in startBotPrefab.transform)
        {
            startBotTransforms.Add(t);
        }
        for (int i = 0; i < startBotTransforms.Count; i++)
        {
            GameObject tmpBotObj = Instantiate(botPrefabs, startBotTransforms[i].position, Quaternion.identity);
            Bot tmpBot = tmpBotObj.GetComponent<Bot>();
            tmpBot.SetBrickType(i + 1);
            tmpBot.SetMaterial(i + 1);
            bots.Add(tmpBot);
        }
    }

    void EndGame()
    {
        Character winChar = mapState[mapState.Count - 1].Bridges[0].StairsList[mapState[mapState.Count - 1].Bridges[0].StairsList.Count - 1].CurrentChar;
        CharEndgame("Win", ref winChar, 0);

        if (winChar.CompareTag("Player"))
        {
            UIManager.instance.WinUIAni();

        }
        else
        {
            UIManager.instance.LoseUIAni();
            CharEndgame("Lose", ref player, 1);
        }
        UIManager.instance.DiactiveJoyStick();
    }

    void CharEndgame(string animName, ref Character character, int place)
    {
        character.EndGame(animName);
        character.transform.position = winPos.WinPoss[place].transform.position;
        character.transform.forward = winPos.WinPoss[place].transform.forward;
    }

    public void ResetLevel()
    {
        NewLevel(levelCount);
    }

    public void NextLevel()
    {
        if (levelCount < maps.Count - 1)
        {
            NewLevel(++levelCount);
        }
        else
        {
            levelCount = 0;
            NewLevel(0);
        }
    }
}
public enum ColorType
{
    None = 0,
    Red = 1,
    Green = 2,
    Blue = 3,
    Yellow = 4,
    Black = 5,
    Pink = 6
}
