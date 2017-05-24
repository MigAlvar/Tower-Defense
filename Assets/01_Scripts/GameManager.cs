using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum gameStatus{
	next, play, gameOver, win
}

public class GameManager : Singleton<GameManager> {
	[SerializeField] private GameObject spawnPoint;
	[SerializeField] private GameObject[] enemies;
	[SerializeField] private int maxEnemiesOnScreen;
	[SerializeField] private int totalEnemies;
	[SerializeField] private int enemiesPerSpawn;

	private int wv,fnd,exwv,ttlExc, kl;

	[SerializeField] private Text waves;
	[SerializeField] private Text funds;
	[SerializeField] private Text escaped;
	[SerializeField] private gameStatus currentState = gameStatus.play;
	[SerializeField] private Text playBtn;
	[SerializeField] private Button pBtn;

	public List<Enemy> EnemyList = new List<Enemy>();

	public int Funds{
		get{
			return fnd;
		}
		set{
			fnd = value;
			funds.text = fnd.ToString();
		}
	}

	const float spawnTime = 0.5f;

	void Awake ()
	{
		
	}
	// Use this for initialization
	void Start () {
		pBtn.gameObject.SetActive(false);
		StartCoroutine(Spawn());
		showMenu();
	}

	void Update(){
		handleEscape();
	}


		IEnumerator Spawn ()
	{
		if (enemiesPerSpawn > 0 && EnemyList.Count < totalEnemies) {
			for (int i = 0; i < enemiesPerSpawn; i++) {
				if (EnemyList.Count < maxEnemiesOnScreen) {
					GameObject newEnemy = Instantiate(enemies[Random.Range(0,2)]) as GameObject;
					newEnemy.transform.position = spawnPoint.transform.position;
					RegisterEnemy(newEnemy.GetComponent<Enemy>());
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

	public void addFunds (int amount, bool expense)
	{	
		if(!expense)
			fnd += amount;
		else
			fnd-= amount;
	}

	void showMenu () {
		switch(currentState){
			case gameStatus.gameOver:
				playBtn.text = "PLAY AGAIN";
				break;
			case gameStatus.next:
				playBtn.text = "NEXT WAVE";
				break;
			case gameStatus.play:
				playBtn.text = "PLAY";
				break;
			case gameStatus.win:
			playBtn.text = "PLAY";
				break;		
		}

		pBtn.gameObject.SetActive(true);
	}

	public void handleEscape(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			TowerManager.Instance.disableSprite();
			TowerManager.Instance.towerBtnPressed = null;
		}
	}
}
