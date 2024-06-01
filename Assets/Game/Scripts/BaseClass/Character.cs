using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("GeneralSetting")]
    [SerializeField] protected Animator anim;
    protected string currentAnimID;
    [SerializeField] protected ColorType brickType;
    public ColorType BrickType => brickType;
    [SerializeField] protected ColorData colorData;
    [SerializeField] protected GameObject brickPrefab;
    [SerializeField] protected float moveSpeed;

    [Header("BrickSettings")]
    [SerializeField] protected Transform spawnBrick;
    [SerializeField] protected int brickCount;
    [SerializeField] protected List<GameObject> bricksList;
    public int BrickLeft => bricksList.Count;
    [SerializeField] protected float brickOffset = 0.15f;
    protected float brickWallHeight;

    // animation
    protected const string speedAnim = "Speed";
    protected const string idleAnim = "Idle";
    protected const string winAnim = "Win";
    protected const string loseAnim = "Lose";

    public virtual void OnInit()
    {
        currentAnimID = "None";
        ChangeAnim(idleAnim);
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
    public virtual void EndGame(string animName)
    {
        ChangeAnim(animName);
        ClearBrick();
    }

    public virtual void AddBrick()
    {
        brickCount++;
        GameObject brickClone = Instantiate(brickPrefab, spawnBrick);
        brickClone.transform.localPosition = new Vector3(0, brickWallHeight, 0);
        brickClone.GetComponent<Renderer>().material = colorData.GetMaterial(brickType);
        bricksList.Add(brickClone);
        brickWallHeight += brickOffset;
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
        brickType = (ColorType)colorIndex;
    }

    public void RemoveBrick()
    {
        if (bricksList.Count > 0)
        {
            Destroy(bricksList[bricksList.Count - 1]);
            bricksList.Remove(bricksList[bricksList.Count - 1]);
        }
        brickWallHeight -= brickOffset;
    }

    public void ClearBrick()
    {
        for (int i = bricksList.Count - 1; i >= 0; i--)
        {
            Destroy(bricksList[i]);
        }
        bricksList.Clear();
    }
}
