using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour
{
    public Image flashlightCharge;
    public Light bulb;
    float charge = 100;
    public Animatronic animatronics;
    public Transform armBone;
    private Quaternion ogRot;
    private Quaternion newRot;
    private void Start()
    {
        ogRot = armBone.localRotation;
        newRot = Quaternion.Euler(new Vector3(0.95f, 0.23f, 27.5f));
    }
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            animatronics.StopCoroutine(animatronics.SetTemporaryTarget(transform));
            animatronics.StartCoroutine(animatronics.SetTemporaryTarget(transform));
            animatronics.AlertAnimatronics();
        }
        if (Input.GetButton("Jump"))
        {
            Charge();
            bulb.enabled = false;
        }
        else
        {
            Deplete();
            if (charge == 0)
                bulb.enabled = false;
            else
                bulb.enabled = true;
        }
        flashlightCharge.fillAmount = charge / 100;
    }
    void Charge()
    {
        charge = Mathf.Clamp(charge + 6 * Time.deltaTime, 0, 100);
        armBone.localRotation = Quaternion.Slerp(armBone.localRotation, newRot, Time.deltaTime * 4);
    }
    void Deplete()
    {
        charge = Mathf.Clamp(charge - 1 * Time.deltaTime, 0, 100);
        armBone.localRotation = Quaternion.Slerp(armBone.localRotation, ogRot, Time.deltaTime * 7);
    }
}