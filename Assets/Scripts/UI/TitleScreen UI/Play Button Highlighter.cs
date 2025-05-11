using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayButtonHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject outline;
    public void OnPointerEnter(PointerEventData eventData)
    {
        //TO DO later
        // Add a coroutine or something to gradually scale the highlight outline.
        outline.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        outline.SetActive(false);
    }
}
