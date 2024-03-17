using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
	[HideInInspector] public int damage;

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			// Damage the enemy
			collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
		}
	}

	void Destroy()      // Called by an event in the "Slash" animation
    {
        Destroy(gameObject);
    }
}
