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

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void DisplaySprite()
    {
        sprites[0].SetActive(true);
    }

    public void HideSprite()
    {
        sprites[0].SetActive(false);
    }
}
