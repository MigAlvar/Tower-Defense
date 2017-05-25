using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TowerManager : Singleton<TowerManager> {

	public TowerBtn towerBtnPressed{ get; set; }

	private SpriteRenderer spriteRenderer;
	private List <Tower> TowerList = new List<Tower>();
	private List <Collider2D> BuildList = new List <Collider2D>();
	private Collider2D buildtile;


	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		buildtile = GetComponent<Collider2D>();
		spriteRenderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

			if(hit.collider.tag == "buildSites"){
				buildtile = hit.collider;
				hit.collider.tag = "BuildSiteFull";
				RegisterBuildSite(buildtile);
				placeTower(hit);
			}
		}
		if(spriteRenderer.enabled){
				transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				transform.position = new Vector2 (transform.position.x,transform.position.y);
			}
	}

	public void RegisterBuildSite (Collider2D buidTag)
	{
		BuildList.Add(buildtile);
	}

	public void RegisterTower (Tower tower)
	{
		TowerList.Add(tower);
	}

	public void RenameTagsBuildSites ()
	{
		foreach (Collider2D buidTag in BuildList) {
			buildtile.tag = "buildSites";
		}
		BuildList.Clear();
	}

	public void DestroyAllTower ()
	{
		foreach (Tower tower in TowerList) {
			Destroy(tower.gameObject);
		}
		TowerList.Clear();
	}

	public void placeTower (RaycastHit2D hit)
	{
		if(!EventSystem.current.IsPointerOverGameObject() && towerBtnPressed != null){
			Tower newTower = Instantiate(towerBtnPressed.TowerObject);
			newTower.transform.position = hit.transform.position;
			buyTower(towerBtnPressed.TowerPrice);
			RegisterTower(newTower);
			disableSprite();
		}
	}

	public void selectedTower (TowerBtn towerselected)
	{
		if (towerselected.TowerPrice >= GameManager.Instance.Funds) {
			towerBtnPressed = towerselected;
			enableSprite (towerBtnPressed.DragSprite);
		}
	}

	public void enableSprite(Sprite sprite){
		spriteRenderer.sprite = sprite;
		spriteRenderer.enabled = true;
	}

	public void disableSprite(){
		spriteRenderer.enabled = false;
	}

	public void buyTower (int price)
	{
		GameManager.Instance.ChangeFunds(price,true);
	}
}
