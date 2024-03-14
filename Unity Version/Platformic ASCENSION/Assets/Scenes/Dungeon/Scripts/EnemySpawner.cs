using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
	[Header("Spawner Variables")]
	[SerializeField] float maxSpawnTime;
	[SerializeField] Transform playerTransform;
	[SerializeField] Timer timerScript;

	[Header("Square Enemy")]
	[SerializeField] GameObject squareEnemyPrefab;
	[SerializeField] float spawnRadiusFromPlayer;
	[SerializeField] float squareMinTimeElapsedToSpawn;
	[SerializeField][Tooltip("Spawn every x seconds")] float squareSpawnRate;
	float squareSpawnTimer = 0f;

	void Update()
	{
		// Spawn Enemies
		if (timerScript.TimeElapsed < maxSpawnTime)
		{
			if (timerScript.TimeElapsed >= squareMinTimeElapsedToSpawn && squareSpawnTimer <= 0f)		// Squares
			{
				// Get a vector pointing in a random direction
				Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
				randomDirection.Normalize();

				// Get the position to spawn the square at
				Vector2 spawnPositionV2 = new Vector2(playerTransform.position.x, playerTransform.position.y) + randomDirection * spawnRadiusFromPlayer;
				Vector3 spawnPosition = new Vector3(spawnPositionV2.x, spawnPositionV2.y, gameObject.transform.position.z);

				// Spawn the square
				Instantiate(squareEnemyPrefab, spawnPosition, gameObject.transform.rotation, gameObject.transform);

				// Reset the spawn timer
				squareSpawnTimer = squareSpawnRate;
			}
		}

		// Decrease spawn timers
		if (squareSpawnTimer > 0f)						// Squares
		{
			squareSpawnTimer -= Time.deltaTime;

			if (squareSpawnTimer < 0f)
			{
				squareSpawnTimer = 0f;
			}
		}
	}
}
