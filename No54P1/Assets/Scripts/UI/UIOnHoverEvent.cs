using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIOnHoverEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    float newScale = 1;
    bool hovering = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
    }
    private void Update()
    {
        if (hovering)
            newScale = 1.2f;
        else
            newScale = 1;
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(newScale, newScale, newScale), Time.fixedDeltaTime * 6);
    }
}