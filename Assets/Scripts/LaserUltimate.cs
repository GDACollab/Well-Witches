using System.Collections;
using UnityEngine;

public class LaserUltimate: MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePosition;

    public GameObject spellCircle;
    public GameObject laserBeam;

    public CameraShake cameraShake;
    public GameObject volume;
    public GameObject light2d;

    [Tooltip("The lower the number, the faster the beam reaches the mouse angle.")]
    public float smoothTime = 0.5f;
    
    private float r;
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        spellCircle.SetActive(false);
        laserBeam.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            spellCircle.SetActive(true);
            StartCoroutine(ActivateLaser());
            StartCoroutine(cameraShake.Shake(.15f, .4f, 2f, volume, light2d));
        }

        mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePosition - transform.position;
        float targetRotation = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        // smoothly rotates it towards mouse position
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.z, targetRotation, ref r, smoothTime);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    IEnumerator ActivateLaser()
    {
        yield return new WaitForSeconds(2f);
        laserBeam.SetActive(true);
        StartCoroutine(DisableUltimate());
    }

    IEnumerator DisableUltimate()
    {
        yield return new WaitForSeconds(5f);
        spellCircle.SetActive(false);
        laserBeam.SetActive(false);
    }
}
