using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
	[Header("Stats")]
	[SerializeField] int maxHealth;
	[SerializeField] int collisionDamage; public int CollisionDamage => collisionDamage;
	int health;


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
		// To to victory screen
		SceneManager.LoadScene("Victory Screen");
	}
}
