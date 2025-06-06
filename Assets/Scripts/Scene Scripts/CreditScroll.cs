using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;


// Original Script from Malisense, by Ethan Yoshino
// Ported by Ivy Dudzik
public class CreditScroll : MonoBehaviour
{
    public enum SongType {MainMenu, Map1, Map2, Boss};
    
    [Serializable]
    private class CreditSong
    {
       public float duration;
       public SongType song;
    }

    [SerializeField] TextAsset creditsText;
    [Header("Scroll Control")]
    [SerializeField] List<Sprite> imageObjects = new List<Sprite>();
    [SerializeField] List<CreditSong> songs = new List<CreditSong>();
    [SerializeField] float scrollSpeed = 1f;
    [SerializeField] float scrollMultiplier = 2f;
    [SerializeField] int maxLines = 35;
    int numLines = 35;
    float scrollSpeedAdjusted = 1f;
    bool startImage = true;
    List<float> textScrollLanes = new List<float> { -270, 0, 270 };
    List<float> objectScrollLanes = new List<float> { -720, -480, 480, 720 };

    [Header("Fade Control")]
    [SerializeField] float fadeInTime = 2f;
    [SerializeField] float fadeOutTime = 0.5f;
    [SerializeField] float longFadeOutTime = 2f;
    [SerializeField] float hintTime = 2f;

    public GameObject creditsTextTemplate;
    public GameObject creditsImageTemplate;
    public GameObject creditsLargeImageTemplate;
    public Animator hintAnimator;
    public Sprite GDALogo;

    List<RectTransform> creditsDisplay = new List<RectTransform>();
    RectTransform imageDisplay;
    RectTransform lastText;
    RectTransform lastRightText;

    float creditsTextHeight;

    List<string> parsedCredits;
    int currLine = 0;
    int currImage = 0;
    bool creditsEnd = false;
    bool creditsTrueEnd = false;

    int currSong = 0;

    float hintTimer;
    bool skipHint => hintTimer > 0;
    bool skipHint2 = true;

    PlayerInput input;
    Action destroyObjects;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
        input.currentActionMap.actionTriggered += ShowSkipHint;
        input.actions["Submit"].started += FastForward;
        input.actions["Submit"].canceled += FastForward;
        hintTimer = 0;

        scrollSpeedAdjusted = scrollSpeed;

        parsedCredits = creditsText.text.Split("\n").ToList();

        RectTransform temp = Instantiate(creditsTextTemplate, transform).GetComponent<RectTransform>();
        temp.GetComponent<TMP_Text>().text = parsedCredits[currLine];
        creditsDisplay.Add(temp);
        lastText = temp;
        currLine++;
        Canvas.ForceUpdateCanvases();
        creditsTextHeight = creditsTextTemplate.GetComponent<RectTransform>().rect.height;

        for (int i = 1; i < numLines; i++)
        {
            SetText();
            Canvas.ForceUpdateCanvases();
        }

        imageDisplay = Instantiate(creditsImageTemplate, transform).GetComponent<RectTransform>();
        imageDisplay.GetComponent<Image>().sprite = imageObjects[currImage];
        imageDisplay.GetComponent<AspectRatioFitter>().aspectRatio = imageObjects[currImage].rect.width / imageObjects[currImage].rect.height;
        imageDisplay.anchoredPosition = new Vector2((currImage % 2 == 0) ? objectScrollLanes[0] : objectScrollLanes[3], imageDisplay.anchoredPosition.y);
        currImage++;

