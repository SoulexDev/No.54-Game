using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobAssistant : MonoBehaviour
{
    public AudioSource sc;
    public AudioClip[] clips;
    static JobAssistant assistant;
    private void Start()
    {
        assistant = this;
    }
    public static void Speak(int clipNumber)
    {
        assistant.sc.PlayOneShot(assistant.clips[clipNumber]);
    }
}