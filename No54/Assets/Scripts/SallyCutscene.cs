using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SallyCutscene : MonoBehaviour
{
    public AudioSource ac;
    public AudioClip whir;
    public AudioClip footStep;
    public void PlayFootStep()
    {
        ac.PlayOneShot(footStep);
    }
    public void PlayWhir()
    {
        ac.PlayOneShot(whir);
    }
}