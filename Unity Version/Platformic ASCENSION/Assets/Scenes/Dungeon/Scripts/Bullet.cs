using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[HideInInspector] public int damage;
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody2D rb;

	public void SetDirection(Vector2 playerFacingDirection)
	{
		rb.velocity = playerFacingDirection * moveSpeed;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Bullet Wall"))
		{
			// Deactivate the bullet
			gameObject.SetActive(false);
		}

		if (collision.CompareTag("Enemy"))
		{
			// Damage the enemy
			collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
		}
	}
}
