using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public LayerMask interactionLayer;
    private LayerMask obstructionMask;
    public GameObject grabIcon;
    private Inventory inventory;
    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        obstructionMask = 1 << 2;
        obstructionMask = ~obstructionMask;
    }
    private void Update()
    {
        grabIcon.SetActive(Physics.Raycast(transform.position, transform.forward, 3, interactionLayer));
        if (Input.GetKeyDown(KeyCode.E) && !PlayerPaused.Paused)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.forward, out hit, 3, interactionLayer))
            {
                IInteractable interactable;
                interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                    interactable.Interact();
                IProgrammable programmable;
                programmable = hit.collider.GetComponent<IProgrammable>();
                if (programmable != null)
                    programmable.Program(inventory.usb[0]);
            }
        }
    }
}