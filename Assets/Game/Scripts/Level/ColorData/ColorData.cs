using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mat", menuName = "Brick Mat")]
public class ColorData : ScriptableObject
{
    [Header("BrickMaterialSetting")]
    [SerializeField] List<Material> brickMat = new List<Material>();

    public Material GetMaterial(ColorType colorType)
    {
        return brickMat[(int)colorType];
    }
}
