using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Floatf
{
    public static float Distance(float floatOne, float floatTwo)
    {
        return Mathf.Abs(floatOne - floatTwo);
    }
    public static bool Equal(float floatOne, float floatTwo)
    {
        return floatOne == floatTwo;
    }
}
