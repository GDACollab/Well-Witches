using System.Collections;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.AI;

public class BossProjectile : MonoBehaviour
{
    [Header("Stats")]
    public float projectileSpawnDistance;
    public float indicatorTime;
    public float projectileTime;
    public float damage;

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

        while (timeElapsed < indicatorTime)
        {
            float t = timeElapsed / indicatorTime;
            indicator.size = Mathf.Lerp(0, 1, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        timeElapsed = 0f;

        while (timeElapsed < projectileTime)
        {
            float t = timeElapsed / projectileTime;
            bossProjectile.position = Vector2.Lerp(originalPosition, transform.position, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        bossProjectile.localPosition = Vector2.zero;
        bossProjectile.gameObject.SetActive(false);
        indicator.gameObject.SetActive(false);
        bossProjectileImpact.SetActive(true);
        Destroy(gameObject, 0.8f);
    }
}
