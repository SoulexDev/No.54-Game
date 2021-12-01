using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VHStape : MonoBehaviour, IInteractable
{
    public AudioClip clip;
    CamRecorder camRecorder;
    private void Start()
    {
        camRecorder = FindObjectOfType<CamRecorder>();
    }
    public void Interact()
    {
        camRecorder.PlayAudio(this);
    }
}