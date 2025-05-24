using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteManager : MonoBehaviour
{
    [Tooltip("Which NPC this is")] public Character NPC;
    [SerializeField] private GameObject spriteHolder;
    private Image sprite;
    private Sprite originalSprite;

    public enum Character {Wisteria, Parcella, Dullahan, Diver, VampireKnight, Hex, Corvus};

    // Start is called before the first frame update
    private void Start()
    {
        spriteHolder.SetActive(false);
        sprite = spriteHolder.GetComponent<Image>();
        originalSprite = sprite.sprite;
    }

    public void DisplaySprite()
    {
        spriteHolder.SetActive(true);
        sprite.sprite = originalSprite;
    }

    public void HideSprite()
    {
        spriteHolder.SetActive(false);
    }

    public void ChangeSprite(Sprite newSprite)
    {
        sprite.sprite = newSprite;
    }
}
