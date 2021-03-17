using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeftButtonEventHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Player.left = true;
        Player.right = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Player.left = false;
        Player.right = false;
    }

}
