using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSwordTrailSlash : MonoBehaviour
{
	[SerializeField] int damage;

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			// Damage the player
			collision.gameObject.GetComponent<Player>().TakeDamage(damage);
		}
	}
}