        // Wait to call song
        if (songs.Count > 0) StartCoroutine(WaitToCall(songs[currSong]));
    }

    // Update is called once per frame
    void Update()
    {
        // Early Exit Hint
        if (hintTimer > 0)
        {
            hintTimer -= Time.deltaTime;
        }
        hintAnimator.SetBool("SkipHint", skipHint);

        foreach (RectTransform disp in creditsDisplay)
        {
            if (disp && disp.anchoredPosition.y > disp.rect.height)
            {
                destroyObjects += () =>
                {
                    creditsDisplay.Remove(disp);
                    Destroy(disp.gameObject);
                };
            }
            disp.transform.Translate(Vector3.up * Time.deltaTime * scrollSpeedAdjusted);
        }

        if (destroyObjects != null)
        {
            destroyObjects();
            destroyObjects = null;
        }

        if (currLine < parsedCredits.Count)
        {
            if (creditsDisplay.Count < numLines)
            {
                if (numLines > creditsDisplay.Count + 1 && numLines > maxLines)
                {
                    numLines--;
                }
                SetText();
            }
            if (imageDisplay == null)
            {
                SetImage();
            }
            if (startImage && Mathf.Abs(creditsDisplay.First().anchoredPosition.y) <= 540)
            {
                startImage = false;
                creditsDisplay.Add(imageDisplay);
            }
        }
        else if (!creditsEnd && currLine >= parsedCredits.Count && Mathf.Abs(lastText.anchoredPosition.y) + lastText.rect.height - 150 <= 540)
        {
            creditsEnd = true;
            var temper = Instantiate(creditsLargeImageTemplate, transform).GetComponent<RectTransform>();

            creditsDisplay.Add(temper);
            temper.GetComponent<Image>().sprite = GDALogo;
        }
        else if (creditsEnd && creditsDisplay.Count <= 1 && !creditsTrueEnd)
        {
            creditsTrueEnd = true;
            SceneHandler.Instance.ToMainMenuScene();
        }
    }

    void SetText()
    {
        string[] tempText = parsedCredits[currLine].Split("|||");
        RectTransform temper;
        lastText = (lastRightText && lastRightText.anchoredPosition.y - lastRightText.rect.height < lastText.anchoredPosition.y - lastText.rect.height) ? lastRightText : lastText;
        if (tempText[0].Trim() != "")
        {
            temper = Instantiate(creditsTextTemplate, transform).GetComponent<RectTransform>();
            creditsDisplay.Add(temper);
            temper.GetComponent<TMP_Text>().text = "<size=50%> </size>";
            temper.anchoredPosition = new Vector2(textScrollLanes[1], lastText.anchoredPosition.y - lastText.rect.height);
            lastText = temper;
            Canvas.ForceUpdateCanvases();
        }
        if (tempText.Length > 1)
        {
            temper = Instantiate(creditsTextTemplate, transform).GetComponent<RectTransform>();
            creditsDisplay.Add(temper);
            temper.sizeDelta /= 1.8f;
            temper.GetComponent<TMP_Text>().text = tempText[0];
            temper.GetComponent<TMP_Text>().alignment = TextAlignmentOptions.CaplineRight;
            temper.anchoredPosition = new Vector2(textScrollLanes[0], lastText.anchoredPosition.y - lastText.rect.height);
            lastText = temper;
            temper = Instantiate(creditsTextTemplate, transform).GetComponent<RectTransform>();
            temper.sizeDelta /= 1.8f;
            creditsDisplay.Add(temper);
            temper.GetComponent<TMP_Text>().text = tempText[1];
            temper.GetComponent<TMP_Text>().alignment = TextAlignmentOptions.CaplineLeft;
            Canvas.ForceUpdateCanvases();
            if (lastText.rect.height > temper.rect.height)
            {
                lastText.GetComponent<TMP_Text>().alignment = TextAlignmentOptions.MidlineRight;
                temper.anchoredPosition = new Vector2(textScrollLanes[2], lastText.anchoredPosition.y - lastText.rect.height / 2 + temper.rect.height / 2);
            }
            else if (lastText.rect.height < temper.rect.height)
            {
                temper.anchoredPosition = new Vector2(textScrollLanes[2], lastText.anchoredPosition.y);
                temper.GetComponent<TMP_Text>().alignment = TextAlignmentOptions.MidlineLeft;
                lastText.anchoredPosition = new Vector2(textScrollLanes[0], temper.anchoredPosition.y - temper.rect.height / 2 + lastText.rect.height / 2);
            }
            else
            {
                temper.anchoredPosition = new Vector2(textScrollLanes[2], lastText.anchoredPosition.y);
                if (lastText.rect.height > creditsTextHeight)
                {
                    lastText.GetComponent<TMP_Text>().alignment = TextAlignmentOptions.MidlineRight;
                    temper.GetComponent<TMP_Text>().alignment = TextAlignmentOptions.MidlineLeft;
                }
            }
            lastRightText = temper;
            numLines++;
        }
        else
        {
            lastRightText = null;
            temper = Instantiate(creditsTextTemplate, transform).GetComponent<RectTransform>();
            creditsDisplay.Add(temper);
            temper.GetComponent<TMP_Text>().text = tempText[0];
            temper.anchoredPosition = new Vector2(textScrollLanes[1], lastText.anchoredPosition.y - lastText.rect.height);
            lastText = temper;
        }
        currLine++;
    }

    void SetImage()
    {
        imageDisplay = Instantiate(creditsImageTemplate, transform).GetComponent<RectTransform>();
        imageDisplay.GetComponent<Image>().sprite = imageObjects[currImage];
        imageDisplay.GetComponent<AspectRatioFitter>().aspectRatio = imageObjects[currImage].rect.width / imageObjects[currImage].rect.height;
        imageDisplay.anchoredPosition = new Vector2((currImage % 2 == 0) ? objectScrollLanes[0] : objectScrollLanes[3], imageDisplay.anchoredPosition.y);
        creditsDisplay.Add(imageDisplay);
        currImage = Mathf.Clamp(currImage + 1, 0, imageObjects.Count - 1);
    }

    IEnumerator WaitToCall(CreditSong song)
    {
        yield return new WaitForSeconds(1f);
        PlaySong(song.song);
        Debug.Log(songs[currSong].duration);
        yield return new WaitForSeconds(song.duration);
        currSong++;
        
        if (currSong < songs.Count)
        {
            StartCoroutine(WaitToCall(songs[currSong]));
        }
    }

    void PlaySong(SongType type)
    {
        AudioManager.Instance.CleanUp();
        switch (type)
        {
            case SongType.MainMenu:
                AudioManager.Instance.PlayOST(FMODEvents.Instance.lobbyBGM);
                break;
            case SongType.Map1:
                AudioManager.Instance.PlayOST(FMODEvents.Instance.mainMapBGM);
                break;
            case SongType.Map2:
                // If someone has time add the second map song to be played here.
                AudioManager.Instance.PlayOST(FMODEvents.Instance.mainMapBGM);
                break;
            case SongType.Boss:
                AudioManager.Instance.PlayOST(FMODEvents.Instance.bossBGM);
                break;
            default:
                AudioManager.Instance.PlayOST(FMODEvents.Instance.mainMapBGM);
                break;
        }
    }

    void OnPause()
    {
        if (skipHint && skipHint2)
        {
            skipHint2 = false;
            SceneHandler.Instance.ToMainMenuScene();
        }
    }
    
    void FastForward(InputAction.CallbackContext ctx)
    {
        if (ctx.started) scrollSpeedAdjusted = scrollSpeed*scrollMultiplier;
        else if (ctx.canceled) scrollSpeedAdjusted = scrollSpeed;
    }

    void ShowSkipHint(InputAction.CallbackContext ctx)
    {
        if (ctx.control.device is Keyboard || ctx.control.device is Gamepad)
        {
            hintTimer = hintTime;
        }
    }

    void ShowSkipHint(InputEventPtr eventPtr, InputDevice device)
    {
        if (device is Keyboard || device is Gamepad)
        {
            hintTimer = hintTime;
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        AudioManager.Instance.CleanUp();
        input.currentActionMap.actionTriggered -= ShowSkipHint;
        input.actions["Submit"].started -= FastForward;
        input.actions["Submit"].canceled -= FastForward;
    }
}
