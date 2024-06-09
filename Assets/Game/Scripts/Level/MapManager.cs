using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapManager : MonoBehaviour
{
    [field:SerializeField] public List<State> MapState { get; private set; }

    [field: SerializeField] public NavMeshData NavMeshData { get; private set; }

    [field: SerializeField] public WinPos WinPos { get; private set; }

    [field: SerializeField] public Transform StartPlayerTransform { get; private set; }

    [field: SerializeField] public List<Transform> StartBotTransforms { get; private set; }

    [field: SerializeField] public ELight MapLight { get; private set; }
}
