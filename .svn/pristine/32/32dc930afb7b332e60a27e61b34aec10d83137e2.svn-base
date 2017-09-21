using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MP;
using odao.scmahjong;

//public class UIOperation : SingletonBehaviour<UIOperation>
public partial class UIOperation : SingletonBehaviour<UIOperation> {
    public struct HEAD_DATA
    {
        public bool isShow;         //是否显示头像
        public int headIndex;       //头像icon    
        public long money;          //携带积分
        public string name;         //玩家昵称
        public bool isZhuang;       //是否是庄家
        public odao.scmahjong.TileDef.Kind dingque;     //定缺类型（万、条、筒）
        public UIOperation.gameState gameState;  //游戏内状态（定缺中、选牌中）
        public void clear()
        {
            this.isShow = true;
            this.headIndex = 0;
            this.money = 0;
            this.name = "";
            this.isZhuang = false;
            this.dingque = odao.scmahjong.TileDef.Kind.NONE;
            this.gameState = UIOperation.gameState.NONE;
        }
    };
    public HEAD_DATA[] _headDatas = new HEAD_DATA[GameMessage.TABLE_PLAYER_NUM];

	//存储换出去的牌
	public TileDef[] _HuanPaiOldDef = new TileDef[GameMessage.MAXHUANPAINUM];
	//存储新换的牌
	public TileDef[] _HuanPaiNewDef = new TileDef[GameMessage.MAXHUANPAINUM];
	//换牌类型
	public GameMessage.HUANPAI_TYPE _HuanPaiType;

    public void OnClickMenu(UIController ctrl)
	{//菜单按钮
        Debug.Log("OnClickMenu");
        UIGameSetingController seting = (UIGameSetingController)ctrl;
        seting.showMenuBtn();
	}
    public void OnClickHelp(UIController ctrl)
    {//帮助按钮
        Debug.Log("OnClickHelp");
        UIGameSetingController seting = (UIGameSetingController)ctrl;
        
        
    }
    public void OnClickSeting(UIController ctrl)
    {//设置按钮
        Debug.Log("OnClickSeting");
        UIGameSetingController seting = (UIGameSetingController)ctrl;
       
       
    }
    public void OnClickExit(UIController ctrl)
    {//退出按钮
        Debug.Log("OnClickExit");
		//GameClient.Instance.MG.Self.Proxy.LeaveTable ();
		UITipsController.Instance.OpenTips ("您正在游戏中，确定要退出？", delegate {
			OutGame();
		}, delegate {
			
		});
       
    }

    /// <summary>
    /// 点击头像
    /// </summary>
    /// <param name="index">0：自己， 1：左， 2：上，3：右</param>
    /// <param name="ctrl"></param>
    public void OnClickHead(int index, UIController ctrl)
    {
        Debug.Log("OnClickHead index:"+index);
        UIGameSetingController seting = (UIGameSetingController)ctrl;
        switch(index){
            case 0:
                OnClickHeadSelf(ctrl);
                break;
            case 1:
                OnClickHeadLeft(ctrl);
                break;
            case 2:
                OnClickHeadUp(ctrl);
                break;
            case 3:
                OnClickHeadRight(ctrl);
                break;
        }
    }
    public void OnClickHeadSelf(UIController ctrl)
    {//点击自己头像
        Debug.Log("OnClickHeadSelf");
        UIGameSetingController seting = (UIGameSetingController)ctrl;
    }
    public void OnClickHeadLeft(UIController ctrl)
    {//点击左侧玩家头像
        Debug.Log("OnClickHeadLeft");
        UIGameSetingController seting = (UIGameSetingController)ctrl;
    }
    public void OnClickHeadUp(UIController ctrl)
    {//点击上面玩家头像
        Debug.Log("OnClickHeadUp");
        UIGameSetingController seting = (UIGameSetingController)ctrl;
    }
    public void OnClickHeadRight(UIController ctrl)
    {//点击右侧玩家头像
        Debug.Log("OnClickHeadRight");
        UIGameSetingController seting = (UIGameSetingController)ctrl;
    }

	public void OnClickHuanpai(UIController ctrl)
	{
		MahjongTile.TotalClickCount = 1;
		UIGameSetingController seting = (UIGameSetingController)ctrl;
		seting.CloseHuanpaiTips ();

		List<byte> cards = new List<byte> ();


		MahjongUserPlayer userPlayer = GameClient.Instance.MG.Self as MahjongUserPlayer;
		bool checkSelect = true;
		if (userPlayer != null) 
		{
			List<MahjongTile> mahjongTiles = userPlayer.Clicked;
			if (mahjongTiles.Count > 0) 
			{
				TileDef.Kind kind = mahjongTiles [0].Def.GetKind();
				foreach(var temp in mahjongTiles)
				{
					if(kind != temp.Def.GetKind()){
						checkSelect = false;
					}
					cards.Add (temp.Def.Value);
				}
			}

		}

		if (cards.Count == 0 || !checkSelect) {
			foreach (var temp in _HuanPaiOldDef) {
				cards.Add (temp.Value);
			}
		} else {
			for(int i = 0; i < GameMessage.MAXHUANPAINUM; i++)
			{
				if (i < cards.Count)
					_HuanPaiOldDef [i] = TileDef.Create (cards[i]);
			}
		}

		GameClient.Instance.MG.Self.Proxy.Huanpai (cards);

	}

	private void OutGame()
	{
		GameClient.Instance.MG.Reset ();
		Reset ();

		UIGameSetingController.Instance.Close();
		UIGameResultController.Instance.Close();
		GameLoading.SwitchScene(2);
	}

}
