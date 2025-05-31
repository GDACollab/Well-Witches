using UnityEngine;

[ExecuteAlways]
public class AttackLungeIndicator : MonoBehaviour
{
    public Transform growEffect;
    public Collider2D collider;
    [Range(0, 2.5f)] public float size;



    private void Update()
    {
        growEffect.localScale = new Vector3(growEffect.localScale.x, size, 0);
    }
}