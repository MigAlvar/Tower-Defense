using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public GameObject spawnPoint;
	public GameObject[] enemies;
	public int maxEnemiesOnScreen;
	public int totalEnemies;
	public int enemiesPerSpawn;

	private int enemiesOnScreen = 0;

	void Awake ()
	{
		if (instance = null) {
			instance = this;
		} else if(instance != null){
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}
	// Use this for initialization
	void Start () {
		spawnEnemies();
	}

	void spawnEnemies ()
	{
		if (enemiesPerSpawn > 0 && enemiesOnScreen < totalEnemies) {
			for (int i = 0; i < enemiesPerSpawn; i++) {
				if (enemiesOnScreen < maxEnemiesOnScreen) {
					GameObject newEnemy = Instantiate(enemies[Random.Range(0,2)]) as GameObject;
					newEnemy.transform.position = spawnPoint.transform.position;
					enemiesOnScreen += 1;
				}
			}
		}
	}



	// Update is called once per frame
	void Update () {
	
	}
}
