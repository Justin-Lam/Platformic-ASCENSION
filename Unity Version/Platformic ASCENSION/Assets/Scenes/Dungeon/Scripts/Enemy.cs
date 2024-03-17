using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[Header("Stats")]
	[SerializeField] int maxHealth;
	[SerializeField] float moveSpeed;			public float MoveSpeed => moveSpeed;
	[SerializeField] int collisionDamage;		public int CollisionDamage => collisionDamage;
	[SerializeField] int value;
	int health;

	[Header("Death")]
	[SerializeField] Rigidbody2D rb;
	[SerializeField] Animator animator;

	DungeonHUD dungeonHUDScript;


	void Awake()
	{
		// Initialize health
		health = maxHealth;
	}

	public void TakeDamage(int damage)
	{
		// Take damage
		health -= damage;

		// Check if dead
		if (health <= 0)
		{
			health = 0;
			Die();
		}
	}
	void OnCollisionEnter2D(Collision2D collision)      // For when the player collides with the enemy
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			// Enemy takes collision damage from the player
			TakeDamage(collision.gameObject.GetComponent<Player>().CollisionDamage);
		}
	}

	void Die()
	{
		// Remove the enemy from the physics simulation
		rb.simulated = false;

		// Play death animation
		animator.SetTrigger("died");

		// Increase the player's gold
		PlayerManager.instance.IncreaseGold(value);

		// Update the gold counter
		GameObject.Find("HUD Canvas").GetComponent<DungeonHUD>().SetGoldCounter();
	}
	void Destroy()
	{
		Destroy(gameObject);
	}
}
