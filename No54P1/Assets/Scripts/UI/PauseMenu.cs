using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    bool paused = false;
    public GameObject menu;
    public AudioMixer mixer;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !CutScenePlaying.cutscenePlaying)
        {
            if (paused)
            {
                paused = false;
                UpdateMenu();
            }
            else
            {
                paused = true;
                UpdateMenu();
            }
        }
    }
    void UpdateMenu()
    {
        menu.SetActive(paused);
        PlayerPaused.Paused = paused;
        if (paused)
        {
            VolumeSetter.affectAudio = false;
            mixer.SetFloat("Volume", -80);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
        else
        {
            VolumeSetter.affectAudio = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }
    }
}
