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
    private float AnnouncementTime = 1;
    [SerializeField]
    private float AnimationTransitionSpeed = 100;
    private Queue<String> AnnouncementQueue = new Queue<String>();
    private bool playingAnnouncement = false;

    private RectTransform panelTransform;
    private float panelOriginalY;
    private float panelGoalY;

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

        panelTransform = Backdrop.GetComponent<RectTransform>();
        panelGoalY = panelOriginalY = panelTransform.position.y;

        // FOR TESTING:
        AddAnnouncementToQueue("this is an announcement");
        AddAnnouncementToQueue("another one");
        AddAnnouncementToQueue("announcing for the third time");
    }

    
    void Update()
    {
        // FOR TESTING PURPOSES ONLY, DO NOT UNCOMMENT
        if(Input.GetKeyDown(KeyCode.Space)){
            AddAnnouncementToQueue(Time.deltaTime.ToString());
        }

        if (panelTransform.position.y < panelGoalY)
        {
            panelTransform.position = new Vector3(panelTransform.position.x,
                                                  panelTransform.position.y + AnimationTransitionSpeed * Time.deltaTime,
                                                  panelTransform.position.z);
            if (panelTransform.position.y > panelGoalY)
            {
                // if we overshoot it, set it exactly
                panelTransform.position = new Vector3(panelTransform.position.x,
                                                      panelTransform.position.y,
                                                      panelTransform.position.z);
            }
        }
        else if (panelTransform.position.y > panelGoalY)
        {
            panelTransform.position = new Vector3(panelTransform.position.x,
                                                  panelTransform.position.y - AnimationTransitionSpeed * Time.deltaTime,
                                                  panelTransform.position.z);
            if (panelTransform.position.y < panelGoalY)
            {
                // if we overshoot it, set it exactly
                panelTransform.position = new Vector3(panelTransform.position.x,
                                                      panelTransform.position.y,
                                                      panelTransform.position.z);
            }
        }
    }
    
    

    IEnumerator TimerRoutine ()
    {
        AnnouncementTextBox.text = AnnouncementQueue.Peek();
        AnnouncementTextBox.alpha = 255;
        Backdrop.gameObject.SetActive(true);
        playingAnnouncement = true;
        panelGoalY = panelOriginalY - panelTransform.sizeDelta.y;
        WaitForSeconds delay = new WaitForSeconds(AnnouncementTime);
        yield return delay;
        AnnouncementQueue.Dequeue();
        if(AnnouncementQueue.Count > 0){
            AnnouncementTextBox.text = AnnouncementQueue.Peek();
            StartCoroutine(TimerRoutine());
        }
        else{
            panelGoalY = panelOriginalY;
            playingAnnouncement = false;
            //AnnouncementTextBox.alpha = 0;
            //Backdrop.gameObject.SetActive(false);
            //AnnouncementTextBox.text = "";
        }
    }

    public void AddAnnouncementToQueue(String announcement){
        AnnouncementQueue.Enqueue(announcement);
        if(!playingAnnouncement){
            StartCoroutine(TimerRoutine());
        }
    }
}
