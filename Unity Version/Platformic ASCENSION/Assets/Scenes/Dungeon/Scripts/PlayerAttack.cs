using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
	[Header("Player Scripts")]
	[SerializeField] PlayerLookAtMouse playerLookAtMouseScript;
	PlayerManager pm;

	[Header("Weapons")]
	[SerializeField] GameObject sword;
	[SerializeField] GameObject gun;
	[SerializeField] GameObject slashPrefab;
	[SerializeField] GameObject bulletPrefab;
	[SerializeField] Transform bulletSpawnPoint;
	enum Weapons { SWORD, GUN }
	Weapons equippedWeapon;
	float slashCooldown;
	float slashCooldownTimer = 0f;
	float shootCooldown;
	float shootCooldownTimer = 0f;

	[Header("Input Actions")]
	[SerializeField] InputAction attackAction;
	[SerializeField] InputAction switchWeaponAction;


	void OnEnable()
	{
		attackAction.Enable();
		switchWeaponAction.Enable();
		switchWeaponAction.performed += SwitchWeapon;
	}
	void OnDisable()
	{
		attackAction.Disable();
		switchWeaponAction.Disable();
	}

	void Start()
	{
		// Get a reference to the PlayerManager
		pm = PlayerManager.instance;

		// Calculate slash and shoot cooldowns
		slashCooldown = 1f / pm.SwordAttackSpeed;
		shootCooldown = 1f / pm.GunAttackSpeed;

		// Equip sword
		EquipSword();
	}

	void Update()
	{
		// Check that the player is pressing/holding the attack button
		if (attackAction.ReadValue<float>() == 1)
		{
			if (equippedWeapon == Weapons.SWORD)        // Sword Attack
			{
				// Check that slash is off cooldown
				if (slashCooldownTimer <= 0f)
				{
					Slash();
					slashCooldownTimer = slashCooldown;
				}
			}
			else                                        // Gun Attack
			{
				// Check that shoot is off cooldown
				if (shootCooldownTimer <= 0f)
				{
					Shoot();
					shootCooldownTimer = shootCooldown;
				}
			}
		}

		// Decrease the cooldowns of slash and shoot if they're on cooldown
		if (slashCooldownTimer > 0f)
		{
			slashCooldownTimer -= Time.deltaTime;

			// Fix value if over subtracted
			if (slashCooldownTimer < 0f)
			{
				slashCooldownTimer = 0f;
			}
		}
		if (shootCooldownTimer > 0f)
		{
			shootCooldownTimer -= Time.deltaTime;

			// Fix value if over subtracted
			if (shootCooldownTimer < 0f)
			{
				shootCooldownTimer = 0f;
			}
		}
	}

	void Slash()
	{
		// Get a vector from the game object's position to the mouse's position
		Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

		// Get the x and y components of the vector
		float x = direction.x;
		float y = direction.y;

		// Clamp the componenets so they can't exceed 1 unit away from the player
		x = Mathf.Clamp(x, -pm.SwordRange, pm.SwordRange);
		y = Mathf.Clamp(y, -pm.SwordRange, pm.SwordRange);

		// Create a slash
		Slash slashScript = Instantiate(slashPrefab, new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z), transform.rotation).GetComponent<Slash>();

		// Set the slash's damage
		slashScript.damage = pm.SwordDamage;
	}

	void Shoot()
	{
		// Get a bullet
		GameObject bulletGO = BulletPool.instance.GetPooledBullet();

		if (bulletGO != null)
		{
			// Move the bullet to the bullet spawn point and make the bullet face in the direction the player is facing
			bulletGO.transform.position = bulletSpawnPoint.position;
			bulletGO.transform.rotation = transform.rotation;

			// Set the bullet to active and set its direction
			bulletGO.SetActive(true);
			bulletGO.GetComponent<Bullet>().SetDirection(playerLookAtMouseScript.FacingDirection);
		}
	}

	void SwitchWeapon(InputAction.CallbackContext context)
	{
		if (equippedWeapon == Weapons.SWORD)
		{
			EquipGun();
		}
		else
		{
			EquipSword();
		}
	}
	void EquipSword()
	{
		gun.SetActive(false);
		equippedWeapon = Weapons.SWORD;
		sword.SetActive(true);
	}
	void EquipGun()
	{
		sword.SetActive(false);
		equippedWeapon = Weapons.GUN;
		gun.SetActive(true);
	}
}
