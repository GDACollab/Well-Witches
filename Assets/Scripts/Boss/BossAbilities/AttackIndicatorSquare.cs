using UnityEngine;

[ExecuteAlways]
public class AttackIndicatorSquare : MonoBehaviour
{
    public Transform growEffect;
    public Collider2D collider;
    [Range(0, 1)] public float size;



    private void Update()
    {
        growEffect.localScale = new Vector3(size, size, 0);
    }
}