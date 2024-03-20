using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectilePortal : MonoBehaviour
{
	[Header("Projectile")]
	[SerializeField] Transform playerTransform;
	[SerializeField] GameObject projectile;
	[SerializeField] int projectileDamage;
	[SerializeField][Tooltip("Shoot this many projectiles per second")] float fireRate;
	float shootTime;
	float shootTimer;

	[Header("Rotation")]
	[SerializeField] float minRotationDegreesPerSecond;
	[SerializeField] float maxRotationDegreesPerSecond;
	float xRotationAmount;
	float YRotationAmount;

	float xRotationDirection;
	float yRotationDirection;

	void OnEnable()
	{
		// Initialize shootTime and shootTimer
		shootTime = 1f / fireRate;
		shootTimer = 0f;

		// Choose random rotation amounts
		xRotationAmount = Random.Range(minRotationDegreesPerSecond, maxRotationDegreesPerSecond);
		YRotationAmount = Random.Range(minRotationDegreesPerSecond, maxRotationDegreesPerSecond);

		// Choose rotation directions
		xRotationDirection = Random.Range(0, 2) * 2 - 1;
		yRotationDirection = Random.Range(0, 2) * 2 - 1;
	}

	void Update()
    {
		// Rotate portal
		transform.Rotate(xRotationDirection * xRotationAmount * Time.deltaTime, 0f, 0f);
		transform.Rotate(0f,yRotationDirection * YRotationAmount * Time.deltaTime, 0f);

		// Check if it's time to shoot
		if (shootTimer >= shootTime)
		{
			// Face the player
			Vector2 playerDirection = playerTransform.position - transform.position;
			float angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			
			// Shoot
			TriangleEnemyProjectile projectileScript = Instantiate(projectile, transform.position, transform.rotation).GetComponent<TriangleEnemyProjectile>();
			projectileScript.SetDamage(projectileDamage);

			// Rest rotation and shootTimer
			transform.rotation = Quaternion.identity;
			shootTimer = 0f;
		}

		// Increment shootTimer
		shootTimer += Time.deltaTime;
	}
}
