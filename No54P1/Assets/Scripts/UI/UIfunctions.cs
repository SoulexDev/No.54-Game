using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.Audio;

public class UIfunctions : MonoBehaviour
{
    public GameObject vPlayer;
    public VideoPlayer player;
    public CanvasGroup overlay;
    public AudioMixer mixer;
    bool startedGame = false;
    private void Start()
    {
        startedGame = false;
    }
    public void StartGame()
    {
        if (!startedGame)
        {
            StartCoroutine(PlayVHS());
            startedGame = true;
        }
    }
    IEnumerator PlayVHS()
    {
        VolumeSetter.affectAudio = false;
        mixer.SetFloat("Volume", -80);
        while (overlay.alpha < 1)
        {
            overlay.alpha += 0.005f;
            yield return null;
        }
        vPlayer.SetActive(true);
        player.Play();
        yield return new WaitForSeconds(0.2f);
        overlay.alpha = 0;
        yield return new WaitForSeconds(26);
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }
    public void LoadNewScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void Restart()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }
}