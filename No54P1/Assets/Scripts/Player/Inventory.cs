using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<USB> usb = new List<USB>();
    public void AddItem(USB newUSB)
    {
        JobAssistant.Speak(1);
        usb.Add(newUSB);
    }
}