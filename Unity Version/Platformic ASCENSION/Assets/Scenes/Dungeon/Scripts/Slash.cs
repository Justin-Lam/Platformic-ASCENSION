using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			//collision.gameObject.GetComponent<Enemy>().TakeDamage(x);
		}
	}

	void Destroy()      // Called by an event in the "Slash" animation
    {
        Destroy(gameObject);
    }
}
