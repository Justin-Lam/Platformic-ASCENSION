using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BossStateCharge : MonoBehaviour
{
	[SerializeField] BossAI ai;

	[SerializeField] Transform playerTransform;
	[SerializeField] float chargeTime;
	float chargeTimer;
	[SerializeField] float rotateDegreesPerSecond;
	[SerializeField] Rigidbody2D rb;
	[SerializeField] float chargeSpeed;
	Vector2 playerDirection;


	void OnEnable()
	{
		Debug.Log("Charging");
		// Initialize chargeTimer
		chargeTimer = chargeTime;
	}

	void Update()
	{
		// Check that we're still supposed to be charging
		if (chargeTimer > 0f)
		{
			// Get playerDirection
			playerDirection = playerTransform.position - transform.position;
			playerDirection.Normalize();

			// Spin
			transform.Rotate(0f, 0f, -rotateDegreesPerSecond * Time.deltaTime);
		}
		else
		{
			rb.velocity = Vector2.zero;
			transform.rotation = Quaternion.identity;
			ai.ChooseRandomAttack();
		}

		// Decrement chargeTimer
		chargeTimer -= Time.deltaTime;
	}

	void FixedUpdate()
	{
		rb.velocity = playerDirection * chargeSpeed;
	}
}
