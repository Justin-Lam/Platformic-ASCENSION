using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateCrazySword : MonoBehaviour
{
	[SerializeField] BossAI ai;

	[SerializeField] GameObject spritesGO;
	[SerializeField] PolygonCollider2D colliderComponent;
	[SerializeField] float hideDuration;
	float hideTimer;

	[SerializeField] Transform playerTransform;

	[SerializeField] GameObject swordTrailGO;
	[SerializeField] BossSwordTrail swordTrailScript;
	[SerializeField] float swordTrailArmTime;
	float armTimer;
	[SerializeField] float howLongStayArmed;
	float howLongStayArmedTimer;
	bool armed;


	void OnEnable()
	{
		// Hide and disable colllider
		spritesGO.SetActive(false);
		colliderComponent.enabled = false;

		// Initialize hideTimer
		hideTimer = hideDuration;

		// Hide sword trail
		swordTrailGO.SetActive(false);

		armed = false;
	}

	void Update()
	{
		if (hideTimer <= 0f)
		{
			if (spritesGO.activeSelf == false)
			{
				// Blink to a random location around the player
				Vector2 randomDirection = Random.insideUnitCircle.normalized;
				float randomDistance = Random.Range(2f, 7f);
				Vector2 randomPosition = (Vector2)playerTransform.position + randomDirection * randomDistance;
				transform.position = randomPosition;

				// Show and enable collider
				spritesGO.SetActive(true);
				colliderComponent.enabled = true;

				// Show sword trail and bring to boss's position
				swordTrailGO.SetActive(true);
				swordTrailGO.transform.position = transform.position;

				// Disarm sword trail and initialize armTimer
				swordTrailScript.Disarm();
				armTimer = swordTrailArmTime;
			}

			if (armTimer <= 0f && !armed)
			{
				// Arm sword trail
				swordTrailScript.Arm();
				howLongStayArmedTimer = howLongStayArmed;
				armed = true;
			}

			if (armed)
			{
				if(howLongStayArmedTimer <= 0f)
				{
					swordTrailGO.SetActive(false);
					ai.ChooseRandomAttack();
				}

				howLongStayArmedTimer -= Time.deltaTime;
			}

			// Decrement armTimer
			armTimer -= Time.deltaTime;
		}

		// Decrement hideTimer
		hideTimer -= Time.deltaTime;
	}
}
