using System.Collections;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.AI;

public class BossProjectile : MonoBehaviour
{
    [Header("Stats")]
    public float projectileSpawnDistance;
    public float timeToHit;

    private Vector2 originalPosition;
    [Header("References")]
    [SerializeField] private Transform bossProjectile;
    [SerializeField] private GameObject bossProjectileImpact;
    [SerializeField] private AttackIndicatorCircle indicator;

    private void Start()
    {
        indicator.size = 0f;
        bossProjectile.gameObject.SetActive(true);
        bossProjectile.position = new Vector2(transform.position.x, transform.position.y + projectileSpawnDistance);
        originalPosition = bossProjectile.position;
        bossProjectileImpact.SetActive(false);
        StartCoroutine(Activate());
    }

    public IEnumerator Activate()
    {
        float timeElapsed = 0f;

        while (timeElapsed < timeToHit)
        {
            float t = timeElapsed / timeToHit;
            bossProjectile.position = Vector2.Lerp(originalPosition, transform.position, t);
            indicator.size = Mathf.Lerp(0, 1, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        bossProjectile.localPosition = Vector2.zero;
        bossProjectile.gameObject.SetActive(false);
        indicator.enabled = false;
        bossProjectileImpact.SetActive(true);
        Destroy(gameObject, 0.5f);
    }
}
