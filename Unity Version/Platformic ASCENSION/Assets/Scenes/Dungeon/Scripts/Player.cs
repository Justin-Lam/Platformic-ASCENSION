using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon
{
	[SerializeField] int damage;												public int Damage => damage;
	[SerializeField][Tooltip("In attacks per second")] float attackSpeed;		public float AttackSpeed => attackSpeed;
}

[System.Serializable]
public class Sword : Weapon
{
	[SerializeField] float range;												public float Range => range;
}

[System.Serializable]
public class Gun : Weapon
{

}


public class Player : Unit
{
	[Header("Weapon Stats")]
	[SerializeField] Sword sword;		public Sword Sword => sword;
	[SerializeField] Gun gun;			public Gun Gun => gun;

	[Header("HUD")]
	[SerializeField] DungeonHUD hud;


	protected override void Start()
	{
		base.Start();

		// Initialize the health bar in the HUD
		hud.SetPlayerHealthBar(health, maxHealth);
	}

	public override void TakeDamage(int damage)
	{
		base.TakeDamage(damage);

		// Update the health bar in the HUD
		hud.SetPlayerHealthBar(health, maxHealth);
	}

	protected override void Die()
	{
		
	}

	void OnCollisionEnter2D(Collision2D collision)      // For when the player collides with an enemy
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			// Player takes collision damage from the enemy
			gameObject.GetComponent<Unit>().TakeDamage(collision.gameObject.GetComponent<Unit>().CollisionDamage);
		}
	}
}
