using UnityEngine;

[ExecuteAlways]
public class AttackIndicatorCircle : MonoBehaviour
{
    public Transform growEffect;
    [Range(0, 1)] public float size;



    private void Update()
    {
        growEffect.localScale = new Vector3(size, size, 0);
    }
}