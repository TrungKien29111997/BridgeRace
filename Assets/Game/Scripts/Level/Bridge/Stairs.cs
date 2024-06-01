using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    [SerializeField] ColorData colorData;
    [SerializeField] Renderer stairRen;

    [SerializeField] ColorType currentBrickType;

    [SerializeField] ColorType brickType;
    public ColorType BrickType => brickType;
    [SerializeField] bool changeColor;
    public bool ChangeColor => changeColor;

    [SerializeField] Character currentChar;
    public Character CurrentChar => currentChar;

    void Start()
    {
        changeColor = false;
        currentBrickType = brickType;
        stairRen.material = colorData.GetMaterial(currentBrickType);
    }

    private void Update()
    {
        if (currentBrickType != brickType)
        {
            currentBrickType = brickType;
            changeColor = true;
        }
        else
        {
            changeColor = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Bot"))
        {
            Character tempChar = other.GetComponent<Character>();

            if (tempChar.BrickLeft > 0)
            {
                if (tempChar.BrickType != this.brickType)
                {
                    tempChar.RemoveBrick();
                }
                stairRen.material = colorData.GetMaterial(tempChar.BrickType);
                brickType = tempChar.BrickType;
                currentChar = tempChar;
            }
        }
    }
}
