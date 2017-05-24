using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour {

	[SerializeField] private float timeBetweenAttacks;
	[SerializeField] private float attackRadius;

	[SerializeField] private Projectile projectile;
	private Enemy targetEnemy= null;

	private float attackCounter;
	private bool isAttacking = false;




	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		attackCounter -= Time.deltaTime;
		Enemy nearestEnemy = GetNearestEnemyInRange ();

		if (targetEnemy == null || targetEnemy.IsDead) {
			if (nearestEnemy != null && Vector2.Distance (transform.position, nearestEnemy.transform.position) <= attackRadius) {
				targetEnemy = nearestEnemy;
			}
		} else {
			if (attackCounter <= 0) {
				isAttacking = true;
				attackCounter = timeBetweenAttacks;
			} else {
				isAttacking = false;
			}
			if(Vector2.Distance(transform.position, nearestEnemy.transform.position) > attackRadius){
			targetEnemy = null;
		}
		}

	}

	void FixedUpdate ()
	{
		if (isAttacking) {
			attack ();
		}
	}

	public void attack ()
	{
		isAttacking = false;
		Projectile newProjectile = Instantiate (projectile) as Projectile;
		newProjectile.transform.position = transform.position;
		if (targetEnemy = null) {
			Destroy (newProjectile);
		} else {
			StartCoroutine(moveProjectile(newProjectile));
		}
	}

	IEnumerator moveProjectile (Projectile projectile)
	{
		while(GetTargetDistance(targetEnemy)>.20f && projectile != null && targetEnemy != null){
			var direction = targetEnemy.transform.position - transform.position;
			var angleDirection = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
			projectile.transform.rotation = Quaternion.AngleAxis(angleDirection,Vector3.forward);
			projectile.transform.localPosition = Vector2.MoveTowards(projectile.transform.localPosition,targetEnemy.transform.localPosition, 5f * Time.deltaTime);
			yield return null;
		}
		if(projectile != null || targetEnemy == null){
			Destroy(projectile);
		}
	}

	private float GetTargetDistance(Enemy thisEnemy){
		if(thisEnemy == null){
			thisEnemy = GetNearestEnemyInRange();

			if(thisEnemy == null){
				return 0f;
			}
		}
		return Mathf.Abs(Vector2.Distance(transform.position,thisEnemy.transform.position));
	}

	private List<Enemy> GetEnemiesInRange ()
	{
		List<Enemy> enemiesInRange = new List<Enemy>();
		foreach(Enemy enemy in GameManager.Instance.EnemyList){
			if(Vector2.Distance(transform.position, enemy.transform.position) <= attackRadius){
				enemiesInRange.Add(enemy);
			}
		}
		return enemiesInRange;
	}

	private Enemy GetNearestEnemyInRange ()
	{
		Enemy nearestEnemy = null;
		float smallestDistance = float.PositiveInfinity;
		foreach (Enemy enemy in GetEnemiesInRange()) {
			if (Vector2.Distance (transform.position, enemy.transform.position) < smallestDistance) {
				smallestDistance = Vector2.Distance(transform.position, enemy.transform.position);
				nearestEnemy = enemy;
			}
		}
		return nearestEnemy;
	}
}
