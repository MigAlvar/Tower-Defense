using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum gameStatus{
	next, play, gameOver, win
}

public class GameManager : Singleton<GameManager> {
	[SerializeField] private GameObject spawnPoint;
	[SerializeField] private Enemy[] enemies;
	[SerializeField] private int totalEnemies;
	[SerializeField] private int enemiesPerSpawn;

	private int wv,fnd,exwv,ttlExc, kl, waveNumber, totalWaves,enemiesToSpawn;

	[SerializeField] private Text waves;
	[SerializeField] private Text funds;
	[SerializeField] private Text escaped;
	[SerializeField] private gameStatus currentState = gameStatus.play;
	[SerializeField] private Text playBtn;
	[SerializeField] private Button pBtn;

	private AudioSource audioSource;
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

	public int TotalEscaped {
		get {
			return ttlExc;
		}
		set {
			ttlExc = value;
		}
	}

	public int WaveEscape {
		get {
			return exwv;
		}
		set {
			exwv = value;
		}
	}

	public int TotaKilled {
		get {
			return kl;
		}
		set {
			kl = value;
		}
	}

	const float spawnTime = 0.5f;

	public AudioSource AudibleSource {
		get {
			return audioSource;
		}
	}

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
				if (EnemyList.Count < totalEnemies) {
					Enemy newEnemy = Instantiate(enemies[Random.Range(0,enemiesToSpawn)]) as Enemy;
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

	public void ChangeFunds (int amount, bool expense)
	{	
		if(!expense)
			fnd += amount;
		else
			fnd-= amount;
	}

	public void showMenu () {
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

	public void PlayBtnPressed ()
	{
		switch (currentState) {
			case gameStatus.next:
				waveNumber += 1;
				totalEnemies += waveNumber;
				break;
			default:
				totalEnemies = 3;
				TotalEscaped = 0;
				totalEnemies = 10;
				enemiesToSpawn=0;
				funds.text = fnd.ToString();
				escaped.text = "Escaped " + TotalEscaped + "/" + totalEnemies;
				AudibleSource.PlayOneShot(SoundManager.Instance.LevelMusic[0]);
				break;
		}
		DestroyAllEnemies();
		TotaKilled = 0;
		WaveEscape = 0;
		waves.text = "Wave" + (wv + 1);

		StartCoroutine(Spawn());
		playBtn.gameObject.SetActive(false);
	}

	public void isWaveOver ()
	{
		escaped.text = "Escaped " + ttlExc + "/" + totalEnemies; 
		if (WaveEscape + TotalEscaped == totalEnemies) {
			if (waveNumber <= enemies.Length) {
				enemiesToSpawn = waveNumber;
			}

			setCurrentGameState ();
			showMenu();
		} 

	}

	public void setCurrentGameState ()
	{
		if (TotalEscaped >= 10) {
			currentState = gameStatus.gameOver;
		}else if(wv == 0 && (TotaKilled + WaveEscape) == 0){
			currentState = gameStatus.play;
		}else if (waveNumber >= totalWaves) {
			currentState = gameStatus.win;
		} else {
			currentState = gameStatus.next;
		}
	}

	public void handleEscape(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			TowerManager.Instance.disableSprite();
			TowerManager.Instance.towerBtnPressed = null;
		}
	}
}
