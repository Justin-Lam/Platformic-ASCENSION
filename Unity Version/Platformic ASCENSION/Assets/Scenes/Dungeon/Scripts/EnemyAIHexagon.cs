using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIHexagon : MonoBehaviour
{
	enum State { CREEPING, CHARGING, WAITING }
	State state = State.CREEPING;
	float timer = 0f;

	// Rotation
	Transform playerTransform;

	[Header("Movement")]
	[SerializeField] float creepTime;
	[SerializeField] float creepSpeed;
	[SerializeField] float chargeTime;
	[SerializeField] float chargeSpeed;
	[SerializeField] Rigidbody2D rb;
	float moveSpeed;

	[Header("Waiting")]
	[SerializeField] float waitTime;


	void Start()
	{
		// Get playerTransform
		playerTransform = GameObject.Find("Player").transform;

		// Initialize moveSpeed to creepSpeed
		moveSpeed = creepSpeed;
	}

	void Update()
	{
		if (state == State.CREEPING)
		{
			if (timer < creepTime)
			{
				// Face the player
				Vector2 playerDirection = playerTransform.position - transform.position;
				float angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			}
			else
			{
				// Set state to charging
				state = State.CHARGING;
				moveSpeed = chargeSpeed;
				timer = 0f;
			}
		}

		if (state == State.CHARGING)
		{
			if (timer >= chargeTime)
			{
				// Set state to waiting
				state = State.WAITING;
				moveSpeed = 0f;
				timer = 0f;
			}
		}

		if (state == State.WAITING)
		{
			if (timer >= waitTime)
			{
				// Set state to creeping
				state = State.CREEPING;
				moveSpeed = creepSpeed;
				timer = 0f;
			}
		}

		// Increment timer
		timer += Time.deltaTime;
	}

	void FixedUpdate()
	{
		rb.velocity = transform.right * moveSpeed;
	}
}