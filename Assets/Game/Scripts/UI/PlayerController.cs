using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform joyStickTransform;
    [SerializeField] Transform joyTransform;
    [SerializeField] int range;
    [SerializeField] Image[] imageControl;

    Vector3 startPress;
    [field: SerializeField] public Vector3 InputMobie { get; private set; }

    [field: SerializeField] public bool MobieActive { get; private set; }

    void Update()
    {
        if (LevelManager.Instance.eGameState == EGameState.GamePlay)
        {
            if (!Input.GetMouseButton(0))
            {
                MobieActive = false;
                joyStickTransform.position = Input.mousePosition;
                InputMobie = Vector3.zero;
            }
            else
            {
                MobieActive = true;
                InputMobie = (Input.mousePosition - startPress).normalized;
                joyTransform.position = startPress + InputMobie * range;
            }
            if (Input.GetMouseButtonDown(0))
            {
                SetJoyStickAlpha(1);
                startPress = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                SetJoyStickAlpha(0);
            }
        }
        else
        {
            SetJoyStickAlpha(0);
        }
    }

    public void SetJoyStickAlpha(float alpha)
    {
        for (int i = 0; i < imageControl.Length; i++)
        {
            imageControl[i].color = new Vector4(1, 1, 1, alpha);
        }
    }
}


