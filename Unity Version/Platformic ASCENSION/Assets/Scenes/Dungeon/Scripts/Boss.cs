using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
	[SerializeField] int maxHealth;
	int health;
	[SerializeField] int collisionDamage; public int CollisionDamage => collisionDamage;
	[SerializeField] Slider healthBar;


	void Awake()
	{
		// Initialize health
		health = maxHealth;
	}

	private void Start()
	{
		// Show and set health bar
		healthBar.gameObject.SetActive(true);
		SetHealthBar();
	}

	public void TakeDamage(int damage)
	{
		// Take damage
		health -= damage;

		// Update health bar
		SetHealthBar();

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
	void SetHealthBar()
	{
		healthBar.value = (float)health / (float)maxHealth;
	}

	void Die()
	{
		// To to victory screen
		SceneManager.LoadScene("Victory Screen");
	}
}
