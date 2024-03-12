using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] Rigidbody2D rb;
	[SerializeField] float moveSpeed;
	[SerializeField] InputAction controls;

	Vector2 moveDirection = Vector2.zero;


	void OnEnable()
	{
		controls.Enable();
	}
	void OnDisable()
	{
		controls.Disable();
	}

	void Update()
	{
		// Get moveDirection
		moveDirection = controls.ReadValue<Vector2>();		// Input system should know that this is WASD from the inspector
	}
	void FixedUpdate()
	{
		// Move the player based on input
		rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
	}
}
