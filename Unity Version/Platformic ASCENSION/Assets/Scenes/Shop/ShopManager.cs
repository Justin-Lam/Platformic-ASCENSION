using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
	[Header("Upgrades")]
	[SerializeField] int maxHealthIncreaseAmount = 1;						// Max Health
	[SerializeField] int maxHealthUpgradeCost = 10;
	[SerializeField] int swordDamageIncreaseAmount = 2;						// Damage
	[SerializeField] int gunDamageIncreaseAmount = 1;
	[SerializeField] int damageUpgradeCost = 50;
	[SerializeField] float attackSpeedIncreaseAmount = 0.75f;				// Attack Speed
	[SerializeField] int attackSpeedUpgradeCost = 50;
	[SerializeField] int unlockDashCost = 100;								// Unlock Dash
	[SerializeField] float dashCooldownDecreaseAmount = 0.25f;				// Dash Cooldown
	[SerializeField] int dashCooldownUpgradeCost = 10;

	[Header("Upgrades Panel")]
	[SerializeField] TextMeshProUGUI goldText;								// Gold
	[SerializeField] TextMeshProUGUI maxHealthIncreaseAmountText;			// Max Health
	[SerializeField] TextMeshProUGUI maxHealthUpgradeCostText;
	[SerializeField] TextMeshProUGUI damageIncreaseAmountText;				// Damage
	[SerializeField] TextMeshProUGUI damageUpgradeCostText;
	[SerializeField] TextMeshProUGUI attackSpeedIncreaseAmountText;			// Attack Speed
	[SerializeField] TextMeshProUGUI attackSpeedUpgradeCostText;
	[SerializeField] GameObject dashPreUnlockGO;							// Unlock Dash
	[SerializeField] TextMeshProUGUI dashDefaultCooldownText;
	[SerializeField] TextMeshProUGUI unlockDashCostText;
	[SerializeField] GameObject dashPostUnlockGO;
	[SerializeField] GameObject upgradeDashCooldownGO;						// Dash Cooldown
	[SerializeField] TextMeshProUGUI dashCooldownDecreaseAmountText;
	[SerializeField] TextMeshProUGUI dashCooldownUpgradeCostText;

	[Header("Stats Panel")]
	[SerializeField] TextMeshProUGUI maxHealthValueText;					// Max Health
	[SerializeField] TextMeshProUGUI swordDamageValueText;					// Damage
	[SerializeField] TextMeshProUGUI gunDamageValueText;
	[SerializeField] TextMeshProUGUI swordAttackSpeedValueText;				// Attack Speed
	[SerializeField] TextMeshProUGUI gunAttackSpeedValueText;
	[SerializeField] GameObject dashCooldownTextGO;							// Dash Cooldown
	[SerializeField] TextMeshProUGUI dashCooldownValueText;


	public void ResetUpgradeVariables()
	{
		maxHealthIncreaseAmount = 1;
		maxHealthUpgradeCost = 10;
		swordDamageIncreaseAmount = 2;
		gunDamageIncreaseAmount = 1;
		damageUpgradeCost = 50;
		attackSpeedIncreaseAmount = 0.75f;
		attackSpeedUpgradeCost = 50;
		unlockDashCost = 100;
		dashCooldownDecreaseAmount = 0.25f;
		dashCooldownUpgradeCost = 10;
	}

	void Start()
	{
		// Get the PlayerManager
		PlayerManager pm = PlayerManager.instance;

		// Initialize the upgrades panel
		goldText.text = "Gold: " + pm.Gold;
		maxHealthIncreaseAmountText.text = "+" + maxHealthIncreaseAmount + " HP";
		maxHealthUpgradeCostText.text = maxHealthUpgradeCost + "g";
		damageIncreaseAmountText.text = "+" + swordDamageIncreaseAmount + " Sword, +" + gunDamageIncreaseAmount + " Gun";
		damageUpgradeCostText.text = damageUpgradeCost + "g";
		attackSpeedIncreaseAmountText.text = "+" + attackSpeedIncreaseAmount + " Attacks/sec";
		attackSpeedUpgradeCostText.text = attackSpeedUpgradeCost + "g";
		if (pm.DashUnlocked)
		{
			// Show dash post unlock UI
			dashPreUnlockGO.SetActive(false);
			dashPostUnlockGO.SetActive(true);

			// Show upgrade dash cooldown
			upgradeDashCooldownGO.SetActive(true);
			dashCooldownDecreaseAmountText.text = "-" + dashCooldownDecreaseAmount + "s";
			dashCooldownUpgradeCostText.text = dashCooldownUpgradeCost + "g";
		}
		else
		{
			// Show dash pre unlock UI
			dashPreUnlockGO.SetActive(true);
			dashPostUnlockGO.SetActive(false);
			dashDefaultCooldownText.text = "Default Cooldown: " + pm.InitialDashCooldown + "s";
			unlockDashCostText.text = unlockDashCost + "g";

			// Hide upgrade dash cooldown
			upgradeDashCooldownGO.SetActive(false);
		}

		// Initialize the stats panel
		maxHealthValueText.text = pm.MaxHealth.ToString();
		swordDamageValueText.text = pm.SwordDamage.ToString();
		gunDamageValueText.text = pm.GunDamage.ToString();
		swordAttackSpeedValueText.text = pm.SwordAttackSpeed.ToString();
		gunAttackSpeedValueText.text = pm.GunAttackSpeed.ToString();
		if (pm.DashUnlocked)
		{
			// Show and set dash cooldown text
			dashCooldownTextGO.SetActive(true);
			dashCooldownValueText.text = pm.DashCooldown.ToString();
		}
		else
		{
			// Hide dash cooldown text
			dashCooldownTextGO.SetActive(false);
		}
	}

	public void OnUpgradeMaxHealthButtonClicked()
	{
		// Get the PlayerManager
		PlayerManager pm = PlayerManager.instance;

		// Check that the player has enough gold
		if (pm.Gold >= maxHealthUpgradeCost)
		{
			// Decrease gold
			pm.DecreaseGold(maxHealthUpgradeCost);

			// Increase health
			pm.IncreaseMaxHealth(maxHealthIncreaseAmount);

			// Update gold
			goldText.text = "Gold: " + pm.Gold;

			// Update stats panel
			maxHealthValueText.text = pm.MaxHealth.ToString();
		}
	}
	public void OnUpgradeDamageButtonClicked()
	{
		// Get the PlayerManager
		PlayerManager pm = PlayerManager.instance;

		// Check that the player has enough gold
		if (pm.Gold >= damageUpgradeCost)
		{
			// Decrease gold
			pm.DecreaseGold(damageUpgradeCost);

			// Increase damages
			pm.IncreaseSwordDamage(swordDamageIncreaseAmount);
			pm.IncreaseGunDamage(gunDamageIncreaseAmount);

			// Update gold
			goldText.text = "Gold: " + pm.Gold;

			// Update stats panel
			swordDamageValueText.text = pm.SwordDamage.ToString();
			gunDamageValueText.text = pm.GunDamage.ToString();
		}
	}
	public void OnUpgradeAttackSpeedButtonClicked()
	{
		// Get the PlayerManager
		PlayerManager pm = PlayerManager.instance;

		// Check that the player has enough gold
		if (pm.Gold >= attackSpeedUpgradeCost)
		{
			// Decrease gold
			pm.DecreaseGold(attackSpeedUpgradeCost);

			// Increase attack speeds
			pm.IncreaseSwordAttackSpeed(attackSpeedIncreaseAmount);
			pm.IncreaseGunAttackSpeed(attackSpeedIncreaseAmount);

			// Update gold
			goldText.text = "Gold: " + pm.Gold;

			// Update stats panel
			swordAttackSpeedValueText.text = pm.SwordAttackSpeed.ToString();
			gunAttackSpeedValueText.text = pm.GunAttackSpeed.ToString();
		}
	}
	public void OnUnlockDashButtonPressed()
	{
		// Get the PlayerManager
		PlayerManager pm = PlayerManager.instance;

		// Check that the player has enough gold
		if (pm.Gold >= unlockDashCost)
		{
			// Decrease gold
			pm.DecreaseGold(unlockDashCost);

			// Unlock dash
			pm.UnlockDash();

			// Update gold
			goldText.text = "Gold: " + pm.Gold;

			// Update upgrades panel
			dashPreUnlockGO.SetActive(false);
			dashPostUnlockGO.SetActive(true);
			upgradeDashCooldownGO.SetActive(true);

			// Update stats panel
			dashCooldownTextGO.SetActive(true);
			dashCooldownValueText.text = pm.DashCooldown.ToString();
		}
	}
	public void OnUpgradeDashCooldownButtonClicked()
	{
		// Get the PlayerManager
		PlayerManager pm = PlayerManager.instance;

		// Check that the player has enough gold
		if (pm.Gold >= dashCooldownUpgradeCost)
		{
			// Decrease gold
			pm.DecreaseGold(dashCooldownUpgradeCost);

			// Decrease dash cooldown
			pm.DecreaseDashCooldown(dashCooldownDecreaseAmount);

			// Update gold
			goldText.text = "Gold: " + pm.Gold;

			// Update stats panel
			dashCooldownValueText.text = pm.DashCooldown.ToString();
		}
	}

	public void GoToDungeon()
	{
		SceneManager.LoadScene("Dungeon");
	}
}
