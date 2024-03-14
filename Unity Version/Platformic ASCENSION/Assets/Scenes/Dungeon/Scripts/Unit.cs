using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	[SerializeField] protected int maxHealth;
	protected int health = 0;
	[SerializeField] protected int collisionDamage;		public int CollisionDamage => collisionDamage;
	[SerializeField] float moveSpeed;					public float MoveSpeed => moveSpeed;


	protected virtual void Start()					// overrided by player
	{
		// Initialize health
		health = maxHealth;
	}

	public virtual void TakeDamage(int damage)		// overrided by player
	{
		health -= damage;

		if (health <= 0)
		{
			health = 0;
			Die();
		}

		if (gameObject.CompareTag("Enemy"))
		{
			Debug.Log(health);
		}
	}

	protected virtual void Die() { }
}
