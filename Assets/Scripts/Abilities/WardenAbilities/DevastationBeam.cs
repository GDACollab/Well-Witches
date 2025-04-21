using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject spellCircle;
    public GameObject laserBeam;
    //public CameraShake cameraShake;
    public GameObject volume;
    public GameObject light2d;

    [Tooltip("The lower the number, the faster the beam reaches the mouse angle.")]
    public float smoothTime = 0.5f;

    private float r;

    HashSet<Collider2D> enemiesInBlast = new HashSet<Collider2D>();

    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        laserBeam.SetActive(false);
    }

    public void Activate(float damagePerTick, float damageTickDuration, float knockbackForce, float knockbackTickDuration, float lifespan)
	{
		this.damagePerTick = damagePerTick;
		this.damageTickDuration = damageTickDuration;
		this.knockbackForce = knockbackForce;
		this.knockbackTickDuration = knockbackTickDuration;

        spellCircle.SetActive(true);
        StartCoroutine(ActivateLaser(lifespan));
	}

    IEnumerator ActivateLaser(float lifespan)
    {
        yield return new WaitForSeconds(2f);
        laserBeam.SetActive(true);
        StartCoroutine(DisableUltimate(lifespan));
    }

    IEnumerator DisableUltimate(float lifespan)
    {
        yield return new WaitForSeconds(lifespan);
        spellCircle.SetActive(false);
        laserBeam.SetActive(false);
		Destroy(gameObject);
    }

    void Update()
	{
		AimAtMouse();
		HandleDamageTick();
		HandleKnockbackTick();
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
		foreach (Collider2D collider in enemiesInBlast) collider.GetComponent<BaseEnemyClass>().TakeDamage(damagePerTick);
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
