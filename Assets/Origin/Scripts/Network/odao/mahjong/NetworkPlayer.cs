#define USE_MSGPACK3
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkInterface;
using MsgPack;
using MsgPack.Serialization;
using MP;
using UnityEngine;
using System.IO;

namespace odao.scmahjong
{    
	// first step exchange
	// second step confirm lacktion
    public partial class NetworkPlayer : Player
    {
        /// <summary>
        /// 登录服务器
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="token"></param>
        /// <param name="machineId"></param>
        public void OriginMsgLoginReqDef(int accountId, string token, string machineId,Source source)
        {
            BaseMessage.LoginReqDef LoginReqDef = new BaseMessage.LoginReqDef();
            LoginReqDef.accountId = accountId;
            int sourceId = 0;
            switch (source)
            {
                case Source.yk:
                    sourceId = 1;
                    break;
                case Source.wechat:
                    sourceId = 2;
                    break;
                case Source.phoneNumber:
                    sourceId = 3;
                    break;
                default:
                    break;
            }
            LoginReqDef.serviceId = 0;
            LoginReqDef.sourceId = sourceId;
            LoginReqDef.token = token;//"2e4f847b-558"
            LoginReqDef.machine = machineId;
            //Info.UserId = userid;
            var serializer = MessagePackSerializer.Get<BaseMessage.LoginReqDef>();
            byte[] msg = serializer.PackSingleObject(LoginReqDef);
            _gsProxy.notifyMP(BaseMessage.LOGIN_REQ_MSG, msg);
        }

        /// <summary>
        /// 购买金币
        /// </summary>
        /// <param name="sShopID"></param>
        /// <param name="sItemIndex"></param>
        public void BuyGoldReqDef(short sShopID, short sItemIndex)
        {
            BaseMessage.BuyGoldReqDef BuyGoldReq = new BaseMessage.BuyGoldReqDef();
            BuyGoldReq.sShopID = sShopID;
            BuyGoldReq.sItemIndex = sItemIndex;
            var serializer = MessagePackSerializer.Get<BaseMessage.BuyGoldReqDef>();
            byte[] msg = serializer.PackSingleObject(BuyGoldReq);
            _gsProxy.notifyMP(BaseMessage.LOBBY_SHOP_EXCHANGE_COIN_REQ_MSG, msg);
            Debug.Log("OnClickBuyGold Success:  sShopID:" + sShopID+ "     sItemIndex:" + sItemIndex);
        }

        /// <summary>
        /// 改名
        /// </summary>
        /// <param name="szNickName"></param>
        public void ChangeNameReqDef(string szNickName)
        {
            BaseMessage.ChangeNameReqDef ChangeNameReq = new BaseMessage.ChangeNameReqDef();
            ChangeNameReq.szNickName= szNickName;
            var serializer = MessagePackSerializer.Get<BaseMessage.ChangeNameReqDef>();
            byte[] msg = serializer.PackSingleObject(ChangeNameReq);
            _gsProxy.notifyMP(BaseMessage.LOBBY_MODIFY_NICK_NAME_REQ_MSG, msg);
            Debug.Log("OnClickChangeNameReqDef Success:  szNickName:" + szNickName);
        }

        /// <summary>
        /// 改性别
        /// </summary>
        /// <param name="cGender"></param>
        public void ChangeGenderReqDef(int cGender)
        {
            BaseMessage.ChangeGenderReqDef ChangeGenderReq = new BaseMessage.ChangeGenderReqDef();
            ChangeGenderReq.cGender = Convert.ToSByte(cGender);
            var serializer = MessagePackSerializer.Get<BaseMessage.ChangeGenderReqDef>();
            byte[] msg = serializer.PackSingleObject(ChangeGenderReq);
            _gsProxy.notifyMP(BaseMessage.LOBBY_MODIFY_GENDER_REQ_MSG, msg);
            Debug.Log("OnClickChangeGenderReqDef Success:  cGender:" + cGender);
        }

        public void PackageInfoReqDef(int iUserID)
        {
            BaseMessage.PackageInfoReqDef PackageInfoReq = new BaseMessage.PackageInfoReqDef();
            PackageInfoReq.iUserID = iUserID;
            var serializer = MessagePackSerializer.Get<BaseMessage.PackageInfoReqDef>();
            byte[] msg = serializer.PackSingleObject(PackageInfoReq);
            _gsProxy.notifyMP(BaseMessage.LOBBY_PACKAGE_INFO_REQ_MSG, msg);
            Debug.Log("OnClick"+"PackageInfoReqDef    Success:  iUserID:" + iUserID);
        }

