using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    Rigidbody rb;
    PlayerController controller;

    [SerializeField] Transform mainCamera;
    Vector2 inputMove;

    Vector2 InputMove
    {
        get
        {
            if (controller != null)
            {
                if (controller.MobieActive)
                {
                    inputMove = new Vector2(controller.InputMobie.x, controller.InputMobie.y);
                }
                else
                {
                    inputMove = new Vector2(Input.GetAxisRaw(Constant.AXIS_HORIZONTAL), Input.GetAxisRaw(Constant.AXIS_VETICAL));
                }
            }
            return inputMove;
        }
    }

    Vector3 inputDir;
    float speed;
    const float RotationSmoothTime = 15f;
    const float SpeedChangeRate = 15f;
    float animationBlend;

    Vector3 tmpForward;
    Vector3 TmpForward
    {
        get
        {
            if (mainCamera != null)
            {
                tmpForward = new Vector3(mainCamera.forward.x, 0f, mainCamera.forward.z);
            }
            return tmpForward;
        }
    }

    Vector3 tmpRight;
    Vector3 TmpRight
    {
        get
        {
            if (mainCamera != null)
            {
                tmpRight = new Vector3(mainCamera.right.x, 0f, mainCamera.right.z);
            }
            return tmpRight;
        }
    }

    Vector3 Gravity => new Vector3(0, rb.velocity.y, 0);

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected override void Update()
    {
        if (isStop)
        {
            rb.velocity = Vector3.zero;
        }
        base.Update();
        Control();
    }

    void FixedUpdate()
    {
        if (InputMove.sqrMagnitude > 0.1f && !isStop)
        {
            if (controller != null)
            {
                if (controller.MobieActive)
                {
                    TF.forward = inputDir;
                }
                else
                {
                    TF.forward = Vector3.Slerp(TF.forward, inputDir, Time.fixedDeltaTime * RotationSmoothTime);
                }
            }
            rb.velocity = TF.forward * speed * Time.fixedDeltaTime + Gravity;
        }
    }

    public override void OnInit()
    {
        base.OnInit();

        //TODO: cache transform => OK
        TF.position = LevelManager.Instance.StartPlayerTransform.position;
        TF.forward = LevelManager.Instance.StartPlayerTransform.forward;
        rb.velocity = Vector3.zero;
    }

    public void SetController(PlayerController tmpController)
    {
        controller = tmpController;
    }

    void Control()
    {

        if (InputMove.sqrMagnitude < 0.1f)
        {
            speed = 0.0f;
            rb.drag = 5;
        }
        else
        {
            speed = moveSpeed;
            rb.drag = 0;
        }
        
        inputDir = (TmpForward * InputMove.y + TmpRight * InputMove.x).normalized;

        animationBlend = Mathf.Lerp(animationBlend, speed, Time.deltaTime * SpeedChangeRate);

        if (animationBlend < 0.01f) animationBlend = 0f;
        anim.SetFloat(Constant.ANIM_SPEED, animationBlend);
    }
}
