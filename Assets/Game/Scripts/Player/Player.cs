using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    Rigidbody rb;
    PlayerInputManager input;

    [SerializeField] GameObject mainCamera;

    Vector3 inputDir;
    float targetRotation;
    float rotationVelocity;
    float speed;
    const float RotationSmoothTime = 5f;
    const float SpeedChangeRate = 10f;
    bool isWin;
    float animationBlend;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInputManager>();
    }

    void Start()
    {
        isWin = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWin)
        {
            Control();
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        if (input.Move.magnitude > 0.1f && !isWin)
        {
            transform.forward = Vector3.Slerp(transform.forward, inputDir.normalized, Time.deltaTime * RotationSmoothTime);
            MoveRigidBody(transform.forward, speed);
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        isWin = false;
        transform.position = LevelManager.instance.StartPlayerTransform.position;
        transform.forward = LevelManager.instance.StartPlayerTransform.transform.forward;
    }

    public override void EndGame(string animName)
    {
        base.EndGame(animName);
        isWin = true;
    }

    void Control()
    {

        if (input.Move.magnitude < 0.1f) speed = 0.0f;
        else speed = moveSpeed;

        Vector3 tmpForward = new Vector3(mainCamera.transform.forward.x, 0f, mainCamera.transform.forward.z);
        Vector3 tmpRight = new Vector3(mainCamera.transform.right.x, 0f, mainCamera.transform.right.z);

        inputDir = (tmpForward * input.Move.y + tmpRight * input.Move.x).normalized;

        animationBlend = Mathf.Lerp(animationBlend, speed, Time.deltaTime * SpeedChangeRate);
        if (animationBlend < 0.01f) animationBlend = 0f;
        anim.SetFloat(speedAnim, animationBlend);
    }

    void MoveRigidBody(Vector3 direction, float speedLimit)
    {
        rb.velocity = direction * speedLimit + new Vector3(0, rb.velocity.y, 0);

        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > speedLimit)
        {
            Vector3 limitVel = flatVel.normalized * speedLimit;
            rb.velocity = new Vector3(limitVel.x, rb.velocity.y, limitVel.z);
        }
    }
}
