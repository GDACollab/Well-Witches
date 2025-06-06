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
    [SerializeField]
    private Animation TransitionAnimation;
    [SerializeField]
    private float AnnouncementTime = 1;
    
    private Queue<String> AnnouncementQueue = new Queue<String>();
    private bool playingAnnouncement = false;

    private void Awake(){
        if (Instance != null)
        {
            Debug.LogError("Found more than one GameManager in the scene. Please make sure there is only one");
        }
        Instance = this;
    }

    void Start()
    {
        AnnouncementTextBox.text = "";
        AnnouncementTextBox.alpha = 0;
        Backdrop.gameObject.SetActive(false);
    }
    
    void Update()
    {
        if (!playingAnnouncement && !TransitionAnimation.isPlaying && Backdrop.gameObject.activeSelf)
        {
            AnnouncementTextBox.text = "";
            AnnouncementTextBox.alpha = 0;
            Backdrop.gameObject.SetActive(false);
        }

        // FOR TESTING PURPOSES ONLY, DO NOT UNCOMMENT
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    AddAnnouncementToQueue(Time.deltaTime.ToString());
        //}
    }

    IEnumerator TimerRoutine ()
    {
        AnnouncementTextBox.text = AnnouncementQueue.Peek();
        AnnouncementTextBox.alpha = 255;
        Backdrop.gameObject.SetActive(true);
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.ravenAnnounce, GameObject.Find("Gatherer").transform.position);
        if (!playingAnnouncement)
        {
            TransitionAnimation.Play("announcementTransition");
            playingAnnouncement = true;
        }
        WaitForSeconds delay = new WaitForSeconds(AnnouncementTime);
        yield return delay;
        AnnouncementQueue.Dequeue();
        if(AnnouncementQueue.Count > 0){
            AnnouncementTextBox.text = AnnouncementQueue.Peek();
            StartCoroutine(TimerRoutine());
        }
        else{
            TransitionAnimation.Play("announcementTransitionReversed");
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
