using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MP;

public class UIGameResultView : MonoBehaviour {
	public Button _btnXuanyao;
	public Button _btnContinue;
	public Transform _scrollDetail;//结算详细界面

	public UIResultItem[] _resultItems;
	public UIDetailItem[] _dtailtems;
	void Awake(){
		_resultItems = new UIResultItem[GameMessage.TABLE_PLAYER_NUM];
		_dtailtems = new UIDetailItem[20];
	}
}
public class UIResultItem : MonoBehaviour {
	public Button _btnResult;

	public Transform _imgBgWin;
	public Transform _imgBgLose;
	public Text _textscore;
	public Transform _handCards;
	public Image _imgHu;
	public Text _textHubs;
	public Transform _huGroup;
	public Image _imgWeiting;
	public Image _imgHuazhu;

	//头像信息
	public Image _imgHeadkuang;
	public Image _imgHeadIcon;
	public Image _imgBenjia;
	public Image _imgMaxWin;
	public Text _textName;

}

public class UIDetailItem : MonoBehaviour {

	public Text _textWinType;//放炮，被放炮类型（点炮，自摸。。。。）
	public Text _textHuType;//胡牌的类型（平胡，清一色，对对胡。。。）
	public Text _textHuBS;//胡几倍
	public Text _textWinScore;//输赢多少钱
	public Text _textWinPenson;//赢了那几个玩家（三家，上家，对家，下家）

}
