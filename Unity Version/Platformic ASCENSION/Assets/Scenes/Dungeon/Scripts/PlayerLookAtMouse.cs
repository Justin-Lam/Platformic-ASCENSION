using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAtMouse : MonoBehaviour
{
	Vector2 facingDirection;		public Vector2 FacingDirection => facingDirection;

	void Update()
	{
		// Get a vector from the game object's position to the mouse's position
		Vector3 direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);

		// Update facingDirection
		facingDirection = new Vector2(direction.x, direction.y).normalized;

		// Calculate the angle of the vector
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;		// note the y then x; it needs to be this way because tan = y/x

		// Rotate the game object by the angle
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
