using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : Singleton<LevelManager>
{
    [Header("GeneralSettings")]
    public EGameState eGameState;
    [field: SerializeField] public Player Player { get; private set; }

    [SerializeField] Bot botPrefabs;
    [SerializeField] Transform environmentTransform;
    [SerializeField] BrickControl poolControl;

    [Header("LightSettings")]
    [SerializeField] Light sunLight;
    [SerializeField] Vector2 intensityDayNight;
    [SerializeField] Color dayColor;
    [SerializeField] Color nightColor;
    [SerializeField] Color ambientColor;

    [Header("MapSettings")]
    [SerializeField] List<MapManager> maps;
    [SerializeField] MapManager currentMap;
    [field: SerializeField] public int LevelCount { get; private set; }

    [Header("MapData")]
    [SerializeField] List<Transform> startBotTransforms = new List<Transform>();
    [field: SerializeField] public WinPos WinPos { get; private set; }
    [field: SerializeField] public List<State> MapState { get; private set; } = new List<State>();
    [field: SerializeField] public List<Bot> Bots { get; private set; } = new List<Bot>();
    [field: SerializeField] public Transform StartPlayerTransform { get; private set; }


    void Start()
    {
        LevelCount = 0;
        NewLevel(LevelCount);
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }

    void OnInit()
    {
        Player.OnInit();

        for (int i = 0; i < Bots.Count; i++)
        {
            Bots[i].TF.position = startBotTransforms[i].position;
            Bots[i].OnInit();
        }
    }

    void ReadMap(int mapIndex)
    {
        switch(maps[mapIndex].MapLight)
        {
            case ELight.Day:
                ChangeLight(ref sunLight, dayColor, intensityDayNight.x, ambientColor);
                break;
            case ELight.Night:
                ChangeLight(ref sunLight, nightColor, intensityDayNight.y, Color.black);
                break;
        }
        currentMap = Instantiate(maps[mapIndex], environmentTransform);
        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(maps[mapIndex].NavMeshData);
        MapState = currentMap.MapState;
        WinPos = currentMap.WinPos;
        StartPlayerTransform = currentMap.StartPlayerTransform;
        startBotTransforms = currentMap.StartBotTransforms;

        for (int i = 0; i < startBotTransforms.Count; i++)
        {
            Bot tmpBot = Instantiate(botPrefabs, startBotTransforms[i].position, Quaternion.identity);
            tmpBot.SetBrickType(i + 1);
            tmpBot.SetMaterial(i + 1);
            Bots.Add(tmpBot);
        }
    }

    public void EndGame(Character winChar)
    {
        ChangeLight(ref sunLight, dayColor, intensityDayNight.x, ambientColor);
        winChar.EndGame(Constant.ANIM_WIN, 0);
        UIManager.Instance.CloseAll();
        if (winChar.CompareTag(Constant.TAG_PLAYER))
        {
            UIManager.Instance.OpenUI<CanvasVictory>();
        }
        else
        {
            UIManager.Instance.OpenUI<CanvasFail>();
            Player.EndGame(Constant.ANIM_LOSE, 1);
        }
    }

    void ChangeLight(ref Light tmpLight, Color tmpColor, float tmpIntensity, Color tmpAmbient)
    {
        tmpLight.color = tmpColor;
        tmpLight.intensity = tmpIntensity;
        RenderSettings.ambientLight = tmpAmbient;
    }

    void NewLevel(int levelIndex)
    {
        SimplePool<EBrickType>.ReleaseAll();
        poolControl.OnInit();
        if (currentMap != null)
        {
            Destroy(currentMap.gameObject);
            for (int i = 0; i < Bots.Count; i++)
            {
                Destroy(Bots[i].gameObject);
            }
            Bots.Clear();
            MapState.Clear();
            startBotTransforms.Clear();
        }
        ReadMap(levelIndex);
        OnInit();
    }
    public void ResetLevel()
    {
        NewLevel(LevelCount);
    }

    public void NextLevel()
    {
        if (LevelCount < maps.Count - 1)
        {
            NewLevel(++LevelCount);
        }
        else
        {
            LevelCount = 0;
            NewLevel(0);
        }
    }

    public void SetState(EGameState state)
    {
        eGameState = state;
    }
}
