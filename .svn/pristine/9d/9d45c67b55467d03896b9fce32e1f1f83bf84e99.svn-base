using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
using UnityEngine;
#endif
using NetworkInterface;
using MP;

namespace odao.scmahjong
{
    // first step exchange
    // second step confirm lacktion
    public partial class NetworkPlayer : Player
	{		
		public PlayerInfo Info { get; set; }
        OdaoClient _gsProxy;

        protected NetworkPlayer()
        {
            _gsProxy = new OdaoClient();
			Info = new PlayerInfo ();
        }

        protected override void init()
        {
            base.init();
        }

		public static NetworkPlayer Create()
        {
            var player = new NetworkPlayer();
            player.init();
            return player;
        }

		//MOUSE.CLICK/HANDLER
		public int AddEventListener(Action<NetWorkState> callback)
		{
			_gsProxy.NetWorkStateChangedEvent += callback;
			return 0;
		}

        public void ConnectGameServer(string host, int port, Action callback)
        {
            _gsProxy.initClient(host, port, () => {
                _gsProxy.connect();
				InitBaseMessage(_gsProxy);
                InitGameMessage(_gsProxy);
                callback();
            });
        }

		public bool IsSpecialType(byte specialType, byte value)
		{
			return ((byte)(specialType & value)) == value;
		}

		public TileDef.ComboType ToComboType(byte specialType)
		{
			if(IsSpecialType(specialType, (byte)GameMessage.SPECIAL_TYPE.PASS))
			{
				return TileDef.ComboType.PASS;
			}

			if(IsSpecialType(specialType,(byte)GameMessage.SPECIAL_TYPE.PONG))
			{
				return TileDef.ComboType.PONG;
			}

			if(IsSpecialType(specialType,(byte)GameMessage.SPECIAL_TYPE.KONG))
			{
				return TileDef.ComboType.KONG;
			}

			if(IsSpecialType(specialType,(byte)GameMessage.SPECIAL_TYPE.DARK_KONG))
			{
				return TileDef.ComboType.KONG_DARK;
			}

			if(IsSpecialType(specialType,(byte)GameMessage.SPECIAL_TYPE.SPECIAL_TYPE_PENGGANG))
			{
				return TileDef.ComboType.KONG_TURN;
			}

			if(IsSpecialType(specialType,(byte)GameMessage.SPECIAL_TYPE.WIN))
			{
				return TileDef.ComboType.WIN;
			}

			Debug.LogError ("ToComboType Error");

			return TileDef.ComboType.PASS_CANCEL;
		}
	}
}
