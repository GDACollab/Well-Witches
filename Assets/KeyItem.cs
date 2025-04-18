using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class KeyItem : MonoBehaviour
{
    public int keyItemID;
    public SpriteRenderer spriteRenderer;
    private Sprite keyItemSprite; 
    
    public void setSprite(int ID) {
        if (ID >= 1 && ID <= 9) {
            keyItemSprite = Resources.Load<Sprite>($"KeyItems/KeyItemSprite {ID}");
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = keyItemSprite;
        }
    }

}
