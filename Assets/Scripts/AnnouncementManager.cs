using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using UnityEngine.UI;
public class AnnouncementManager : MonoBehaviour
{
    public static AnnouncementManager Instance {get; private set;}
    [SerializeField]
    private TextMeshProUGUI AnnouncementTextBox;
    [SerializeField]
    private Image Backdrop;
    private Queue<String> AnnouncementQueue = new Queue<String>();
    private bool playingAnnouncement = false;

    private void Awake(){
        if(Instance != null && Instance != this){
            Destroy(this);
        }
        else{
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        AnnouncementTextBox.text = "";
        AnnouncementTextBox.alpha = 0;
        Backdrop.gameObject.SetActive(false);
    }

    // FOR TESTING PURPOSES ONLY, DO NOT UNCOMMENT
    /*
    void Update()
    {
        Debug.Log(AnnouncementQueue.Count);
        if(Input.GetKeyDown(KeyCode.Space)){
            AddAnnouncementToQueue(Time.deltaTime.ToString());
        }
    }
    */
    

    IEnumerator TimerRoutine ()
    {
        AnnouncementTextBox.text = AnnouncementQueue.Peek();
        AnnouncementTextBox.alpha = 255;
        Backdrop.gameObject.SetActive(true);
        playingAnnouncement = true;
        WaitForSeconds delay = new WaitForSeconds(1);
        yield return delay;
        AnnouncementQueue.Dequeue();
        if(AnnouncementQueue.Count > 0){
            AnnouncementTextBox.text = AnnouncementQueue.Peek();
            StartCoroutine(TimerRoutine());
        }
        else{
            AnnouncementTextBox.alpha = 0;
            Backdrop.gameObject.SetActive(false);
            AnnouncementTextBox.text = "";
            playingAnnouncement = false;
        }
    }

    public void AddAnnouncementToQueue(String announcement){
        AnnouncementQueue.Enqueue(announcement);
        if(!playingAnnouncement){
            StartCoroutine(TimerRoutine());
        }
    }
}
