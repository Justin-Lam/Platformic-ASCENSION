using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
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
			gameObject.SetActive(false);
		}

		if (collision.CompareTag("Enemy"))
		{

		}
	}
}
