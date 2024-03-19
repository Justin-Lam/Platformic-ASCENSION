using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
	enum Attack { CHARGE, DASH_AND_SHOOT, RAPID_SHOOT, CRAZY_SWORD, LASERS }
	Attack attack;
	int attackCounter = 0;

	[Header("State Scripts")]
	[SerializeField] BossStateRest rest;
	[SerializeField] BossStateCharge charge;
	[SerializeField] BossStateDashAndShoot dashAndShoot;
	[SerializeField] BossStateRapidShoot rapidShoot;
	[SerializeField] BossStateCrazySword crazySword;
	[SerializeField] BossStateLasers lasers;


	void Start()
	{
		// Disable all state scripts
		DisableAllStates();

		// Choose a random attack to execute
		ChooseRandomAttack();
	}

	public void ChooseRandomAttack()
	{
		// Disable all states
		DisableAllStates();

		// Check if attackCounter is equal to 3
		if (attackCounter == 3)
		{
			// Execute rest
			rest.enabled = true;

			// Reset attack counter
			attackCounter = 0;
		}
		else
		{
			// Choose a random attack
			attack = (Attack)Random.Range(0, 2);

			// Execute that attack
			switch (attack)
			{
				case Attack.CHARGE:
					charge.enabled = true;
					break;

				case Attack.DASH_AND_SHOOT:
					dashAndShoot.enabled = true;
					break;

				case Attack.RAPID_SHOOT:
					rapidShoot.enabled = true;
					break;

				case Attack.CRAZY_SWORD:
					crazySword.enabled = true;
					break;

				case Attack.LASERS:
					lasers.enabled = true;
					break;

				default:
					Debug.Log("ERROR: Default switch case called in ChooseRandomAttack() of BossAI");
					break;
			}

			// Increment attack counter
			attackCounter++;
		}
	}

	void DisableAllStates()
	{
		rest.enabled = false;
		charge.enabled = false;
		dashAndShoot.enabled = false;
		rapidShoot.enabled = false;
		crazySword.enabled = false;
		lasers.enabled = false;
	}
}
