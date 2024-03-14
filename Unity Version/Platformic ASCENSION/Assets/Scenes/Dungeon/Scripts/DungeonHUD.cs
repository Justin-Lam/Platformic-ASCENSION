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

	public void SetGoldCounter(int gold)
	{
		goldText.text = "Gold: " + gold;
	}

	public void SetPlayerHealthBar(int health, int maxHealth)
	{
		healthBarSlider.value = (float)health / (float)maxHealth;
		healthText.text = health + "/" + maxHealth;
	}
}
