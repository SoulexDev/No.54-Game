using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCam : MonoBehaviour
{
    float x, y;
    Quaternion rot;
    private void Start()
    {
        rot = transform.rotation;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void Update()
    {
        x = Input.GetAxisRaw("Mouse X") * 0.2f;
        y = Input.GetAxisRaw("Mouse Y") * 0.2f;
        transform.rotation = Quaternion.Slerp(transform.rotation * Quaternion.Euler(-y, x, 0), rot, 0.05f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, Mathf.Max(0f, Quaternion.Angle(transform.rotation, rot) - 20));
    }
}