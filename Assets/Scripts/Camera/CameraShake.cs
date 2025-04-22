using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude, float delay, GameObject volume, GameObject light2d)
    {
        yield return new WaitForSeconds (delay);
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;
        volume.SetActive(true);
        light2d.SetActive(true);
        while (elapsed < duration) 
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        volume.SetActive(false);
        light2d.SetActive(false);
        transform.localPosition = originalPos;
    }
}
