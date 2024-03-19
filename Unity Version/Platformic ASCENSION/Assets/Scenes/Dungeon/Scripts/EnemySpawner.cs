using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
	[Header("Spawner Variables")]
	[SerializeField] float maxSpawnTime;
	[SerializeField] float doublingTime;
	bool increasedSpawnRates = false;
	[SerializeField] float spawnRadiusFromPlayer;
	[SerializeField] Transform playerTransform;
	[SerializeField] Timer timerScript;

	[Header("Square Enemy")]
	[SerializeField] GameObject squareEnemyPrefab;
	[SerializeField] float squareMinTimeElapsedToSpawn;
	[SerializeField][Tooltip("Spawn every x seconds")] float squareSpawnRate;
	float squareSpawnTimer = 0f;

	[Header("Triangle Enemy")]
	[SerializeField] GameObject triangleEnemyPrefab;
	[SerializeField] float triangleMinTimeElapsedToSpawn;
	[SerializeField][Tooltip("Spawn every x seconds")] float triangleSpawnRate;
	float triangleSpawnTimer = 0f;

	[Header("Hexagon Enemy")]
	[SerializeField] GameObject hexagonEnemyPrefab;
	[SerializeField] float hexagonMinTimeElapsedToSpawn;
	[SerializeField][Tooltip("Spawn every x seconds")] float hexagonSpawnRate;
	float hexagonSpawnTimer = 0f;


	void Update()
	{
		// Spawn Enemies
		if (timerScript.TimeElapsed < maxSpawnTime)
		{
			if (timerScript.TimeElapsed >= squareMinTimeElapsedToSpawn && squareSpawnTimer <= 0f)			// Squares
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

			if (timerScript.TimeElapsed >= triangleMinTimeElapsedToSpawn && triangleSpawnTimer <= 0f)		// Triangles
			{
				// Get a vector pointing in a random direction
				Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
				randomDirection.Normalize();

				// Get the position to spawn the triangle at
				Vector2 spawnPositionV2 = new Vector2(playerTransform.position.x, playerTransform.position.y) + randomDirection * spawnRadiusFromPlayer;
				Vector3 spawnPosition = new Vector3(spawnPositionV2.x, spawnPositionV2.y, gameObject.transform.position.z);

				// Spawn the square
				Instantiate(triangleEnemyPrefab, spawnPosition, gameObject.transform.rotation, gameObject.transform);

				// Reset the spawn timer
				triangleSpawnTimer = triangleSpawnRate;
			}

			if (timerScript.TimeElapsed >= hexagonMinTimeElapsedToSpawn && hexagonSpawnTimer <= 0f)			// Hexagons
			{
				// Get a vector pointing in a random direction
				Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
				randomDirection.Normalize();

				// Get the position to spawn the triangle at
				Vector2 spawnPositionV2 = new Vector2(playerTransform.position.x, playerTransform.position.y) + randomDirection * spawnRadiusFromPlayer;
				Vector3 spawnPosition = new Vector3(spawnPositionV2.x, spawnPositionV2.y, gameObject.transform.position.z);

				// Spawn the hexagon
				Instantiate(hexagonEnemyPrefab, spawnPosition, gameObject.transform.rotation, gameObject.transform);

				// Reset the spawn timer
				hexagonSpawnTimer = hexagonSpawnRate;
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
		if (triangleSpawnTimer > 0f)                    // Triangle
		{
			triangleSpawnTimer -= Time.deltaTime;

			if (triangleSpawnTimer < 0f)
			{
				triangleSpawnTimer = 0f;
			}
		}
		if (hexagonSpawnTimer > 0f)                     // Hexagon
		{
			hexagonSpawnTimer -= Time.deltaTime;

			if (hexagonSpawnTimer < 0f)
			{
				hexagonSpawnTimer = 0f;
			}
		}

		// Check if the doubling time has been reached
		if (timerScript.TimeElapsed >= doublingTime && !increasedSpawnRates)
		{
			squareSpawnRate /= 2f;
			triangleSpawnRate /= 2f;
			hexagonSpawnRate /= 2f;
			increasedSpawnRates = true;
		}
	}
}
