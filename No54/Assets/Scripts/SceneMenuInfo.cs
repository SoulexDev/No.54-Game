using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneMenuInfo : MonoBehaviour
{
    public Slider volSlider;
    public Slider sensSlider;

    public void ChangeVol()
    {
        SettingsSingleton.instance.volume = volSlider.value;
    }
    public void ChangeSens()
    {
        SettingsSingleton.instance.sensitivity = sensSlider.value;
    }
}