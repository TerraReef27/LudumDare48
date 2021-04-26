using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalvageGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] crates;
	[SerializeField] private int numberToSpawn;
	[SerializeField] private GameObject lootHolder;
	[SerializeField] private Terrain terrain;
	[SerializeField] Vector2 min;
	[SerializeField] Vector2 max;

	private GameManager gameManager;

	void Awake()
	{
		gameManager = FindObjectOfType<GameManager>();
		gameManager.OnReset += GameManager_OnReset;
	}
	void Start()
	{
		SpawnCrates();
	}
    public void SpawnCrates()
	{
		DestroyCrates();
		
		int spawned = 0;
		while(spawned < numberToSpawn)
		{
			float xpos = Random.Range(min.x, max.x);
			float zpos = Random.Range(min.y, max.y);
			Vector3 spawnPos = new Vector3(xpos, 0,zpos);
			float ypos = terrain.SampleHeight(spawnPos) - 500f + .5f;

			spawnPos = new Vector3(xpos, ypos, zpos);

			
			float rand = Random.Range(0, 100) - spawnPos.y/2;
			int type = 0;
			if(rand > 95)
				type = 2;
			else if(rand > 60)
				type = 1;
			else
				type = 0;


			Instantiate(crates[type], spawnPos, Quaternion.identity, lootHolder.transform);
			spawned++;
		}
	}

	private void DestroyCrates()
	{
		foreach(Transform child in lootHolder.transform)
		{
			GameObject.Destroy(child.gameObject);
		}
	}
	private void GameManager_OnReset()
	{
		SpawnCrates();
	}
}
