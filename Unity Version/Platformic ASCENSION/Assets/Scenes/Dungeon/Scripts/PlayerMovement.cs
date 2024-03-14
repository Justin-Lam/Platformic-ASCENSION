using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	// Movement
	float defaultMoveSpeed;
	float moveSpeed;
	Vector2 moveDirection;

	[Header("Dashing")]
	[SerializeField] float dashMoveSpeed;
	[SerializeField][Tooltip("In seconds")] float dashLength;
	[SerializeField] float dashCooldown;
	float dashLengthCounter = 0f;
	float dashCooldownCounter = 0f;

	[Header("Rigidbody and collider")]
	[SerializeField] Rigidbody2D rb;
	[SerializeField] CircleCollider2D circleCollider;

	[Header("Input Actions")]
	[SerializeField] InputAction movementAction;
	[SerializeField] InputAction dashAction;


	void OnEnable()
	{
		movementAction.Enable();
		dashAction.Enable();
		dashAction.performed += Dash;
	}
	void OnDisable()
	{
		movementAction.Disable();
		dashAction.Disable();
	}

	void Start()
	{
		// Get defaultMoveSpeed
		defaultMoveSpeed = GetComponent<Unit>().MoveSpeed;

		// Initialize moveSpeed to defaultMoveSpeed
		moveSpeed = defaultMoveSpeed;
	}
	void Update()
	{
		// Movement
		// Record movement input when the player isn't dashing
		if (dashLengthCounter <= 0)
		{
			moveDirection = movementAction.ReadValue<Vector2>();        // Input system should know that this is WASD from the inspector
		}

		// Dashing
		// If we're dashing, keep dashing for the length of dashLength
		if (dashLengthCounter > 0)
		{
			dashLengthCounter -= Time.deltaTime;

			// If we just stopped dashing, reenable collider, set moveSpeed back to default, and fix dashLengthCounter if needed
			if (dashLengthCounter < 0)
			{
				circleCollider.enabled = true;
				moveSpeed = defaultMoveSpeed;
				dashLengthCounter = 0;
			}
		}
		// If dash is on cooldown, start making it not on cooldown
		if (dashCooldownCounter > 0)
		{
			dashCooldownCounter -= Time.deltaTime;

			// Fix dashCooldownCounter if needed
			if (dashCooldownCounter < 0)
			{
				dashCooldownCounter = 0;
			}
		}
	}
	void FixedUpdate()
	{
		// Move the player based on input
		rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
	}

	void Dash(InputAction.CallbackContext context)
	{
		// Check that dash is off cooldown
		if (dashCooldownCounter <= 0)
		{
			// Check that the player is moving
			if (moveDirection != Vector2.zero)
			{
				// Disable collider
				circleCollider.enabled = false;

				// Set adjustedMoveSpeed
				moveSpeed = dashMoveSpeed;

				// Set dashLengthCounter and dashCooldownCounter
				dashLengthCounter = dashLength;
				dashCooldownCounter = dashCooldown;
			}
		}
	}
}
