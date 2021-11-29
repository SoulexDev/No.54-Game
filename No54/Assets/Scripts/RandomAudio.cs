using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudio : MonoBehaviour
{
    public AudioClip[] randomClips;
    public Transform[] randomPositions;
    AudioSource source;
    AudioReverbZone zone;
    System.Random r;
    System.Random e;
    System.Random p;
    private void Start()
    {
        source = GetComponent<AudioSource>();
        zone = GetComponent<AudioReverbZone>();
        zone.enabled = false;
        r = new System.Random();
        e = new System.Random();
        p = new System.Random();
        StartCoroutine(PlaySound());
    }
    IEnumerator PlaySound()
    {
        while (true)
        {
            int pn = p.Next(0, randomPositions.Length);
            int en = e.Next(0, 35);
            int rn = r.Next(0, randomClips.Length);
            if (rn == randomClips.Length)
                rn = randomClips.Length - 1;
            if (pn == randomPositions.Length)
                pn = randomPositions.Length - 1;
            yield return new WaitForSeconds(en);
            transform.position = randomPositions[pn].position;
            zone.enabled = true;
            source.PlayOneShot(randomClips[rn]);
            while (source.isPlaying)
            {
                yield return null;
            }
            zone.enabled = false;
            yield return null;
        }
    }
}