using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRecorder : MonoBehaviour
{
    [SerializeField] private AudioSource aSource;
    private Inventory inventory;
    [SerializeField] private AudioClip[] vhsEffects;

    private void Start()
    {
        //aSource = GetComponent<AudioSource>();
    }
    public void PlayAudio(VHStape tape)
    {
        if (aSource.isPlaying)
            return;
        StartCoroutine(VHSplay(tape));
    }
    IEnumerator VHSplay(VHStape tape)
    {
        Destroy(tape.gameObject);
        aSource.PlayOneShot(vhsEffects[0]);
        while (aSource.isPlaying)
        {
            yield return null;
        }
        aSource.PlayOneShot(tape.clip);
        while (aSource.isPlaying)
        {
            yield return null;
        }
        aSource.PlayOneShot(vhsEffects[1]);
    }
}