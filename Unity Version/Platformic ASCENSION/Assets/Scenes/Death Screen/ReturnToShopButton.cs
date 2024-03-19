using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToShopButton : MonoBehaviour
{
	public void GoToShop()
	{
		SceneManager.LoadScene("Shop");
	}
}
