using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCrouch : MonoBehaviour
{
    private Vector3 ogPos;
    private Vector3 newPos;
    private CPMPlayer player;
    private float robotFOV;
    private bool crouching = false;
    public Animatronic[] robots;
    void Start()
    {
        player = FindObjectOfType<CPMPlayer>();
        robotFOV = robots[0].viewAngle;
        ogPos = transform.localPosition;
        newPos = transform.localPosition - new Vector3(0, 0.5f, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !CutScenePlaying.cutscenePlaying)
        {
            player.crouchMult = 0.75f;
            robots[0].viewAngle = 160;
            robots[1].viewAngle = 160;
            crouching = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && !CutScenePlaying.cutscenePlaying)
        {
            player.crouchMult = 1;
            robots[0].viewAngle = robotFOV;
            robots[1].viewAngle = robotFOV;
            crouching = false;
        }
        if (crouching && !CutScenePlaying.cutscenePlaying)
            Crouch();
        else
            UnCrouch();
    }
    public void Crouch()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, newPos, Time.deltaTime * 8);
    }
    public void UnCrouch()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, ogPos, Time.deltaTime * 8);
    }
}