using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
	[SerializeField] int value;

	[Header("Death")]
	[SerializeField] Rigidbody2D rb;
	[SerializeField] Animator animator;


	protected override void Die()
	{
		// Remove the enemy from the physics simulation
		rb.simulated = false;

		// Play death animation
		animator.SetTrigger("died");

		// Increase the player's gold
		PlayerManager.instance.IncreaseGold(value);
	}
	void Destroy()
	{
		Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D collision)		// For when an enemy collides with the player
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			// Enemy takes collision damage from the player
			gameObject.GetComponent<Unit>().TakeDamage(collision.gameObject.GetComponent<Unit>().CollisionDamage);
		}
	}
}
