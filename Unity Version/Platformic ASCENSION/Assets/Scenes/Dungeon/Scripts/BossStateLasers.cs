using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateLasers : MonoBehaviour
{
	[SerializeField] BossAI ai;

	[SerializeField] GameObject lasersGO;
	[SerializeField] float duration;
	float timer;

	void OnEnable()
	{
		timer = duration;
		lasersGO.SetActive(true);
	}

	void Update()
	{
		transform.rotation = Quaternion.identity;

		if (timer <= 0)
		{
			lasersGO.SetActive(false);
			ai.ChooseRandomAttack();
		}

		timer -= Time.deltaTime;
	}
}
