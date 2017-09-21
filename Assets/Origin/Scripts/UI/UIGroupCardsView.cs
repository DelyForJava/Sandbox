using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MP;

public class UIGroupCardsView : MonoBehaviour {
	public UICardView[] _cardViews;

	void Awake(){
		_cardViews = new UICardView[GameMessage.HANDLE_MJ_NUM];
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public class UICardView : MonoBehaviour {
	public Image _imgBg;
	public Image _imgMj;
	public Image _imgbsBG;
	public Text _textX;
	public Text _textNum;
}
