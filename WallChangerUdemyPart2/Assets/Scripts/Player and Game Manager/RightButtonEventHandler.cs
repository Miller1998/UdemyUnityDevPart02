using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightButtonEventHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        Player.left = false;
        Player.right = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Player.left = false;
        Player.right = false;
    }

}
