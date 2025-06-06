using UnityEngine;

public class BossWell : MonoBehaviour
{
    public static BossWell instance;

    [SerializeField] private SpriteRenderer sr;

    public Sprite inactiveWell;
    public Sprite activeWell;

    public bool canEnter = false;

    private void Awake()
    {
        if (instance != null) Debug.LogWarning("Found more than one BossWell in the scene. Please make sure there is only one");
        else instance = this;

        if (sr == null) { sr = GetComponentInChildren<SpriteRenderer>(); }
    }

    private void Start()
    {
        canEnter = false;
        sr.sprite = inactiveWell;

        if (GameManager.instance.currentKeyItem == 9)
        {
            ActivateWell();
        }
    }

    public void ActivateWell()
    {
        canEnter = true;
        sr.sprite = activeWell;
    }
}
