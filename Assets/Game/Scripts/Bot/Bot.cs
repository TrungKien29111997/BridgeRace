using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    NavMeshAgent nav;

    [Header("StateData")]
    [SerializeField] State currentBrickState;
    [SerializeField] int currentBrickStateIndex;
    [SerializeField] Brick currentBrick;
    [SerializeField] int maxBrickCarry;
    const int tolerance = 3;
    [SerializeField] int bridgeSelected;
    int brickCarry;
    bool findBrickBool;

    [SerializeField] SkinnedMeshRenderer[] skinnedMesh;
    public SkinnedMeshRenderer[] SkinnedMesh => skinnedMesh;

    IStateBot currentState;
    [SerializeField] EcurrentState ecurrentState;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
        Control();
    }

    public override void OnInit()
    {
        base.OnInit();
        currentBrickStateIndex = 0;
        NewState(currentBrickStateIndex);
    }

    public override void EndGame(string animName)
    {
        base.EndGame(animName);
        nav.enabled = false;
        ChangeState(LevelManager.idleState);
    }

    public override void AddBrick()
    {
        base.AddBrick();
        findBrickBool = false;
    }
    public void ChangeState(IStateBot newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    void Control()
    {
        float currentHorizontalSpeed = new Vector3(nav.velocity.x, 0.0f, nav.velocity.z).magnitude;

        nav.speed = moveSpeed;

        anim.SetFloat(speedAnim, currentHorizontalSpeed);

        if (nav.speed < 0.1f)
        {
            anim.SetFloat(speedAnim, 0f);
        }
    }

    public void EnterIdleState()
    {
        ecurrentState = EcurrentState.None;
    }

    public void EnterFindBrick()
    {
        ecurrentState = EcurrentState.FindBrick;
        findBrickBool = false;
        brickCarry = maxBrickCarry + Random.Range(-tolerance, +tolerance);
    }
    public void FindBrick()
    {
        if (!findBrickBool)
        {
            for (int i = 0; i < currentBrickState.SpawnPosArray.Length; i++)
            {
                if (currentBrickState.SpawnPosArray[i].BrickType == this.brickType && currentBrickState.SpawnPosArray[i].BrickObj != null)
                {
                    currentBrick = currentBrickState.SpawnPosArray[i];
                    nav.SetDestination(currentBrick.transform.position);
                    findBrickBool = true;
                }
            }
        }

        if (bricksList.Count > brickCarry)
        {
            ChangeState(LevelManager.buildBrickState);
        }
    }

    public void EnterBuildBridge()
    {
        ecurrentState = EcurrentState.BuildBridge;
    }
    public void BuildBridge()
    {
        nav.SetDestination(currentBrickState.Bridges[bridgeSelected].StairsList[currentBrickState.Bridges[bridgeSelected].StairsList.Count - 1].transform.position);
        if (bricksList.Count < 1)
        {
            ChangeState(LevelManager.findBrickState);
        }
    }

    public void NextState()
    {
        if (currentBrickStateIndex < LevelManager.instance.MapState.Count -1)
        {
            NewState(++currentBrickStateIndex);
        }
        else
        {
            ChangeState(LevelManager.idleState);
        }
    }

    void NewState(int currentIndex)
    {
        currentBrickState = LevelManager.instance.MapState[currentIndex];
        bridgeSelected = Random.Range(0, currentBrickState.Bridges.Length);
        ChangeState(LevelManager.findBrickState);
    }

    public void SetMaterial(int colorIndex)
    {
        for (int i = 0; i < skinnedMesh.Length; i++)
        {
            skinnedMesh[i].material = colorData.GetMaterial((ColorType)colorIndex);
        }
    }

    enum EcurrentState
    {
        None = 0,
        FindBrick = 1,
        BuildBridge = 2
    }
}
