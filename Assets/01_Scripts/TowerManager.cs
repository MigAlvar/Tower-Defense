using UnityEngine;
using UnityEngine.EventSystems;

public class TowerManager : Singleton<TowerManager> {

	public TowerBtn towerBtnPressed{ get; set; }

	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

			if(hit.collider.tag == "buildSites"){
				hit.collider.tag = "BuildSiteFull";
				placeTower(hit);
			}
		}
		if(spriteRenderer.enabled){
				transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				transform.position = new Vector2 (transform.position.x,transform.position.y);
			}
	}

	public void placeTower (RaycastHit2D hit)
	{
		if(!EventSystem.current.IsPointerOverGameObject() && towerBtnPressed != null){
			GameObject newTower = Instantiate(towerBtnPressed.TowerObject);
			newTower.transform.position = hit.transform.position;
			disableSprite();
		}
	}

	public void selectedTower (TowerBtn towerselected)
	{
		towerBtnPressed = towerselected;
		enableSprite(towerBtnPressed.DragSprite);
	}

	public void enableSprite(Sprite sprite){
		spriteRenderer.sprite = sprite;
		spriteRenderer.enabled = true;
	}

	public void disableSprite(){
		spriteRenderer.enabled = false;
	}
}
