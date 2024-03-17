using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
	// Singleton Pattern
	public static PlayerManager instance;
	
	// Variables and Their Upgrade Functions
	[Header("Upgradable Player Variables")]
	[SerializeField] int initialMaxHealth = 5;					// Max Health
	[SerializeField] int initialSwordDamage = 2;				// Sword
	[SerializeField] float initialSwordAttackSpeed = 1.25f;
	[SerializeField] int initialGunDamage = 1;					// Gun
	[SerializeField] float initialGunAttackSpeed = 1.25f;
	[SerializeField] bool dashUnlocked = false;				public bool DashUnlocked => dashUnlocked;						// Dash
	[SerializeField] float initialDashCooldown = 5f;		public float InitialDashCooldown => initialDashCooldown;
	int maxHealth;				public int MaxHealth => maxHealth;						// Max Health
	int swordDamage;			public int SwordDamage => swordDamage;					// Sword
	float swordAttackSpeed;		public float SwordAttackSpeed => swordAttackSpeed;
	int gunDamage;				public int GunDamage => gunDamage;						// Gun
	float gunAttackSpeed;		public float GunAttackSpeed => gunAttackSpeed;
	float dashCooldown;			public float DashCooldown => dashCooldown;              // Dash
	public void IncreaseMaxHealth(int amount) { maxHealth += amount; }						// Max Health
	public void IncreaseSwordDamage(int amount) { swordDamage += amount; }					// Sword
	public void IncreaseSwordAttackSpeed(float amount) { swordAttackSpeed += amount; }
	public void IncreaseGunDamage(int amount) { gunDamage += amount; }						// Gun
	public void IncreaseGunAttackSpeed(float amount) { gunAttackSpeed += amount; }
	public void UnlockDash() { dashUnlocked = true; }										// Dash
	public void DecreaseDashCooldown(float amount) { dashCooldown -= amount; }

	[Header("Non-upgradable Player Variables")]
	[SerializeField] int initialGold = 0;
	[SerializeField] float moveSpeed = 3.5f;		public float MoveSpeed => moveSpeed;
	[SerializeField] int collisionDamage = 5;		public int CollisionDamage => collisionDamage;
	[SerializeField] float swordRange = 1f;			public float SwordRange => swordRange;
	int gold; public int Gold => gold;
	public void IncreaseGold(int amount) { gold += amount; }
	public void DecreaseGold(int amount) { gold -= amount; }


	// Functions
	void Awake()
	{
		// Singleton Pattern
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
			return;
		}

		// Initialize variables to their initial values
		gold = initialGold;

		maxHealth = initialMaxHealth;

		swordDamage = initialSwordDamage;
		swordAttackSpeed = initialSwordAttackSpeed;

		gunDamage = initialGunDamage;
		gunAttackSpeed = initialGunAttackSpeed;

		dashCooldown = initialDashCooldown;
	}
}
