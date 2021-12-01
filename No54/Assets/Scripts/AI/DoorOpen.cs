using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public DoorScript door;
    private void OnTriggerEnter(Collider other)
    {
        if (!door.Opened)
        {
            if (other.tag == "Robot")
            {
                door.OpenDoor();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (door.Opened)
        {
            if (other.tag == "Robot")
            {
                door.CloseDoor();
            }
        }
    }
}