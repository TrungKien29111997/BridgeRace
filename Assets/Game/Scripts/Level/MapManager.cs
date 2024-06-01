using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapManager : MonoBehaviour
{
    [SerializeField] List<State> mapState;
    public List<State> MapState => mapState;
    [SerializeField] NavMeshData navMeshData;
    public NavMeshData NavMeshData => navMeshData;

    [SerializeField] WinPos winPos;
    public WinPos WinPos => winPos;
    [SerializeField] Transform startPlayerTransform;
    public Transform StartPlayerTransform => startPlayerTransform;

    [SerializeField] GameObject startBotPrefab;
    public GameObject StartBotPrefab => startBotPrefab;
}
