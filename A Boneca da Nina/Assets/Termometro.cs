using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Termometro : MonoBehaviour, IPointerClickHandler
{
    void OnGUI()
    {
        // Event e = Event.current;
        // if (e.isMouse)
        // {
        //     Debug.Log("Mouse clicks: " + e.clickCount);
        // }
    } 
 
    public void OnPointerClick(PointerEventData eventData)
    {
        // Debug.Log(eventData.clickCount);
 
        // if (tap == 2)
        // {
        //     // do something
        // }
 
    }
}