using UnityEngine;
using UnityEngine.UI;

public class TowerBtn : MonoBehaviour {

	[SerializeField] private GameObject towerObject;
	[SerializeField] private Sprite dragSprite;
	[SerializeField] private Text towerPriceLabel; 
	[SerializeField] private int towerPrice;


	void Start(){
		towerPriceLabel.text = towerPrice.ToString();
	}

	public GameObject TowerObject {
		get {
			return towerObject;
		}
	}

	public Sprite DragSprite{
		get{
			return dragSprite;
		}
	}

	public int TowerPrice{
		get {
			return towerPrice;
		}
	}
}
