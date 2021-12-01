using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class JobIncomplete : MonoBehaviour
{
    bool endingInitiated = false;
    public CanvasGroup overlay;
    public VideoPlayer player;
    public GameObject videoImage;
    public AudioMixer mixer;
    private void Start()
    {
        endingInitiated = false;
    }
    private void Update()
    {
        if (!endingInitiated && GameClockandWinConditions.timer == 6)
        {
            StartCoroutine(PlayEndingVHS());
            endingInitiated = true;
        }
    }
    IEnumerator PlayEndingVHS()
    {
        mixer.SetFloat("Volume", -80);
        CutScenePlaying.cutscenePlaying = true;
        overlay.gameObject.SetActive(true);
        while(overlay.alpha < 1)
        {
            overlay.alpha += 0.01f;
            yield return null;
        }
        videoImage.SetActive(true);
        player.Play();
        yield return new WaitForSeconds(13.5f);
        SceneManager.LoadScene(1);
    }
}