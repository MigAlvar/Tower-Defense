using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	
	[SerializeField] private Transform exitPoint;
	[SerializeField] private Transform[] waypoints;
	[SerializeField] private float navigationUpdate;
	[SerializeField] private int rewardAmount;

	private int target =0;
	private Transform enemy;
	private float navigationTime = 0;
	private int healthpoints;
	private Collider2D enemyCollider;
	private Animator anim;
	private bool isDead = false;


	public bool IsDead {
		get {
			return isDead;	
		}
	}

	// Use this for initialization
	void Start () {
		enemy = GetComponent<Transform>();
		enemyCollider = GetComponent<BoxCollider2D>();
		//GameManager.Instance.RegisterEnemy();
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (waypoints != null && isDead != true) {
			navigationTime += Time.deltaTime;
			if (navigationTime > navigationUpdate) {
				if (target < waypoints.Length) {
					enemy.position = Vector2.MoveTowards (enemy.position, waypoints[target].position, navigationTime);
				} else {
					enemy.position = Vector2.MoveTowards(enemy.position,exitPoint.position,navigationTime);
				}
				navigationTime = 0;
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		Projectile newP = other.gameObject.GetComponent<Projectile> ();

		if (other.tag == "Checkpoint") {
			target++;
		} else if (other.tag == "Finish") {
			GameManager.Instance.UnregisterEnemy (this);
		}
		if (newP) {
			EnemyHit(newP.AttackStrength);
		}
	}

	public void EnemyHit (int hitStrength)
	{
		healthpoints += hitStrength;
		anim.SetTrigger("isHurt");

		if (healthpoints <= 0){
			anim.SetTrigger("isDead");
			Death();
		}
	}

	public void Death ()
	{
		isDead = true;
		enemyCollider.enabled = false;
		//Destroy (this.gameObject);
	}


}
