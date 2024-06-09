using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("GeneralSetting")]
    [SerializeField] protected Animator anim;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected Transform brickAtBack;

    [Header("BrickSettings")]
    [SerializeField] protected Brick brickPrefab;
    [SerializeField] protected float brickOffset = 0.15f;
    [field: SerializeField] public EBrickType BrickType { get; private set; }
    [SerializeField] protected ColorData colorData;


    protected List<Brick> bricksList = new List<Brick>();
    public int BrickLeft => bricksList.Count;

    protected float brickWallHeight;
    protected string currentAnimID;
    [SerializeField] protected bool isStop;
    protected int brickCount;

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

    protected virtual void Start()
    {
        isStop = false;
    }

    protected virtual void Update()
    {
        if (LevelManager.Instance.eGameState == EGameState.GamePlay)
        {
            isStop = false;
        }
        else
        {
            isStop = true;
        }
    }

    public virtual void OnInit()
    {
        isStop = false;
        currentAnimID = Constant.ANIM_NONE;
        ChangeAnim(Constant.ANIM_IDLE);
        brickWallHeight = 0;
        ClearBrick();
    }

    public virtual void OnDespawn()
    {

    }

    protected virtual void OnDead()
    {
        Invoke(nameof(OnDespawn), 1f);
    }
    public virtual void EndGame(string animName, int place)
    {
        ChangeAnim(animName);
        ClearBrick();
        TF.position = LevelManager.Instance.WinPos.WinPoss[place].position;
        TF.forward = LevelManager.Instance.WinPos.WinPoss[place].forward;
    }

    public void ChangeAnim(string animID)
    {
        if (currentAnimID != animID)
        {
            anim.ResetTrigger(currentAnimID);
            currentAnimID = animID;
            anim.SetTrigger(currentAnimID);
        }
    }

    public void SetBrickType(int colorIndex)
    {
        BrickType = (EBrickType)colorIndex;
    }
    public virtual void AddBrick()
    {
        brickCount++;
        Brick brickClone = Instantiate(brickPrefab, brickAtBack);
        brickClone.TF.localPosition = new Vector3(0, brickWallHeight, 0);

        //TODO: tranh getcomponent => OK
        brickClone.BrickRen.material = colorData.GetMaterial(BrickType);
        bricksList.Add(brickClone);
        brickWallHeight += brickOffset;
    }

    public void RemoveBrick()
    {
        if (BrickLeft > 0)
        {
            Brick brick = bricksList[bricksList.Count - 1];
            bricksList.Remove(brick);
            Destroy(brick.gameObject);
            brickWallHeight -= brickOffset;
        }
    }

    public void ClearBrick()
    {
        if (BrickLeft > 0)
        {
            for (int i = bricksList.Count - 1; i >= 0; i--)
            {
                Destroy(bricksList[i].gameObject);
            }
            bricksList.Clear();
        }
    }
}
