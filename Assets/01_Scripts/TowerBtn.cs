using UnityEngine;
using UnityEngine.UI;

public class TowerBtn : MonoBehaviour {

	[SerializeField] private Tower towerObject;
	[SerializeField] private Sprite dragSprite;
	[SerializeField] private Text towerPriceLabel; 
	[SerializeField] private int towerPrice;


	void Start(){
	towerPriceLabel = GetComponentInChildren<Text>();
		towerPriceLabel.text = TowerPrice.ToString();
	}

	public Tower TowerObject {
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
