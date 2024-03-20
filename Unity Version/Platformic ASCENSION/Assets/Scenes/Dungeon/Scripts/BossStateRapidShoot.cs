using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateRapidShoot : MonoBehaviour
{
	[SerializeField] BossAI ai;
	[SerializeField] GameObject projectilePortalsGO;
	[SerializeField] float duration;
	float timer;

	void OnEnable()
	{
		// Enable projectile portals
		projectilePortalsGO.SetActive(true);

		// Initialize timer
		timer = duration;
	}

	void Update()
	{
		if (timer <= 0f)
		{
			projectilePortalsGO.SetActive(false);
			ai.ChooseRandomAttack();
		}

		// Decrement timer
		timer -= Time.deltaTime;
	}
}
