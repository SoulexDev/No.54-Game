using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgrammingComputer : MonoBehaviour
{
    public Animatronic mountedAnimatronic;
    public Image programmingFillBar;
    public bool playerSittingDown;

    //private void Update()
    //{
    //    if (mountedAnimatronic != null && playerSittingDown && Input.GetButton("Fire2"))
    //        Program();
    //}
    //void Program()
    //{
    //    mountedAnimatronic.programmingPercentage = Mathf.Clamp(mountedAnimatronic.programmingPercentage + 1 * Time.deltaTime, 0, 100);
    //    programmingFillBar.fillAmount = mountedAnimatronic.programmingPercentage / 100;
    //    if (mountedAnimatronic.programmingPercentage == 100)
    //        mountedAnimatronic.fullyProgrammed = true;
    //}
    //public void UpdateComputer()
    //{
    //    if(mountedAnimatronic != null)
    //        programmingFillBar.fillAmount = mountedAnimatronic.programmingPercentage / 100;
    //    else
    //        programmingFillBar.fillAmount = 0;
    //}
}