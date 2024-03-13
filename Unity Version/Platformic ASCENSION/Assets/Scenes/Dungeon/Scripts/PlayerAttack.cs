using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
	[Header("Player Scripts")]
	[SerializeField] Player playerScript;
	[SerializeField] PlayerLookAtMouse playerLookAtMouseScript;

	[Header("Weapons")]
	[SerializeField] GameObject sword;
	[SerializeField] GameObject gun;
	[SerializeField] GameObject slashPrefab;
	[SerializeField] GameObject bulletPrefab;
	[SerializeField] Transform bulletSpawnPoint;

	[Header("Input Actions")]
	[SerializeField] InputAction attackAction;
	[SerializeField] InputAction switchWeaponAction;

	enum Weapons { SWORD, GUN }
	Weapons equippedWeapon;


	void OnEnable()
	{
		attackAction.Enable();
		attackAction.performed += Attack;

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
		// Equip sword
		sword.SetActive(true);
		equippedWeapon = Weapons.SWORD;
		gun.SetActive(false);
	}

	void Attack(InputAction.CallbackContext context)
	{
		if (equippedWeapon == Weapons.SWORD)		// Sword Attack
		{
			Slash();
		}
		else										// Gun Attack
		{
			Shoot();
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
		x = Mathf.Clamp(x, -playerScript.Sword.Range, playerScript.Sword.Range);
		y = Mathf.Clamp(y, -playerScript.Sword.Range, playerScript.Sword.Range);

		// Create a slash
		Instantiate(slashPrefab, new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z), transform.rotation);
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
			sword.SetActive(false);
			equippedWeapon = Weapons.GUN;
			gun.SetActive(true);
		}
		else
		{
			gun.SetActive(false);
			equippedWeapon = Weapons.SWORD;
			sword.SetActive(true);
		}
	}
}
