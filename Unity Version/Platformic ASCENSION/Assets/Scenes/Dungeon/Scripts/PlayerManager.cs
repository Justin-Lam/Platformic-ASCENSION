using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
	public static PlayerManager instance;

	DungeonHUD dungeonHUDScript;

	int gold = 0;		public int Gold => gold;

	int maxHealth;		public int MaxHealth => maxHealth;


	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (SceneManager.GetActiveScene().name == "Dungeon")
		{
			// Set dungeonHUDScript
			dungeonHUDScript = FindFirstObjectByType<DungeonHUD>();

			// Update the gold counter in the HUD
			dungeonHUDScript.SetGoldCounter(gold);
		}
	}

	public void IncreaseGold(int amount)
	{
		// Increase gold
		gold += amount;

		// Check if we're in the dungeon
		if (SceneManager.GetActiveScene().name == "Dungeon")
		{
			// Update the gold counter in the HUD
			dungeonHUDScript.SetGoldCounter(gold);
		}
	}
}
