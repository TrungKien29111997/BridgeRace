using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension 
{
    public static T Last<T>(this List<T> list) where T: MonoBehaviour
    {
        //return list[list.Count - 1];
        return list[^1];
    } 
}
