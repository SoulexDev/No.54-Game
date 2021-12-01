using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaused : MonoBehaviour
{
    public static bool Paused = false;
    private void Start()
    {
        Paused = false;
    }
}