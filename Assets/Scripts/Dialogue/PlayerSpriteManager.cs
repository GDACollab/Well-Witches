using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSpriteManager : MonoBehaviour
{
    [SerializeField] private GameObject playerSpritesHolder; 
    [SerializeField] private GameObject wardenSpriteHolder;
    [SerializeField] private GameObject gathererSpriteHolder;
    [SerializeField] private List<GameObject> wardenSprites;
    [SerializeField] private List<GameObject> gathererSprites;
    private Vector2 activePosition = new Vector2(250, 0);
    private Vector2 inactivePosition = new Vector2(300, -10);
    public enum PlayerSprites {NEUTRAL, HAPPY, SAD};

    // Start is called before the first frame update
    private void Start()
    {
        foreach(GameObject sprite in wardenSprites)
        {
            sprite.SetActive(false);
        }
        foreach(GameObject sprite in gathererSprites)
        {
            sprite.SetActive(false);
        }
        playerSpritesHolder.SetActive(false);
    }

    public void DisplayPlayerSprite()
    {
        playerSpritesHolder.SetActive(true);
        wardenSprites[0].SetActive(true);
        gathererSprites[0].SetActive(true);
    }

    public void HidePlayerSprite()
    {
        playerSpritesHolder.SetActive(false);
    }

    public void ChangeWardenSprite(string newSprite)
    {
        foreach(GameObject sprite in wardenSprites)
        {
            sprite.SetActive(false);
        }
        switch (newSprite) 
        {
            case "neutral":
                wardenSprites[(int)PlayerSprites.NEUTRAL].SetActive(true);
                break;
            case "happy":
                wardenSprites[(int)PlayerSprites.HAPPY].SetActive(true);
                break;
            case "sad":
                wardenSprites[(int)PlayerSprites.SAD].SetActive(true);
                break;
            default:
                Debug.LogWarning("Sprite not found.");
                wardenSprites[(int)PlayerSprites.NEUTRAL].SetActive(true);
                break;
        }
    }

    public void ChangeGathererSprite(string newSprite) 
    {
        foreach(GameObject sprite in gathererSprites)
        {
            sprite.SetActive(false);
        }
        switch (newSprite) 
        {
            case "neutral":
                gathererSprites[(int)PlayerSprites.NEUTRAL].SetActive(true);
                break;
            case "happy":
                gathererSprites[(int)PlayerSprites.HAPPY].SetActive(true);
                break;
            case "sad":
                gathererSprites[(int)PlayerSprites.SAD].SetActive(true);
                break;
            default:
                Debug.LogWarning("Sprite not found.");
                gathererSprites[(int)PlayerSprites.NEUTRAL].SetActive(true);
                break;
        }
    }

    public void SwitchToWarden()
    {
        wardenSpriteHolder.GetComponent<RectTransform>().anchoredPosition = activePosition;
        gathererSpriteHolder.GetComponent<RectTransform>().anchoredPosition = inactivePosition;
        wardenSpriteHolder.GetComponent<RectTransform>().SetAsLastSibling();
    }

    public void SwitchToGatherer()
    {
        gathererSpriteHolder.GetComponent<RectTransform>().anchoredPosition = activePosition;
        wardenSpriteHolder.GetComponent<RectTransform>().anchoredPosition = inactivePosition;
        gathererSpriteHolder.GetComponent<RectTransform>().SetAsLastSibling();
    }
}
