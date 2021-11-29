using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakDoor : MonoBehaviour
{
    public void Break(Vector3 dir)
    {
        foreach (Rigidbody rb in transform.GetComponentsInChildren<Rigidbody>())
        {
            rb.AddForceAtPosition(dir * 100, transform.position + new Vector3(0, 1.5f, 0));
        }
    }
}