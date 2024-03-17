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
	[SerializeField] GameObject dashPopupTextGO;
	[SerializeField] float dashMoveSpeed;
	[SerializeField][Tooltip("In seconds")] float dashLength;
	float dashCooldown;
	float dashLengthCounter = 0f;
	float dashCooldownCounter = 0f;
	bool dashUnlocked;

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
		// Get a reference to the PlayerManager
		PlayerManager pm = PlayerManager.instance;

		// Get defaultMoveSpeed
		defaultMoveSpeed = pm.MoveSpeed;

		// Initialize moveSpeed to defaultMoveSpeed
		moveSpeed = defaultMoveSpeed;

		// Get dashUnlocked
		dashUnlocked = pm.DashUnlocked;

		// Get dashCooldown if dash is unlocked
		if (dashUnlocked)
		{
			dashCooldown = pm.DashCooldown;
		}
		else
		{
			dashPopupTextGO.SetActive(false);
		}
	}
	void Update()
	{
		// Movement
		// Record movement input while the player isn't dashing
		if (dashLengthCounter <= 0)
		{
			moveDirection = movementAction.ReadValue<Vector2>();        // Input system should know that this is WASD from the inspector
		}

		// Dashing
		// If we're dashing, keep dashing for the length of dashLength
		if (dashLengthCounter > 0)
		{
			dashLengthCounter -= Time.deltaTime;

			// If we just stopped dashing, re-enable collider, set moveSpeed back to default, and fix dashLengthCounter if needed
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
				dashPopupTextGO.SetActive(true);
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
		// Check that dash is unlocked, off cooldown, and that the player is moving
		if (dashUnlocked && dashCooldownCounter <= 0 && moveDirection != Vector2.zero)
		{
			// Disable collider
			circleCollider.enabled = false;

			// Set adjustedMoveSpeed
			moveSpeed = dashMoveSpeed;

			// Set dashLengthCounter and dashCooldownCounter
			dashLengthCounter = dashLength;
			dashCooldownCounter = dashCooldown;

			// Hide dash popup text
			dashPopupTextGO.SetActive(false);
		}
	}
}
