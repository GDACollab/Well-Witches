using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayButtonHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject outline;
    Image outlineImage;
    public float fadeSpeed = 5; //speed of fade effect; should take about 1/fadeSpeed seconds to fully fade in/out

    void Start()
    {
        outlineImage = outline.GetComponent<Image>();
        outlineImage.color = new Color(outlineImage.color.r, outlineImage.color.g, outlineImage.color.b, 0); //set color to be invisible
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //TO DO later
        // Add a coroutine or something to gradually scale the highlight outline.
        //Done! coroutine has been implemented :)
        StopAllCoroutines();
        StartCoroutine(fadeIn());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(fadeOut());
    }

    //Function for instantly stopping the highlight
    //used for situations that somehow prevent the other functions/coroutines from running *cough* settings button *cough*
    public void quickStop()
    {
        StopAllCoroutines();
        outlineImage.color = new Color(outlineImage.color.r, outlineImage.color.g, outlineImage.color.b, 0);
        outline.SetActive(false);
    }

    IEnumerator fadeIn()
    {
        outline.SetActive(true); //Set visible...
        while (true)
        {
            //increase by a bit...
            outlineImage.color = new Color(outlineImage.color.r, outlineImage.color.g, outlineImage.color.b, outlineImage.color.a + fadeSpeed * Time.deltaTime);
            //Debug.Log("FI:" + outlineImage.color.a);
            if (outlineImage.color.a >= 1)
            {
                break; //When it hits the max, you're done!
            }
            yield return 0; //wait a frame to repeat...
        }
    }

    IEnumerator fadeOut()
    {
        while (true)
        {
            //increase by a bit...
            outlineImage.color = new Color(outlineImage.color.r, outlineImage.color.g, outlineImage.color.b, outlineImage.color.a - fadeSpeed * Time.deltaTime);
            //Debug.Log("FO:" + outlineImage.color.a);
            if (outlineImage.color.a <= 0)
            {
                break; //When it hits the min, you're done!
            }
            yield return 0; //wait a frame to repeat...
        }
        outline.SetActive(false); //Make the object non-active until it's done
    }
}
