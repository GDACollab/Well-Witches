using System.Collections;
using UnityEngine;

public class BossShield : MonoBehaviour
{
    [SerializeField] private GameObject shieldModel;
    private Renderer _renderer;
    public bool shieldActive = true;

    private void Awake()
    {
        if (shieldModel == null) { shieldModel = GetComponent<GameObject>(); };
        _renderer = shieldModel.GetComponent<Renderer>();
    }

    public IEnumerator SpawnShield()
    {
        shieldActive = true;
        shieldModel.transform.localScale = Vector3.one * 1.56f;
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

    public IEnumerator PopShield()
    {
        shieldActive = true;
        GathererBubbleShield.Instance.isActive = false;
        float start = 0f;
        float end = 1f;
        float lerp = 0f;
        while (lerp < 1)
        {
            _renderer.material.SetFloat("_AlphaClip", Mathf.Lerp(start, end, lerp));
            shieldModel.transform.localScale = Vector3.one * Mathf.Lerp(1.56f, 3f, lerp);
            lerp += Time.deltaTime * 3f;
            yield return null;
        }
        //AudioManager.Instance.PlayOneShot(FMODEvents.Instance.bubbleDeactivate, this.transform.position);
        yield return null;
    }
}
