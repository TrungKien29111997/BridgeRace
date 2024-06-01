using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] Animator anim;
    [SerializeField] Transform joyStickTransform;
    [SerializeField] Image[] imageControl;

    string currentAnim;
    bool activeJoyStick;

    const string victoryAnim = "Victory";
    const string loseAnim = "Lose";
    const string idleAnim = "Idle";
    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (activeJoyStick)
        {
            if (!Input.GetMouseButton(0))
            {
                joyStickTransform.position = Input.mousePosition;
            }
            if (Input.GetMouseButtonDown(0))
            {
                SetJoyStickAlpha(1);
            }
            if (Input.GetMouseButtonUp(0))
            {
                SetJoyStickAlpha(0);
            }
        }
    }

    public void DiactiveJoyStick()
    {
        activeJoyStick = false;
        SetJoyStickAlpha(0);
    }

    void SetJoyStickAlpha(float alpha)
    {
        for (int i = 0; i < imageControl.Length; i++)
        {
            imageControl[i].color = new Vector4(1, 1, 1, alpha);
        }
    }

    public void OnInit()
    {
        activeJoyStick = true;
        currentAnim = "None";
        ChangeAnim(idleAnim);
        SetJoyStickAlpha(0);
    }
    public void WinUIAni()
    {
        ChangeAnim(victoryAnim);
    }

    public void LoseUIAni()
    {
        ChangeAnim(loseAnim);
    }

    public void ChangeAnim(string animID)
    {
        if (currentAnim != animID)
        {
            anim.ResetTrigger(currentAnim);
            currentAnim = animID;
            anim.SetTrigger(currentAnim);
        }
    }
}


