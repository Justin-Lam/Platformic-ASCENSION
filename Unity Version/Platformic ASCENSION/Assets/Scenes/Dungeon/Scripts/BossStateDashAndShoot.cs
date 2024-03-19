using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateDashAndShoot : MonoBehaviour
{
	[SerializeField] BossAI ai;

	[SerializeField] PolygonCollider2D colliderComponent;

	[SerializeField] Transform playerTransform;
	[SerializeField] float minBlinkDistance;
	[SerializeField] float maxBlinkDistance;

	[SerializeField] GameObject projectile;
	[SerializeField] int projectileDamage;
	[SerializeField] int dashAndShootAmount;
	int dashAndShootCounter = 0;
	[SerializeField] float waitTime;
	float waitTimer = 0f;


	void OnEnable()
	{
		Debug.Log("Shooting");
		// Disable collider
		colliderComponent.enabled = false;
	}

	void Update()
	{
		if (dashAndShootCounter < dashAndShootAmount)
		{
			if (waitTimer >= waitTime)
			{
				// Blink to a random location that's within a 5-8 unit radius from the player
				Vector2 randomDirection = Random.insideUnitCircle.normalized;
				float randomDistance = Random.Range(minBlinkDistance, maxBlinkDistance);
				Vector2 randomPosition = (Vector2)playerTransform.position + randomDirection * randomDistance;
				transform.position = randomPosition;

				// Face the player
				Vector2 playerDirection = playerTransform.position - transform.position;
				float angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

				// Shoot a projectile and increment dashAndShootCounter
				TriangleEnemyProjectile projectileScript = Instantiate(projectile, transform.position, transform.rotation).GetComponent<TriangleEnemyProjectile>();
				projectileScript.SetDamage(projectileDamage);
				dashAndShootCounter++;

				// Rest waitTimer
				waitTimer = 0f;
			}
		}
		else
		{
			colliderComponent.enabled = true;
			transform.rotation = Quaternion.identity;
			ai.ChooseRandomAttack();
		}

		// Increment waitTimer
		waitTimer += Time.deltaTime;
	}
}
