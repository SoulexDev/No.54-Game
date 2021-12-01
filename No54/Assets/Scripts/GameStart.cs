using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public AudioSource sc;
    public CanvasGroup overlay;

    private void Start()
    {
        Time.timeScale = 1;
        StartCoroutine(GameInit());
    }
    IEnumerator GameInit()
    {
        sc.Play();
        while (overlay.alpha > 0)
        {
            overlay.alpha -= 0.01f;
            yield return null;
        }
        overlay.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
    }
}