        public void PackageAddItemReqDef(int iItemID, int iAddNum)
        {
            BaseMessage.PackageAddItemReqDef PackageAddItemReq = new BaseMessage.PackageAddItemReqDef();
            PackageAddItemReq.iItemID = iItemID;
            PackageAddItemReq.iAddNum = iAddNum;
            var serializer = MessagePackSerializer.Get<BaseMessage.PackageAddItemReqDef>();
            byte[] msg = serializer.PackSingleObject(PackageAddItemReq);
            _gsProxy.notifyMP(BaseMessage.LOBBY_PACKAGE_ADD_ITEM_REQ_MSG, msg);
            Debug.Log("OnClick" + "PackageAddItemReqDef    Success:  iItemID:" + iItemID);
            Debug.Log("OnClick" + "PackageAddItemReqDef    Success:  iAddNum:" + iAddNum);
        }



        #region 历史




        public void StartAuth(int userid)
        {
            GameMessage.SendCardReqDef data = new GameMessage.SendCardReqDef();
            data.cCard = 9;
            byte[] arr = XConvert.ConvertToByte(data);

            BaseMessage.TestConnect t = new BaseMessage.TestConnect();
            t.b1 = 128;
            byte[] tarr = XConvert.ConvertToByte(t, 0);

            BaseMessage.AuthenReqDef authenReq = new BaseMessage.AuthenReqDef();
            authenReq.iUserID = userid;
            authenReq.iRoomID = 1;
            authenReq.cLoginType = BaseMessage.LoginType.USER_LOGIN_THREE;
            authenReq.szPasswd = STRMD5.MD5Num("000000");

			Info.UserId = userid;

            //byte[] msg = XConvert.ToByte(authenReq);
            byte[] msg = XConvert.ConvertToByte(authenReq);
			_gsProxy.notify(BaseMessage.AUTHEN_REQ_MSG, msg);
        }

        /// <summary>
        /// 原始发包功能，测试成功
        /// </summary>
        /// <param name="userid"></param>
        public void NoMsgLoginReqDef(int userid)
        {
            BaseMessage.LoginReqDef LoginReqDef = new BaseMessage.LoginReqDef();
            LoginReqDef.accountId = 10000;
            LoginReqDef.serviceId = 1;
            LoginReqDef.sourceId = 1111;
            LoginReqDef.token = "0123456789ABCDE";
            LoginReqDef.machine = "ABCDEFGH";


            Info.UserId = userid;

            byte[] msg = XConvert.ConvertToByte(LoginReqDef);
            _gsProxy.notify(BaseMessage.LOGIN_REQ_MSG, msg);
        }

        public void StartAuth2()
        {
            Debug.Log("*************StartAuth2");
            BaseMessage.LobbyLoginReq authenReq = new BaseMessage.LobbyLoginReq();
            authenReq.iAccountID = 10000;
            authenReq.iServiceID = 20001;
            authenReq.strToken = "jjidikdopakdopakdosapkdasdada231a";
            var serializer = MsgPack.Serialization.MessagePackSerializer.Get<BaseMessage.LobbyLoginReq>();
            byte[] msg = serializer.PackSingleObject(authenReq);

            _gsProxy.notifyMP(BaseMessage.READY_REQ_MSG, msg);
        }


        /// <summary>
        /// msgPack发包
        /// </summary>
        /// <param name="userid"></param>
        public void LoginReqDef(int userid)
        {
            BaseMessage.LoginReqDef LoginReqDef = new BaseMessage.LoginReqDef();
            LoginReqDef.accountId = 10000;
            LoginReqDef.serviceId = 1;
            LoginReqDef.sourceId = 1;
            LoginReqDef.token = "0123456789ABCDE";
            LoginReqDef.machine = "ABCDEFGH";


            Info.UserId = userid;

            //byte[] msg = XConvert.ToByte(authenReq);
            //byte[] msg = XConvert.ConvertToByte(LoginReqDef);


            MemoryStream strm = new MemoryStream();
            // Creates serializer.
            //var serializer = SerializationContext.Default.GetSerializer<BaseMessage.LoginReqDef>();
            var serializer = MessagePackSerializer.Get<BaseMessage.LoginReqDef>();
            // Pack obj to stream.
            serializer.Pack(strm, LoginReqDef);
            strm.Position = 0;
            // Unpack from stream.
            //var unpackedObject = serializer.Unpack(strm);

            byte[] sendMes = StreamToBytes(strm);
            Stream inputStream = BytesToStream(sendMes); 
            //System.Text.UnicodeEncoding converter = new System.Text.UnicodeEncoding();
            ////byte[] inputBytes = converter.GetBytes(inputString);
            //string inputString = converter.GetString(sendMes);
            //Debug.LogError(sendMes.ToString());
            _gsProxy.notify(BaseMessage.LOGIN_REQ_MSG, sendMes);
        }




