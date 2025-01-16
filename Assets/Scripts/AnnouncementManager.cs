using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using UnityEngine.UI;
public class AnnouncementManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI AnnouncementTextBox;
    [SerializeField]
    private Image Backdrop;
    private Queue<String> AnnouncementQueue = new Queue<String>();
    void Start()
    {
        AnnouncementTextBox.text = "";
        AnnouncementTextBox.alpha = 0;
        Backdrop.gameObject.SetActive(false);
    }

    // FOR TESTING PURPOSES ONLY
    /*
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            AddAnnouncementToQueue("hi");
        }
    }
    */

    IEnumerator TimerRoutine ()
    {
        WaitForSeconds delay = new WaitForSeconds(1);
        yield return delay;
        AnnouncementQueue.Dequeue();
        AnnouncementTextBox.alpha = 0;
        Backdrop.gameObject.SetActive(false);
        AnnouncementTextBox.text = "";
    }

    public void AddAnnouncementToQueue(String announcement){
        AnnouncementQueue.Enqueue(announcement);
        AnnouncementTextBox.alpha = 255;
        Backdrop.gameObject.SetActive(true);
        AnnouncementTextBox.text = AnnouncementQueue.Peek();
        StartCoroutine(TimerRoutine());
    }
}
