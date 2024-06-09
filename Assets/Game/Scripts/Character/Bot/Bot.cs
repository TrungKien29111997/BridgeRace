using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [Header("BotComponent")]
    [SerializeField] NavMeshAgent nav;
    [field: SerializeField] public SkinnedMeshRenderer[] SkinnedMesh { get; private set; }

    //StateData
    [SerializeField] int maxBrickCarry;
    [SerializeField] State currentBrickState;
    [SerializeField] int currentBrickStateIndex;
    [SerializeField] BrickPos currentBrick;
    [SerializeField] int bridgeSelected;
    const int tolerance = 3;
    int brickCarry;
    bool findBrickBool;
    IState<Bot> currentState;

    float currentHorizontalSpeed;
    float CurrentHorizontalSpeed
    {
        get
        {
            if (nav != null)
            {
                currentHorizontalSpeed = new Vector3(nav.velocity.x, 0.0f, nav.velocity.z).magnitude;
            }
            return currentHorizontalSpeed;
        }
    }

    protected override void Update()
    {
        base.Update();
        if (isStop)
        {
            nav.speed = 0;
            return;
        }
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

    public override void EndGame(string animName, int place)
    {
        base.EndGame(animName, place);
        nav.enabled = false;
        ChangeState(Constant.idleState);
    }

    public override void AddBrick()
    {
        base.AddBrick();
        findBrickBool = false;
    }
    void ChangeState(IState<Bot> newState)
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
        nav.speed = moveSpeed;

        anim.SetFloat(Constant.ANIM_SPEED, CurrentHorizontalSpeed);

        if (nav.speed < 0.1f)
        {
            anim.SetFloat(Constant.ANIM_SPEED, 0f);
        }
    }
    void NewState(int currentIndex)
    {
        currentBrickState = LevelManager.Instance.MapState[currentIndex];
        bridgeSelected = Random.Range(0, currentBrickState.Bridges.Length);
        ChangeState(Constant.findBrickState);
    }

    public void EnterFindBrick()
    {
        findBrickBool = false;
        brickCarry = maxBrickCarry + Random.Range(-tolerance, +tolerance);
    }

    public void FindBrick()
    {
        if (!findBrickBool)
        {
            for (int i = 0; i < currentBrickState.SpawnPosArray.Length; i++)
            {
                if (currentBrickState.SpawnPosArray[i].BrickType == this.BrickType && currentBrickState.SpawnPosArray[i].Brick != null)
                {
                    currentBrick = currentBrickState.SpawnPosArray[i];
                    nav.SetDestination(currentBrick.TF.position);
                    findBrickBool = true;
                }
            }
        }

        if (bricksList.Count > brickCarry)
        {
            ChangeState(Constant.buildBrickState);
        }
    }

    public void BuildBridge()
    {
        nav.SetDestination(currentBrickState.Bridges[bridgeSelected].CurrentDoor.TF.position);
        if (bricksList.Count < 1)
        {
            ChangeState(Constant.findBrickState);
        }
    }

    public void NextState()
    {
        if (currentBrickStateIndex < LevelManager.Instance.MapState.Count -1)
        {
            NewState(++currentBrickStateIndex);
        }
        else
        {
            ChangeState(Constant.idleState);
        }
    }



    public void SetMaterial(int colorIndex)
    {
        for (int i = 0; i < SkinnedMesh.Length; i++)
        {
            SkinnedMesh[i].material = colorData.GetMaterial((EBrickType)colorIndex);
        }
    }
}
