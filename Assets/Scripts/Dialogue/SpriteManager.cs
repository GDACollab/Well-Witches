using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] GameObject character; // the character whose sprites are being managed
    [SerializeField] List<GameObject> sprites;
    public enum Sprites {NEUTRAL, HAPPY, SAD};

    // Start is called before the first frame update
    private void Start()
    {
        foreach(GameObject sprite in sprites)
        {
            sprite.SetActive(false);
        }
    }

    public void DisplaySprite()
    {
        sprites[0].SetActive(true);
    }

    public void HideSprite()
    {
        sprites[0].SetActive(false);
    }

    public void ChangeSprite(string newSprite)
    {
        foreach(GameObject sprite in sprites)
        {
            sprite.SetActive(false);
        }
        switch (newSprite) 
        {
            case "neutral":
                sprites[(int)Sprites.NEUTRAL].SetActive(true);
                break;
            case "happy":
                sprites[(int)Sprites.HAPPY].SetActive(true);
                break;
            case "sad":
                sprites[(int)Sprites.SAD].SetActive(true);
                break;
            default:
                Debug.LogWarning("Sprite not found.");
                sprites[(int)Sprites.NEUTRAL].SetActive(true);
                break;
        }
    }
}
