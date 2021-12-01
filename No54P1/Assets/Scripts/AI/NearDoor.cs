using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearDoor : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject brokenDoor;
    [SerializeField] private Transform robotPos;
    public static bool playerNearDoor = false;
    public static GameObject currentDoor;
    public static Vector3 currentDoorPos;
    public static Quaternion currentDoorRot;
    public static Transform currentRobotPos;
    private static NearDoor nearDoor;

    private void Start()
    {
        nearDoor = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        currentRobotPos = robotPos;
        if(door != null)
        {
            currentDoor = door;
            currentDoorPos = currentDoor.transform.position;
            currentDoorRot = currentDoor.transform.rotation;
        }
        playerNearDoor = true;
    }
    private void OnTriggerExit(Collider other)
    {
        playerNearDoor = false;
    }
    public static void Break(Vector3 dir)
    {
        BreakDoor newDoor = Instantiate(nearDoor.brokenDoor, currentDoorPos, currentDoorRot).GetComponent<BreakDoor>();
        newDoor.Break(dir);
        Destroy(currentDoor);
    }
}