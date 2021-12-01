using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSetter : MonoBehaviour
{
    public AudioMixer mixer;
    public static bool affectAudio;
    private void Start()
    {
        affectAudio = true;
    }
    void Update()
    {
        if(affectAudio)
            mixer.SetFloat("Volume", SettingsSingleton.instance.volume);
    }
}