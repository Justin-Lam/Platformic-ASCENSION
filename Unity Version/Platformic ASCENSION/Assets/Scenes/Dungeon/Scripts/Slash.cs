using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
	public int damage;

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			collision.gameObject.GetComponent<Unit>().TakeDamage(damage);
		}
	}

	void Destroy()      // Called by an event in the "Slash" animation
    {
        Destroy(gameObject);
    }
}