        /// <summary>
        /// 将 Stream 转成  byte[] 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        /// <summary>
        /// 将 byte[] 转成 Stream
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        public void LobbyLoginReqDef(int userId, string token)
        {
            BaseMessage.LobbyLoginReqDef LobbyLoginReqDef = new BaseMessage.LobbyLoginReqDef();

            LobbyLoginReqDef.userId = userId;
            LobbyLoginReqDef.token = token;//"2e4f847b-558"
            
            var serializer = MessagePackSerializer.Get<BaseMessage.LobbyLoginReqDef>();

            byte[] msg = serializer.PackSingleObject(LobbyLoginReqDef);
            _gsProxy.notifyMP(BaseMessage.LOBBY_LOGIN_REQ_MSG, msg);
        }





        //deprecated
        public void EnterGame()
        {
			Debug.Log ("EnterGame=======================================");
            BaseMessage.SitDownReqDef sitDownReq = new BaseMessage.SitDownReqDef();
            sitDownReq.iBindUserID = 0;
            sitDownReq.iTableNum = 0;
            sitDownReq.usTableNumExtra = 0;
            var point = new BaseMessage.UserPointLimitDef();
            point.iMinPoint = 0;
            point.iMaxPoint = 0;
            sitDownReq.userPointLimit = point;

            byte[] msg = XConvert.ConvertToByte(sitDownReq);
			_gsProxy.notify(BaseMessage.SITDOWN_REQ_MSG, msg);
        }

        //deprecated
        public void Ready(int ready)
        {
            BaseMessage.ReadyReqDef readyReqDef = new BaseMessage.ReadyReqDef();
            readyReqDef.cReady = (byte)ready;

            byte[] msg = XConvert.ConvertToByte(readyReqDef);
			_gsProxy.notify(BaseMessage.READY_REQ_MSG, msg);

#if UNITY_5
            UnityEngine.Debug.Log("Send Ready Msg");
#endif
        }

        public void Kick()
        {
			_gsProxy.Disconnect ();
        }

        public void HeartBeat()
        {
			if (_gsProxy.IsConnected ()) {
				_gsProxy.notify (OdaoMessageHeaderId.NM_KEEP_ALIVE, new byte[8]);
			}
        }

		public void Match()
		{			
			GameMessage.MatchGameDef data = new GameMessage.MatchGameDef ();
			data.gameType = 1;
			byte[] msg = XConvert.ConvertToByte (data);
			_gsProxy.notify (GameMessage.c2s_MatchGameDef, msg);
		}

        public new bool Play(TileDef def)
        {
			if (base.Play (def)) {
                GameMessage.SendCardReqDef data = new GameMessage.SendCardReqDef();
                data.cCard = def.Value;
#if USE_MSGPACK3
                var serializer = MsgPack.Serialization.MessagePackSerializer.Get<GameMessage.SendCardReqDef>();
                byte[] msg = serializer.PackSingleObject(data);
                _gsProxy.notifyMP(GameMessage.c2s_SendCardReqDef, msg);
#else
				byte[] msg = XConvert.ConvertToByte (data);
				_gsProxy.notify (GameMessage.c2s_SendCardReqDef, msg);
#endif

#if UNITY_5
                                     UnityEngine.Debug.Log ("SendCardReqDef -> " + data.cCard);
				UnityEngine.Debug.Log("##########AFTER -> " + ToString());
#endif
				return true;
			}

#if UNITY_5
			UnityEngine.Debug.Log ("SendCardReqDef Error...");
#endif

            return false;
        }

		public override bool Chow(TileDef tile, int from = -1)
		{
			return true;
		}

		public override TileComboDef Pong(TileDef tile, int from = -1)
		{
			Debug.Log("*************************player Pong:" + tile.ToString());
			TileComboDef combo = base.Pong (tile, from);
			if (combo != null) {
				GameMessage.SpecialCardReqDef data = new GameMessage.SpecialCardReqDef ();
				data.specialType = (byte)GameMessage.SPECIAL_TYPE.PONG;
				data.card = tile.Value;
                Debug.Log("*************************Pong:" + data.card);
                SendSpecialCardReqDef(data);
				/*byte[] msg = XConvert.ConvertToByte (data);
				_gsProxy.notify (GameMessage.c2s_SpecialCardReqDef, msg);*/
				return combo;
			}
            Debug.Log("**********************Pong***combo == null");
			return null;
		}

		public override TileComboDef Kong(TileDef tile, int from = -1)
		{
			Debug.Log("*************************player Kong:" + tile.ToString());
			TileComboDef combo = base.Kong (tile, from);
			if (combo != null) {
				GameMessage.SpecialCardReqDef data = new GameMessage.SpecialCardReqDef ();
				data.specialType = (byte)GameMessage.SPECIAL_TYPE.KONG;
				data.card = tile.Value;
                Debug.Log("Kong:" + data.card);
                SendSpecialCardReqDef(data);
				/*byte[] msg = XConvert.ConvertToByte (data);
				_gsProxy.notify (GameMessage.c2s_SpecialCardReqDef, msg);*/
				return combo;
			}
			return null;
		}

