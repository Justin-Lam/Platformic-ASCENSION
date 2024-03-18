using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAITriangle : MonoBehaviour
{
	enum State { MOVING, WAITING }
	State state = State.MOVING;
	float timer = 0f;

	[Header("Moving")]
	[SerializeField] float moveTime;
	[SerializeField] float turnAngle;
	bool moveStuffSet = false;
	float moveSpeed;
	Vector2 moveDirection = Vector2.zero;
	float rotationAngle;

	[Header("Waiting")]
	[SerializeField] float waitTime;

	[Header("Projectile")]
	[SerializeField] int projectileDamage;
	[SerializeField] GameObject projectile;

	[Header("Components")]
	[SerializeField] Rigidbody2D rb;
	Transform playerTransform;


	void Start()
	{
		// Get playerTransform
		playerTransform = GameObject.Find("Player").transform;

		// Get moveSpeed
		moveSpeed = GetComponent<Enemy>().MoveSpeed;
	}

	void Update()
	{
		if (state == State.MOVING)
		{
			if (!moveStuffSet)
			{
				// Face the player:
				FacePlayer();

				// Turn 45 degrees either clockwise or counter-clockwise
				if (Random.Range(0, 2) == 0)
				{
					// Rotate 45 degrees clockwise
					transform.Rotate(Vector3.forward, turnAngle);
				}
				else
				{
					// Rotate 45 degrees counter-clockwise
					transform.Rotate(Vector3.forward, -turnAngle);
				}

				// Set moveStuffSet to true
				moveStuffSet = true;
			}
			if (timer >= moveTime)
			{
				// Face the player and shoot a projectile
				FacePlayer();
				TriangleEnemyProjectile projectileScript = Instantiate(projectile, transform.position, transform.rotation).GetComponent<TriangleEnemyProjectile>();
				projectileScript.SetDamage(projectileDamage);


				// Start waiting
				state = State.WAITING;
				timer = 0f;
			}
		}

		if (state == State.WAITING)
		{
			if (timer >= waitTime)
			{
				// Start moving
				state = State.MOVING;
				timer = 0f;
				moveStuffSet = false;
			}
		}

		// Increment timer
		timer += Time.deltaTime;
	}

	void FixedUpdate()
	{
		if (state == State.MOVING)
		{
			rb.velocity = transform.right * moveSpeed;
		}
		else
		{
			rb.velocity = Vector2.zero;
		}
	}

	void FacePlayer()
	{
		// Get the direction from the triangle enemy to the player
		moveDirection = playerTransform.position - transform.position;
		moveDirection.Normalize();
		// Calculate the angle in degrees
		rotationAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		// Rotate the triangle enemy to face the player
		transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
	}
}
