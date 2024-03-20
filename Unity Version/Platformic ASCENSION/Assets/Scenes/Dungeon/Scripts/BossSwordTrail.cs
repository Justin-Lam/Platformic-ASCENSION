using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSwordTrail : MonoBehaviour
{
    public void Disarm()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 100f/255f);
            child.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }

    public void Arm()
    {
		foreach (Transform child in transform)
		{
			child.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
			child.gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
		}
	}
}