		public override void Win(byte card)
        {
			Debug.Log("*************************player Win:" + card);
            GameMessage.SpecialCardReqDef data = new GameMessage.SpecialCardReqDef();
            data.specialType = (byte)GameMessage.SPECIAL_TYPE.WIN;
			data.card = card;
            Debug.Log("Win:" + card);
			/*byte[] msg = XConvert.ConvertToByte (data);
			_gsProxy.notify (GameMessage.c2s_SpecialCardReqDef, msg);*/
            SendSpecialCardReqDef(data);
        }

		public override void Pass()
		{
			GameMessage.SpecialCardReqDef data = new GameMessage.SpecialCardReqDef ();
			data.specialType = (byte)GameMessage.SPECIAL_TYPE.PASS;
			data.card = 0;
            Debug.Log("Pass:" + 0);
			/*byte[] msg = XConvert.ConvertToByte (data);
			_gsProxy.notify (GameMessage.c2s_SpecialCardReqDef, msg);*/
            SendSpecialCardReqDef(data);
		}

		public override void SetLackTileKind(TileDef.Kind k)
        {
            base.SetLackTileKind(k);
            Debug.Log("SetLackTileKind:"+k);
            GameMessage.DingQueDef data = new GameMessage.DingQueDef();
            data.cCard = (byte)k;

#if USE_MSGPACK3
            var serializer = MsgPack.Serialization.MessagePackSerializer.Get<GameMessage.DingQueDef>();
            byte[] msg = serializer.PackSingleObject(data);
            _gsProxy.notifyMP(GameMessage.c2s_DingQueDef, msg);
#else

            byte[] msg = XConvert.ConvertToByte (data);
            _gsProxy.notify(GameMessage.c2s_DingQueDef, msg);
			
#endif

        }

		public bool TrusteeShip(int flag)
		{
			GameMessage.TrusteeShipClientDef data = new GameMessage.TrusteeShipClientDef();
            data.flag = (byte)flag;
            byte[] msg = XConvert.ConvertToByte(data);
            _gsProxy.notify(GameMessage.c2s_TrusteeShipClientDef, msg);

            return true;
        }

        #endregion

        #region qmy common Send SpecialCardReq
        public void SendSpecialCardReqDef(GameMessage.SpecialCardReqDef data)
        {
            
#if USE_MSGPACK3
            var serializer = MsgPack.Serialization.MessagePackSerializer.Get<GameMessage.SpecialCardReqDef>();
            byte[] msg = serializer.PackSingleObject(data);
            _gsProxy.notifyMP(GameMessage.c2s_SpecialCardReqDef, msg);
#else
            byte[] msg = XConvert.ConvertToByte(data);
            _gsProxy.notify(GameMessage.c2s_SpecialCardReqDef, msg);
#endif


        }

		public override void LeaveTable()
		{
			byte[] msg = new byte[0];
			_gsProxy.notifyMP(GameMessage.c2s_LeaveTableReqDef, msg);
		}

		public override void ChangeTable()
		{
			byte[] msg = new byte[0];
			_gsProxy.notifyMP(GameMessage.c2s_ChangeTableReqDef, msg);
		}

		public override void ContinueGame()
		{
			byte[] msg = new byte[0];
			_gsProxy.notifyMP(GameMessage.c2s_ContinueGameReqDef, msg);
		}

		public override void Huanpai(List<byte> cards)
		{
			Debug.Log ("--------------Huanpai:--------------------");
			GameMessage.HuanPaiDef data = new GameMessage.HuanPaiDef();
			data.vHuanPai = cards;
			var serializer = MsgPack.Serialization.MessagePackSerializer.Get<GameMessage.HuanPaiDef>();
			byte[] msg = serializer.PackSingleObject(data);
			_gsProxy.notifyMP(GameMessage.c2s_HuanPaiDef, msg);
		}

		//收到0xB1发送此消息
		public void LeaveOut(sbyte leaveType)
		{
			BaseMessage.TablePlayerLeaveDef leavedef = new BaseMessage.TablePlayerLeaveDef();
			leavedef.iUserID = Info.UserId;
			leavedef.cLeaveType = leaveType;

			byte[] msg = XConvert.ConvertToByte(leavedef);
			_gsProxy.notify(BaseMessage.TABLE_PLAYER_LEAVE_MSG, msg);
			Debug.Log("***************send LeaveOut type:"+leaveType);
		}
        #endregion

    }
} 
