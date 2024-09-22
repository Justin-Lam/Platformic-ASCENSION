using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleEnemyProjectile : MonoBehaviour
{
    // Damage
    int damage;

	[Header("Movement")]
	[SerializeField] Rigidbody2D rb;
    [SerializeField] float moveSpeed;
	Vector2 moveDirection;

	[Header("Rotation")]
    [SerializeField] float rotateDegreesPerSecond;

    [Header("Death")]
    [SerializeField] float lifeSpan;


    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    void Start()
    {
        // Start moving in the direction the triangle enemy is facing
        rb.velocity = transform.right * moveSpeed;

        // Activate death timer
        StartCoroutine(ActivateDeathTimer());
    }

    void Update()
    {
        // Spin
        transform.Rotate(Vector3.forward, -rotateDegreesPerSecond * Time.deltaTime);
    }

    IEnumerator ActivateDeathTimer()
    {
        yield return new WaitForSeconds(lifeSpan);
        Destroy(gameObject);
    }

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
        {
            // Damage the player and destroy self
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject);
        }
	}
}
