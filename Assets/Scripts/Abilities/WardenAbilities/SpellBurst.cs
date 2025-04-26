using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellBurst : MonoBehaviour
{
    [SerializeField] private SpellBurstProjectile projectilePrefab;
    [SerializeField] private EdgeCollider2D edgeCollider;

    private Camera cam;
    private GameObject cameraHolder;

    public void Activate(float damage, float speed, float lifetime, float timeBetweenProjectile, float abilityDuration)
    {
        cameraHolder = GameObject.Find("Camera Holder");  // committing some sins with this but oh well
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        edgeCollider = GetComponent<EdgeCollider2D>();
        GetScreenEdges();
        StartCoroutine(SpawnSpectralProjectile(damage, speed, lifetime, timeBetweenProjectile));
        Destroy(gameObject, abilityDuration);
    }

    IEnumerator SpawnSpectralProjectile(float damage, float speed, float lifetime, float timeBetweenProjectile)
    {
        while (true)
        {
            SpellBurstProjectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<SpellBurstProjectile>();
            projectile.Initialize(damage, speed, lifetime);
            yield return new WaitForSeconds(timeBetweenProjectile);
        }
    }

    // Credits to
    // https://www.youtube.com/watch?v=sZp8746MR1Y
    // for the screen collision stuff
    private void GetScreenEdges()
    {
        Vector2 offset = new Vector2(cameraHolder.transform.position.x, cameraHolder.transform.position.y);
        List <Vector2> edges = new List<Vector2>();
        edges.Add(cam.ScreenToWorldPoint(Vector2.zero - offset)); // top left
        edges.Add(cam.ScreenToWorldPoint(new Vector2(Screen.width, 0) - offset)); // top right
        edges.Add(cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height) - offset)); // bottom right
        edges.Add(cam.ScreenToWorldPoint(new Vector2(0, Screen.height) - offset)); // bottom left
        edges.Add(cam.ScreenToWorldPoint(Vector2.zero - offset)); // top left
        edgeCollider.SetPoints(edges);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider != null && collider.gameObject.name == "SpellBurstProjectile")
        {
            Rigidbody2D colliderRB = collider.GetComponent<Rigidbody2D>();
            RaycastHit2D[] hit2D = Physics2D.RaycastAll(collider.transform.position, colliderRB.velocity);
            Vector2 contactPoint = hit2D[1].point;
            Vector2 normal = Vector2.Perpendicular(contactPoint - GetClosestPoint(collider.transform.position)).normalized;
            colliderRB.velocity = Vector2.Reflect(colliderRB.velocity, normal);
        }
    }
    Vector2 GetClosestPoint(Vector2 position)
    {
        Vector2[] points = edgeCollider.points;
        float shortestDistance = Vector2.Distance(position, points[0]);
        Vector2 closestPoint = points[0];
        foreach (Vector2 point in points)
        {
            if (Vector2.Distance(position, point) < shortestDistance)
            {
                shortestDistance = Vector2.Distance(position, point);
                closestPoint = point;
            }
        }
        return closestPoint;
    }
}
