using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameClockandWinConditions : MonoBehaviour
{
    public Animatronic robot1;
    public Animatronic robot2;
    public static bool complete = false;
    public TextMeshProUGUI timerText;
    public static float timer = 12;
    bool claimedComplete = false;
    private void Start()
    {
        timer = 12;
        complete = false;
        StartCoroutine(CheckBools());
        StartCoroutine(IncreaseTime());
    }
    IEnumerator CheckBools()
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);

        while (true)
        {
            yield return wait;
            if (timer == 6 && (!robot1.readyToShip || !robot2.readyToShip))
                StopAllCoroutines();
            complete = robot1.readyToShip && robot2.readyToShip;
            if(complete && !claimedComplete)
            {
                JobAssistant.Speak(0);
                claimedComplete = true;
            }
        }
    }
    IEnumerator IncreaseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(120);
            timer += 1;
            if(timer == 3 && !complete)
                JobAssistant.Speak(2);
            if (timer > 12)
                timer = 1;
            timerText.text = timer.ToString() + "am";
        }
    }
}