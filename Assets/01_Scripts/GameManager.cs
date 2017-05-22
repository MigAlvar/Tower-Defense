using UnityEngine;
using System.Collections;

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

	private int enemiesOnScreen = 0;

	const float spawnTime = 0.5f;

	void Awake ()
	{
		
	}
	// Use this for initialization
	void Start () {
		StartCoroutine(Spawn());
	}

	void spawnEnemies ()
	{
		
	}

		IEnumerator Spawn ()
	{
		if (enemiesPerSpawn > 0 && enemiesOnScreen < totalEnemies) {
			for (int i = 0; i < enemiesPerSpawn; i++) {
				if (enemiesOnScreen < maxEnemiesOnScreen) {
					GameObject newEnemy = Instantiate(enemies[Random.Range(0,2)]) as GameObject;
					newEnemy.transform.position = spawnPoint.transform.position;
					enemiesOnScreen += 1;
				}
			}
			yield return new WaitForSeconds(spawnTime);
			StartCoroutine(Spawn());
		}
	
	}

	public void removeEnemiesOnScreen(){
		if(enemiesOnScreen > 0){
			enemiesOnScreen -= 1;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
