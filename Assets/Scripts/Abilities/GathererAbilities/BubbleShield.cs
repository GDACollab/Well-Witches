using System.Collections;
using TMPro;
using UnityEngine;

public class BubbleShield : MonoBehaviour
{
    [SerializeField] private GameObject shieldModel;
    private Renderer _renderer;
    public void Activate(float duration)
    {
        if (shieldModel == null) { shieldModel = GetComponentInChildren<GameObject>(); };
        _renderer = shieldModel.GetComponent<Renderer>();
        shieldModel.transform.localScale = Vector3.one;
        StartCoroutine(SpawnShield());
        Destroy(gameObject, duration);
    }

    // so there's the problem that it's constantly colliding with the Gatherer
    // this could also be fixed by making a Shield layer, separating the Warden and Gatherer layers
    // then configuring the layer collision matrix so Shield only collide with Warden and Enemies
    // BUT that messes with other layer checking stuff somewhere else so
    // this really ugly code here works - Jim
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.name)
        {
            case "EnemyFireball(Clone)":
                Rigidbody2D proj = collision.GetComponent<Rigidbody2D>();
                collision.transform.rotation = Quaternion.Euler(collision.transform.rotation.eulerAngles.x, collision.transform.rotation.eulerAngles.y, collision.transform.rotation.eulerAngles.z + 180f);
                proj.velocity = collision.gameObject.transform.up * collision.GetComponent<EnemyProjectile>().speed;
                AudioManager.Instance.PlayOneShot(FMODEvents.Instance.bubbleDeflect, this.transform.position);
                break;
            case "Warden":
                StartCoroutine(PopShield());
                break;
            case "MeleeEnemy(Clone)":
                StartCoroutine(PopShield());
                break;
            case "RangedEnemy(Clone)":
                StartCoroutine(PopShield());
                break;
            case "TankEnemy(Clone)":
                StartCoroutine(PopShield());
                break;
            default:
                break;
        }
        
    }

    IEnumerator SpawnShield()
    {
        _renderer.material.SetFloat("_AlphaClip", 0f);
        float start = 1.2f;
        float end = -0.2f;
        float lerp = 0f;
        while (lerp < 1)
        {
            _renderer.material.SetFloat("_Dissolve", Mathf.Lerp(start, end, lerp));
            lerp += Time.deltaTime * 1.5f;
            yield return null;
        }
    }
    IEnumerator PopShield()
    {
        GathererBubbleShield.Instance.isActive = false;
        float start = 0f;
        float end = 1f;
        float lerp = 0f;
        while (lerp < 1)
        {
            _renderer.material.SetFloat("_AlphaClip", Mathf.Lerp(start, end, lerp));
            shieldModel.transform.localScale = Vector3.one * Mathf.Lerp(1f, 3f, lerp);
            lerp += Time.deltaTime * 3f;
            yield return null;
        }
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.bubbleDeactivate, this.transform.position);
        Destroy(gameObject);
        yield return null;
    }

}
