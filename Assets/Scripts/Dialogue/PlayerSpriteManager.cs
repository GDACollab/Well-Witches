using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpriteManager : MonoBehaviour
{
    [SerializeField] private GameObject playerSpritesHolder; 
    [SerializeField] private GameObject wardenSpriteHolder;
    [SerializeField] private GameObject gathererSpriteHolder;
    private Image wardenSprite;
    private Sprite wardenOriginalSprite;
    private Image gathererSprite;
    private Sprite gathererOriginalSprite;
    private Animator moveAnimator;

    // Start is called before the first frame update
    private void Start()
    {
        playerSpritesHolder.SetActive(false);
        wardenSprite = wardenSpriteHolder.GetComponent<Image>();
        wardenOriginalSprite = wardenSprite.sprite;
        gathererSprite = gathererSpriteHolder.GetComponent<Image>();
        gathererOriginalSprite = gathererSprite.sprite;
        moveAnimator = playerSpritesHolder.GetComponent<Animator>();
    }

    public void DisplayPlayerSprite()
    {
        playerSpritesHolder.SetActive(true);
        wardenSprite.sprite = wardenOriginalSprite;
        gathererSprite.sprite = gathererOriginalSprite;
    }

    public void HidePlayerSprite()
    {
        SwitchToGatherer();
        playerSpritesHolder.SetActive(false);
    }

    public void ChangeWardenSprite(Sprite newSprite)
    {
        wardenSprite.sprite = newSprite;
    }

    public void ChangeGathererSprite(Sprite newSprite)
    {
        gathererSprite.sprite = newSprite;
    }

    public void SwitchToWarden()
    {
        moveAnimator.SetBool("WardenActive", true);
        wardenSpriteHolder.GetComponent<RectTransform>().SetAsLastSibling();
        gathererSpriteHolder.GetComponent <RectTransform>().SetAsFirstSibling();
    }

    public void SwitchToGatherer()
    {
        moveAnimator.SetBool("WardenActive", false);
        gathererSpriteHolder.GetComponent<RectTransform>().SetAsLastSibling();
        wardenSpriteHolder.GetComponent<RectTransform>().SetAsFirstSibling();
    }
}
