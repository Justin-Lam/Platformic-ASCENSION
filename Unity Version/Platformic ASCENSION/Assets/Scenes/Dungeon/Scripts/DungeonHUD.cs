using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DungeonHUD : MonoBehaviour
{
	[Header("Gold Counter")]
	[SerializeField] TextMeshProUGUI goldText;

	[Header("Player Health Bar")]
	[SerializeField] Slider healthBarSlider;
	[SerializeField] TextMeshProUGUI healthText;

	void Start()
	{
		// Initialize gold counter
		SetGoldCounter();
	}

	public void SetGoldCounter()
	{
		goldText.text = "Gold: " + PlayerManager.instance.Gold;
	}

	public void SetPlayerHealthBar(int health, int maxHealth)
	{
		healthBarSlider.value = (float)health / (float)maxHealth;
		healthText.text = health + "/" + maxHealth;
	}
}
