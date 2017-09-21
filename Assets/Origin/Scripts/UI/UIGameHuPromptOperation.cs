using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using odao.scmahjong;
using MP;

public partial class UIOperation : SingletonBehaviour<UIOperation> {

    public struct HUPROMPT_INFO
    {
        public int mjType;//胡牌类型
        public int mjView;//1-9
        public int beishu;//胡牌倍数
        public int zhangshu;//胡牌张数
        public void clear()
        {
            mjType = -1;
            mjView = -1;
            beishu = -1;
            zhangshu = -1;
        }
    }

    public const int HUPROMPT_MJTYPE_NUM = 10;//最多胡9张
	public List<GameMessage.AllTips> _huTipsCards;

	public void Reset()
	{
		_huTipsCards = null;
	}

    


}
