using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIOperation : SingletonBehaviour<UIOperation> {

    /// <summary>
    /// 游戏内玩家状态
    /// </summary>
    public enum gameState
    {
        DINGQUE = 0,      //定缺状态
        XUANPAI = 1,      //选牌状态
        DAPAI = 2,      //打牌状态
        NONE = 10,        //初始化状态
    }
	public void OnClickDQCrak(UIController ctrl)
	{
		GameClient.Instance.MahjongGamePlayer.SetLackTileKind (odao.scmahjong.TileDef.Kind.CRAK);
		ctrl.Close ();
	}

	public void OnClickDQBam(UIController ctrl)
	{
		GameClient.Instance.MahjongGamePlayer.SetLackTileKind (odao.scmahjong.TileDef.Kind.BAM);
		ctrl.Close ();
	}

	public void OnClickDQDot(UIController ctrl)
	{
		GameClient.Instance.MahjongGamePlayer.SetLackTileKind (odao.scmahjong.TileDef.Kind.DOT);
		ctrl.Close ();
	}
}
