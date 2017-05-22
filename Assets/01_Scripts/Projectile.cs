using UnityEngine;

public enum proType{
	rock, arrow, fireball
};

public class Projectile : MonoBehaviour {


	[SerializeField] private int attackStrength;
	[SerializeField] private proType projectileType;


	public int AttackStrength{
		get {
			return attackStrength;
		}
	}

	public proType ProjectileType {
		get {
			return projectileType;
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
