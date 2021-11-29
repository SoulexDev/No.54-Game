using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class USB : MonoBehaviour, IInteractable
{
    public Color usbColor;
    private Inventory inventory;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    public void Interact()
    {
        if (inventory.usb.Count > 0)
            return;
        else
        {
            inventory.AddItem(this);
            gameObject.SetActive(false);
        }
    }
}