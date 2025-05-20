using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DevastationBeam : MonoBehaviour
{
	float damagePerTick;
	float damageTickDuration;
	float damageTickCounter = 0;

	float knockbackForce;
	float knockbackTickDuration;
	float knockbackTickCounter = 0;

    private Camera mainCam;
    private Vector3 mousePosition;

	[SerializeField] private float shakeAmplitdue;
	[SerializeField] private float shakeFrequency;
    [SerializeField] private GameObject spellCircle;
    [SerializeField] private GameObject laserBeam;
    [SerializeField] private GameObject volume;
    [SerializeField] private GameObject impactLight;
    [SerializeField] private Light2D light2d;

    private CinemachineVirtualCamera cinemachineCam;
	private CinemachineBasicMultiChannelPerlin shake;
	private float lifespan;
	private float shakeTimer;

    [Tooltip("The lower the number, the faster the beam reaches the mouse angle.")]
    public float smoothTime = 0.5f;

    private float r;

    HashSet<Collider2D> enemiesInBlast = new HashSet<Collider2D>();

    public void Activate(float damagePerTick, float damageTickDuration, float knockbackForce, float knockbackTickDuration, float lifespan)
	{
		this.lifespan = lifespan;
		shakeTimer = lifespan;
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
		cinemachineCam = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
        shake = cinemachineCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        Vector3 rotation = mousePosition - transform.position;
        float targetRotation = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, targetRotation);

        this.damagePerTick = damagePerTick;
		this.damageTickDuration = damageTickDuration;
		this.knockbackForce = knockbackForce;
		this.knockbackTickDuration = knockbackTickDuration;

        spellCircle.SetActive(true);
		laserBeam.SetActive(false);
        light2d.enabled = false;
        StartCoroutine(ActivateLaser());
	}

    IEnumerator ActivateLaser()
    {
        yield return new WaitForSeconds(1f);

        //camera shake
        shake.m_AmplitudeGain = shakeAmplitdue;
        shake.m_FrequencyGain = shakeFrequency;

        laserBeam.SetActive(true);
        volume.SetActive(true);
        impactLight.SetActive(true);
        light2d.enabled = true;
        StartCoroutine(DisableUltimate());
    }

    IEnumerator DisableUltimate()
    {
        yield return new WaitForSeconds(0.15f);
		volume.SetActive(false);
        impactLight.SetActive(false);

        yield return new WaitForSeconds(lifespan-0.15f);

		// disable camera shake
        shake.m_AmplitudeGain = 0f;
		shake.m_FrequencyGain = 0f;


        spellCircle.SetActive(false);
        laserBeam.SetActive(false);
        light2d.enabled = false;
        Destroy(gameObject);
    }

    void Update()
	{
		AimAtMouse();

		if (shakeTimer > 0 && shake.m_AmplitudeGain > 0)
		{
			shakeTimer -= Time.deltaTime;
			shake.m_AmplitudeGain = Mathf.Lerp(shakeAmplitdue, 0f, 1 - (shakeTimer / lifespan));
            shake.m_FrequencyGain = Mathf.Lerp(shakeFrequency, 0f, 1 - (shakeTimer / lifespan));
        }

        if (laserBeam.gameObject.activeSelf)
		{
            HandleDamageTick();
            HandleKnockbackTick();
        }
	}

	void AimAtMouse()
	{
        mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePosition - transform.position;
        float targetRotation = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        // smoothly rotates it towards mouse position
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.z, targetRotation, ref r, smoothTime);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

	void HandleDamageTick()
	{
		if (damageTickCounter > 0) damageTickCounter -= Time.deltaTime;
		if (damageTickCounter <= 0)
		{
			DamageEnemies();
			damageTickCounter = damageTickDuration;
		}
	}
	void DamageEnemies()
	{
		foreach (Collider2D collider in enemiesInBlast.ToList()) collider.GetComponent<BaseEnemyClass>().TakeDamage(damagePerTick);
	}

	void HandleKnockbackTick()
	{
		if (knockbackTickCounter > 0) knockbackTickCounter -= Time.deltaTime;
		if (knockbackTickCounter <= 0)
		{
			KnockbackEnemies();
			knockbackTickCounter = knockbackTickDuration;
		}
	}
	void KnockbackEnemies()
	{
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 mouseDirection = (mousePosition - transform.position).normalized;
		foreach (Collider2D collider in enemiesInBlast) collider.GetComponent<Rigidbody2D>().AddForce(mouseDirection * knockbackForce, ForceMode2D.Impulse);
	}

	void OnTriggerEnter2D(Collider2D collider) { if (collider.CompareTag("Enemy")) enemiesInBlast.Add(collider); }
	void OnTriggerExit2D(Collider2D collider) { enemiesInBlast.Remove(collider); } // only removes if item in set
}
