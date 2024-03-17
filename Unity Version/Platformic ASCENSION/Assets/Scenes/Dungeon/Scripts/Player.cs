using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Header("HUD")]
	[SerializeField] DungeonHUD dungeonHUDScript;

	// Stats
	int maxHealth;
	int health;
	int collisionDamage;		public int CollisionDamage => collisionDamage;


	void Start()
	{
		// Get a reference to the PlayerManager
		PlayerManager pm = PlayerManager.instance;

		// Initialize stats
		maxHealth = pm.MaxHealth;
		collisionDamage = pm.CollisionDamage;

		// Initialize health
		health = maxHealth;

		// Initialize the health bar in the HUD
		dungeonHUDScript.SetPlayerHealthBar(health, maxHealth);
	}

	public void TakeDamage(int damage)
	{
		// Take damage
		health -= damage;

		// Update the health bar
		dungeonHUDScript.SetPlayerHealthBar(health, maxHealth);

		// Check if dead
		if (health <= 0)
		{
			health = 0;
			Die();
		}
	}

	void OnCollisionEnter2D(Collision2D collision)      // For when an enemy collides with the player
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			// Player takes collision damage from the enemy
			TakeDamage(collision.gameObject.GetComponent<Enemy>().CollisionDamage);
		}
	}

	void Die()
	{
		// Go the death scene
	}
}
