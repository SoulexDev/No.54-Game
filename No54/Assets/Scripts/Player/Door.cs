using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private CPMPlayer player;
    bool open = false;
    Quaternion rotTarget;
    Quaternion ogRot;
    Quaternion openTargetDot1;
    Quaternion openTargetDot2;

    private void Start()
    {
        player = FindObjectOfType<CPMPlayer>();
        ogRot = transform.rotation;
        openTargetDot1 = Quaternion.Euler(ogRot.eulerAngles.x, ogRot.eulerAngles.y, ogRot.eulerAngles.z + 100);
        openTargetDot2 = Quaternion.Euler(ogRot.eulerAngles.x, ogRot.eulerAngles.y, ogRot.eulerAngles.z - 100);
    }
    public void Interact()
    {
        if (open)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
    }
    void OpenDoor()
    {
        Vector3 doorInverse = transform.InverseTransformPoint(player.transform.position);
        open = true;
        if (doorInverse.x > 0)
            rotTarget = openTargetDot1;
        else
            rotTarget = openTargetDot2;
    }
    void CloseDoor()
    {
        open = false;
        rotTarget = ogRot;
    }
    private void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, rotTarget, Time.deltaTime * 5);
    }
}