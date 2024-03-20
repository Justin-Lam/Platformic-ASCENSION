using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateRest : MonoBehaviour
{
	[SerializeField] BossAI ai;

	[SerializeField] float restTime;
	float restTimer;

	void OnEnable()
	{
		restTimer = restTime;
	}

	void Update()
	{
		if (restTimer <= 0f)
		{
			ai.ChooseRandomAttack();
		}

		restTimer -= Time.deltaTime;
	}
}
