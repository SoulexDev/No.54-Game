using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour, IInteractable
{
    public AudioMixer mixer;
    public Light flashLight;
    public Transform cam;
    public Transform robotHead;
    public AudioSource audioSC;
    bool lookCam = false;
    public GameObject sally;
    public Transform playerCutscenePos;
    public Transform player;
    public GameObject endCredits;
    public CanvasGroup endText;

    public void Interact()
    {
        if (GameClockandWinConditions.complete)
            StartCoroutine(EndCutscene());
        else
            audioSC.Play();
    }

    IEnumerator EndCutscene()
    {
        CutScenePlaying.cutscenePlaying = true;
        flashLight.intensity = 120000;
        sally.SetActive(true);
        PlayerPaused.Paused = true;
        VolumeSetter.affectAudio = false;
        lookCam = true;
        yield return new WaitForSeconds(5);
        endCredits.SetActive(true);
        yield return new WaitForSeconds(2);
        while(endText.alpha < 1)
        {
            endText.alpha += 0.005f;
            yield return null;
        }
        yield return new WaitForSeconds(30);
        SceneManager.LoadScene(0);
    }
    private void Update()
    {
        if (lookCam)
        {
            float curvol;
            mixer.GetFloat("Volume", out curvol);
            mixer.SetFloat("Volume", Mathf.Lerp(curvol, -80, Time.deltaTime * 4));
            cam.SetPositionAndRotation(playerCutscenePos.position, Quaternion.Slerp(cam.rotation, Quaternion.LookRotation(robotHead.position - cam.position, Vector3.up), Time.deltaTime * 5));
        }
    }
}