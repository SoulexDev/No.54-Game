using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomUSBpositions : MonoBehaviour
{
    public GameObject[] USBs;
    public Transform[] positions;
    System.Random r = new System.Random();
    System.Random t = new System.Random();
    private void Start()
    {
        int rn = r.Next(0, positions.Length);
        if (rn == positions.Length)
            rn = positions.Length - 1;

        int tn = t.Next(0, positions.Length);
        if (tn == positions.Length)
            tn = positions.Length - 1;

        if (tn == rn)
            tn = rn + 2;
        if (tn >= positions.Length)
            tn = 0;
        USBs[0].transform.SetPositionAndRotation(positions[rn].position, Quaternion.Euler(-90, 0, 30));
        USBs[1].transform.SetPositionAndRotation(positions[tn].position, Quaternion.Euler(-90, 0, 30));
    }
}