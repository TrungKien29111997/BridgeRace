using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform target;
    [SerializeField] Vector3 offset;
    public float lerpSpeed = 10;

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

    void Start()
    {
        target = GameObject.FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        TF.position = Vector3.Lerp(TF.position, target.position, Time.deltaTime * lerpSpeed);
    }
}
