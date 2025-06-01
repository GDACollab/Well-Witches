using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWell : MonoBehaviour
{
    [SerializeField] private Sprite activeWell;
    [SerializeField] private Sprite inactiveWell;
    public bool canEnter = false;

    private SpriteRenderer sr;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        sr.sprite = inactiveWell; 
    }

    public void EnableBossWell()
    {
        canEnter = true;
        sr.sprite = activeWell;
    }
}
