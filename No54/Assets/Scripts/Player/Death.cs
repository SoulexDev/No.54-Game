using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    public static bool playerDead = false;
    public AudioMixer mixer;
    public Transform playerCam;
    public CanvasGroup canvAlph;
    public GameObject deathScreen;
    public Light flashLight;
    public AudioSource jumpscareAudio;
    public Transform footBase;
    private Transform hold;
    private Transform look;
    private bool inHand = false;
    public AudioSource deathLineSC;
    private void Start()
    {
        playerDead = false;
        mixer.SetFloat("Volume", 0);
    }

    public IEnumerator JumpscareSequence(Transform facePos, Transform holdPos, Animator anim, Transform pos, AudioClip deathLine)
    {
        flashLight.intensity = 20000;
        playerDead = true;
        pos.position = footBase.position;
        pos.rotation = Quaternion.LookRotation((playerCam.position - pos.position).normalized, Vector3.up);
        anim.SetTrigger("Jumpscare");
        yield return new WaitForSeconds(0.1f);
        jumpscareAudio.Play();
        hold = holdPos;
        look = facePos;
        inHand = true;
        yield return new WaitForSeconds(1.6f);
        deathScreen.SetActive(true);
        while(canvAlph.alpha < 1)
        {
            canvAlph.alpha += 0.01f;
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        mixer.SetFloat("Volume", -80);
        deathLineSC.Stop();
        deathLineSC.PlayOneShot(deathLine);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        canvAlph.blocksRaycasts = true;
    }
    private void Update()
    {
        if (inHand)
            SetCamPos();
    }
    void SetCamPos()
    {
        playerCam.position = hold.position;
        playerCam.rotation = Quaternion.LookRotation((look.position - playerCam.position).normalized, Vector3.up);
    }
}