﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> {
	[SerializeField]
	private GameObject spawnPoint;
	[SerializeField]
	private GameObject[] enemies;
	[SerializeField]
	private int maxEnemiesOnScreen;
	[SerializeField]
	private int totalEnemies;
	[SerializeField]
	private int enemiesPerSpawn;

	public List<Enemy> EnemyList = new List<Enemy>();

	const float spawnTime = 0.5f;

	void Awake ()
	{
		
	}
	// Use this for initialization
	void Start () {
		StartCoroutine(Spawn());
	}

		IEnumerator Spawn ()
	{
		if (enemiesPerSpawn > 0 && EnemyList.Count < totalEnemies) {
			for (int i = 0; i < enemiesPerSpawn; i++) {
				if (EnemyList.Count < maxEnemiesOnScreen) {
					GameObject newEnemy = Instantiate(enemies[Random.Range(0,2)]) as GameObject;
					newEnemy.transform.position = spawnPoint.transform.position;
				}
			}
			yield return new WaitForSeconds(spawnTime);
			StartCoroutine(Spawn());
		}
	
	}

	public void RegisterEnemy (Enemy enemy)
	{
		EnemyList.Add(enemy);
	}

	public void UnregisterEnemy (Enemy enemy)
	{
		EnemyList.Remove(enemy);
		Destroy(enemy.gameObject);
	}

	public void DestroyAllEnemies ()
	{
		foreach(Enemy enemy in EnemyList){
			Destroy(enemy.gameObject);
		}
		EnemyList.Clear();
	}
	// Update is called once per frame
	void Update () {
	
	}
}
