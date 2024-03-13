using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool instance;      // singleton pattern

    [SerializeField] int poolAmount;
    [SerializeField] GameObject bulletPrefab;
    List<GameObject> pooledBullets = new List<GameObject>();

	void Awake()
	{
		if (instance == null)
        {
            instance = this;
        }
	}

	void Start()
    {
        // Create the pooled bullets
        for (int i = 0; i < poolAmount; i++)
        {
            GameObject obj = Instantiate(bulletPrefab, gameObject.transform);       // create as children of the game object this script is attatched to so the hierarchy doesn't get too crowded
            obj.SetActive(false);
            pooledBullets.Add(obj);
        }
    }

    public GameObject GetPooledBullet()
    {
        // Search through the pooled bullets for one that isn't active
        for (int i = 0; i < pooledBullets.Count; i++)
        {
            // Return the first found inactive pooled bullet
            if (!pooledBullets[i].activeInHierarchy)
            {
                return pooledBullets[i];
            }
        }

        // Return null if there were no inactive bullets found
        Debug.Log("PROBLEM: All pooled bullets are active");
        return null;
    }
}
