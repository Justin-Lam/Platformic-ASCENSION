using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLasers : MonoBehaviour
{
	[SerializeField] float rotateDegreesPerSecond;

    [SerializeField] float armTime;
    float timer;
	bool armed;

	void OnEnable()
	{
		// Initialize rotation
		transform.rotation = Quaternion.identity;

		// Initialize timer and armed
		timer = 0f;
		armed = false;

		// Disarm lasers
		foreach(Transform child in transform)
		{
			child.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 100f / 255f);
			child.gameObject.GetComponent<BoxCollider2D>().enabled = false;
		}
	}

	void Update()
    {
		// Spin
		transform.Rotate(0f, 0f, rotateDegreesPerSecond * Time.deltaTime);

        if (timer >= armTime && !armed)
		{
			// Arm lasers
			foreach (Transform child in transform)
			{
				child.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1f);
				child.gameObject.GetComponent<BoxCollider2D>().enabled = true;
			}

			armed = true;
		}

		timer += Time.deltaTime;
    }
}
