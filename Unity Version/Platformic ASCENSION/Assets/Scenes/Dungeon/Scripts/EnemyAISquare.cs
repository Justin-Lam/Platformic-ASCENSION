using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAISquare : MonoBehaviour
{
	[SerializeField] Rigidbody2D rb;

	Transform playerTransform;
    float moveSpeed;
    Vector2 moveDirection;

    void Start()
    {
		// Get playerTransform
		playerTransform = GameObject.Find("Player").transform;

        // Get moveSpeed
        moveSpeed = GetComponent<Unit>().MoveSpeed;
    }

    void Update()
    {
		// Get moveDirection: a normalized vector from the square enemy's position to the player's position
		moveDirection = playerTransform.position - transform.position;
		moveDirection.Normalize();

		// Get the angle the square enemy needs to rotate in order to face the player
		float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

		// Rotate the square enemy to face the player
		transform.rotation = Quaternion.Euler(Vector3.forward * angle);
	}

	void FixedUpdate()
	{
		// Move towards the player
		rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
	}
}
