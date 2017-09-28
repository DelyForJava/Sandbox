#define USER_MP
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
using UnityEngine;
#endif
using NetworkInterface;
using MP;
using MsgPack;
using MsgPack.Serialization;

namespace odao.scmahjong
{
	public class PlayerInfo {
		public int UserId { get; set; }
	}

	public partial class NetworkPlayer : Player
	{		
		public void InitBaseMessage(OdaoClient client)
		{
			_gsProxy = client;
		    var playerInfo = UIOperation.playerLobbyInfo;

            _gsProxy.on(BaseMessage.LOGIN_REQ_MSG, delegate (Message obj) {
                OdaoMessage msg = (OdaoMessage)obj;
                //BaseMessage.LoginResDef data = XConvert.ConvertToObject<BaseMessage.LoginResDef>(msg.data);
                var serializer = MsgPack.Serialization.MessagePackSerializer.Get<BaseMessage.LoginResDef>();
                var data = serializer.UnpackSingleObject(msg.data);
                Debug.LogError("LOGIN_RES_MSG: " + BaseMessage.LOGIN_REQ_MSG + ",cErrorCode:"+ data.cErrorCode + ", iUserID:" + data.iUserID + ",accountId:" + data.accountId + ",serviceId:" + data.serviceId + ",szPasswdToken:" + data.szPasswdToken + ",cGender:" + data.cGender + ",cVipLv:" + data.cVipLv + ",llGameCoin:" + data.llGameCoin + ",llBankCoin:" + data.llBankCoin + ",llDiamondNum:" + data.llDiamondNum + ",llGoldBean:" + data.llGoldBean + ",szNickName:" + data.szNickName + ",szWXIconURL:" + data.szWXIconURL + ",szWXNickName:" + data.szWXNickName + ",iVipExp:" + data.iVipExp + ",cLevel:" + data.cLevel + ",iLevelExp:" + data.iLevelExp);

                
                playerInfo.iUserID = data.iUserID;
                playerInfo.accountId = data.accountId;
                playerInfo.serviceId = data.serviceId;
                playerInfo.szPasswdToken = data.szPasswdToken;
                playerInfo.cGender = data.cGender;
                playerInfo.cVipLv = data.cVipLv;
                playerInfo.llGameCoin = data.llGameCoin;
                playerInfo.llBankCoin = data.llBankCoin;
                playerInfo.llDiamondNum = data.llDiamondNum;
                playerInfo.llGoldBean = data.llGoldBean;
                playerInfo.szNickName = data.szNickName;
                playerInfo.szWXIconURL = data.szWXIconURL;
                playerInfo.szWXNickName = data.szWXNickName;
                playerInfo.iVipExp = data.iVipExp;
                playerInfo.cLevel = data.cLevel;
                playerInfo.iLevelExp = data.iLevelExp;



#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
                Loom.QueueOnMainThread(delegate () {
                    //GameLoading.SwitchScene(2);
                    UnityEngine.SceneManagement.SceneManager.LoadScene(2);
                });

                if (data.cErrorCode == 0)
                {
                    var gameClient = GameClient.Instance;
                    gameClient.MahjongGamePlayer.ConnectGameServer(UIOperation.ipRes, 36665, delegate ()
                    {
                        gameClient.MahjongGamePlayer.LobbyLoginReqDef(data.iUserID, data.szPasswdToken);
                    });
                }
#endif
            });

            _gsProxy.on(BaseMessage.LOBBY_LOGIN_RES_MSG, delegate (Message obj) {
                OdaoMessage msg = (OdaoMessage)obj;
                //BaseMessage.LoginResDef data = XConvert.ConvertToObject<BaseMessage.LoginResDef>(msg.data);
                var serializer = MsgPack.Serialization.MessagePackSerializer.Get<BaseMessage.LobbyLoginResDef>();
                var data = serializer.UnpackSingleObject(msg.data);
                Debug.LogError("LOBBY_LOGIN_RES_MSG: " + BaseMessage.LOBBY_LOGIN_RES_MSG + ", cErrorCode:" + data.cErrorCode + ",iUserID:" + data.iUserID);

#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
                Loom.QueueOnMainThread(delegate () {
                    //GameLoading.SwitchScene(2);
                    //UnityEngine.SceneManagement.SceneManager.LoadScene(2);
                });
#endif
            });

		    _gsProxy.on(BaseMessage.LOBBY_SHOP_EXCHANGE_COIN_RES_MSG, delegate (Message obj) {
		        OdaoMessage msg = (OdaoMessage)obj;
		        //BaseMessage.LoginResDef data = XConvert.ConvertToObject<BaseMessage.LoginResDef>(msg.data);
		        var serializer = MsgPack.Serialization.MessagePackSerializer.Get<BaseMessage.BuyGoldResDef>();
		        var data = serializer.UnpackSingleObject(msg.data);
		        Debug.LogError("LOBBY_SHOP_EXCHANGE_COIN_RES_MSG: " + BaseMessage.LOBBY_SHOP_EXCHANGE_COIN_RES_MSG + ", cErrorCode:" + data.cErrorCode + ",iAddCoin:" + data.iAddCoin+ ",iSubIDiamond:" + data.iSubIDiamond);

		        if (data.cErrorCode == 0)
		        {
		            playerInfo.llGameCoin += data.iAddCoin;
		            playerInfo.llDiamondNum -= data.iSubIDiamond;
                }
                else if(data.cErrorCode == 1)
		        {
		            //钻石不足
		        }
		        else
		        {
		            //配置错误
		        }
            });

		    _gsProxy.on(BaseMessage.LOBBY_MODIFY_NICK_NAME_RES_MSG, delegate (Message obj) {
		        OdaoMessage msg = (OdaoMessage)obj;
		        //BaseMessage.LoginResDef data = XConvert.ConvertToObject<BaseMessage.LoginResDef>(msg.data);
		        var serializer = MsgPack.Serialization.MessagePackSerializer.Get<BaseMessage.ChangeNameResDef>();
		        var data = serializer.UnpackSingleObject(msg.data);
		        Debug.LogError("LOBBY_MODIFY_NICK_NAME_RES_MSG: " + BaseMessage.LOBBY_MODIFY_NICK_NAME_RES_MSG + ", cErrorCode:" + data.cErrorCode + ",iSubDiamond:" + data.iSubDiamond + ",szNickName:" + data.szNickName);

		        if (data.cErrorCode == 0)
		        {
		            playerInfo.szNickName = data.szNickName;
		            playerInfo.llDiamondNum -= data.iSubDiamond;
                    //GameObject.Find("Canvas/personalInfoPanel/comfirmPanel").SetActive(false);
		        }
		        else if (data.cErrorCode == 1)
		        {
		            //钻石不足
		        }
		        else
		        {
		            //配置错误
		        }
		    });

		    _gsProxy.on(BaseMessage.LOBBY_MODIFY_GENDER_RES_MSG, delegate (Message obj) {
		        OdaoMessage msg = (OdaoMessage)obj;
		        //BaseMessage.LoginResDef data = XConvert.ConvertToObject<BaseMessage.LoginResDef>(msg.data);
		        var serializer = MsgPack.Serialization.MessagePackSerializer.Get<BaseMessage.ChangeGenderResDef>();
		        var data = serializer.UnpackSingleObject(msg.data);
		        Debug.LogError("LOBBY_MODIFY_GENDER_RES_MSG: " + BaseMessage.LOBBY_MODIFY_GENDER_RES_MSG + ", cErrorCode:" + data.cErrorCode + ",cGender:" + data.cGender );

		        if (data.cErrorCode == 0)
		        {
		            playerInfo.cGender = data.cGender;
		            Loom.QueueOnMainThread(delegate () {
		                PlayerDataManager.Instance.LoadGenderImage();
                    });
                }
		        else if (data.cErrorCode == 1)
		        {
		            //钻石不足
		        }
		        else
		        {
		            //配置错误
		        }
		    });



		    _gsProxy.on(BaseMessage.LOBBY_PACKAGE_INFO_RES_MSG, delegate (Message obj) {
		        OdaoMessage msg = (OdaoMessage)obj;
		        var serializer = MessagePackSerializer.Get<BaseMessage.PackageInfoResDef>();
		        var data = serializer.UnpackSingleObject(msg.data);
		        Debug.LogError("LOBBY_PACKAGE_INFO_RES_MSG: " + BaseMessage.LOBBY_PACKAGE_INFO_RES_MSG + ", ItemInfoList.Count:" + data.ItemInfoList.Count);
		        if (data.ItemInfoList.Count != 0)
		        {
		            Loom.QueueOnMainThread(delegate () {
		                PackageDataManager.Instance.LoadDataToPanel(data.ItemInfoList);
		            });
		        }

            });

		    _gsProxy.on(BaseMessage.LOBBY_PACKAGE_ADD_ITEM_RES_MSG, delegate (Message obj) {
		        OdaoMessage msg = (OdaoMessage)obj;
		        var serializer = MessagePackSerializer.Get<BaseMessage.PackageAddItemResDef>();
		        var data = serializer.UnpackSingleObject(msg.data);
		        Debug.LogError("LOBBY_PACKAGE_ADD_ITEM_RES_MSG: " + BaseMessage.LOBBY_PACKAGE_ADD_ITEM_RES_MSG + ", cErrorCode:" + data.cErrorCode + ", iItemID:" + data.iItemID + ", iAddNum:" + data.iAddNum + ", LExpireTime:" + data.LExpireTime + ", LGetTime:" + data.LGetTime);


		    });




            #region Old




            _gsProxy.on (BaseMessage.AUTHEN_RES_MSG, delegate (Message obj) {
				OdaoMessage msg = (OdaoMessage)obj;
				BaseMessage.AuthenResDef data = XConvert.ConvertToObject<BaseMessage.AuthenResDef> (msg.data);
				Debug.Log ("AUTHEN_RES_MSG " + data.cError + "," + data.iPlayerServerID + "," + data.iPlayerGameID);

				#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
				Loom.QueueOnMainThread (delegate() {
					GameLoading.SwitchScene (2);
				});
				#endif
			});

			_gsProxy.on (BaseMessage.AGAIN_LOGIN_RES_MSG, delegate (Message obj) {
				Debug.Log ("AGAIN_LOGIN_RES_MSG ");
				OdaoMessage msg = (OdaoMessage)obj;
				int offset = 0;
				BaseMessage.AgainLoginRes data = XConvert.ConvertToObject<BaseMessage.AgainLoginRes> (msg.data, ref offset);
				Debug.Log ("AGAIN_LOGIN_RES_MSG " + data.iUserID + ","
				+ data.iServerTime + ","
				+ data.iTableNum + ","
				+ data.usTableNumExtra + ","
				+ data.cPlayerNum);

				//userinfo
				for (int i = 0; i < data.cPlayerNum; ++i) {
					BaseMessage.PlayerInfoExtra userInfo = XConvert.ConvertToObject<BaseMessage.PlayerInfoExtra> (msg.data, ref offset);
					//byte[] bytes = System.Text.Encoding.GetEncoding("GBK").GetBytes(userInfo.szUserName);
					//string name = System.Text.Encoding.UTF8.GetString(bytes);
                }
                int tempHandCardLen = 0;

#if USER_MP
                byte[] dataMP = msg.data.Skip(offset).ToArray();
				/*
				System.IO.FileStream fsForWrite = new System.IO.FileStream("MP.GameMessage.GameStateDef.bytes", System.IO.FileMode.Create);
				try
				{
					fsForWrite.Write(dataMP,0,dataMP.Length);
				}
				catch(Exception ex)
				{
					Debug.Log(ex.ToString());
				}
				finally
				{
					fsForWrite.Close();
				}
				*/
                var serializer = MsgPack.Serialization.MessagePackSerializer.Get<MP.GameMessage.GameStateDef>();
                GameMessage.GameStateDef info = serializer.UnpackSingleObject(dataMP);
                tempHandCardLen = info.cHandCards.Count;
                
#else
                GameMessage.GameStateDef info = XConvert.ConvertToObject<GameMessage.GameStateDef>(msg.data, ref offset);
                tempHandCardLen = info.cHandCards.Length;
#endif

				#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
				GameClient.Instance.GameStateDef = info;
				#endif
				PocketList.Clear ();
				// total 14
				for (int i = 0; i < tempHandCardLen; ++i) {
					if (TileDef.IsValid (info.cHandCards [i])) {
						TileDef def = TileDef.Create (info.cHandCards [i]);
						PocketList.Add (def);
					}
				}
#if USER_MP
                //玩家已经出的牌
                List<TileDef> list = new List<TileDef>();
                for (int i = 0; i < GameMessage.TABLE_PLAYER_NUM; ++i)
                {
                    for (int j = 0; j < info.cSendCards[i].cCards.Count; ++j)
                    {
                        byte card = info.cSendCards[i].cCards[j];
                        Console.Write("{0} ", card);
                        list.Add(TileDef.Create(card));
                    }
                    Debug.Log("##################");
                }
#else
                 //玩家已经出的牌
                List<TileDef> list = new List<TileDef> ();
				for (int i = 0; i < GameMessage.TABLE_PLAYER_NUM; ++i) {
					for (int j = 0; j < 21; ++j) {
						byte card = info.cSendCards [i * 21 + j];
						Console.Write ("{0} ", card);
						list.Add (TileDef.Create (card));
					}
					Debug.Log ("##################");
				}
#endif
				MahjongTile.TotalClickCount = 1;
				//HUAN PAI
				if(info.iGameStates == GameMessage.GS_WK_HUANPAI)
				{
					for(int i = 0; i < GameMessage.TABLE_PLAYER_NUM; i++)
					{
						UIOperation.Instance._headDatas[i].gameState = UIOperation.gameState.XUANPAI;
						UIOperation.Instance._headDatas[i].isShow = true;
					}
					for(int i = 0; i < info.v_RecommendCard.Count; i++)
					{
						if(i < GameMessage.MAXHUANPAINUM)
							UIOperation.Instance._HuanPaiOldDef[i] = TileDef.Create(info.v_RecommendCard[i]);
					}
					MahjongTile.TotalClickCount = 3;

				}else if(info.iGameStates == GameMessage.GS_WK_DINGQUE)
				{//DING QUE
					for (int i = 0; i < GameMessage.TABLE_PLAYER_NUM; i++)
					{
						UIOperation.Instance._headDatas[i].gameState = UIOperation.gameState.DINGQUE;
						UIOperation.Instance._headDatas[i].isShow = true;
					}
				}
				if(info.cDingQueCards.Count > 0)
				{//定缺完毕

					for (int i = 0; i < info.cDingQueCards.Count; ++i)
					{
						TileDef.Kind k = (TileDef.Kind)info.cDingQueCards[i];
						if(k != TileDef.Kind.NONE)
						{
							UIOperation.Instance._headDatas[i].dingque = k;
							UIOperation.Instance._headDatas[i].gameState = UIOperation.gameState.NONE;
						}

					}
				}
				GameClient.Instance.SpecialType = 0;
				if(info.iSpecialTypeTotal > 0 && info.v_SpecialCard.Count > 0)
				{//等待吃碰杠胡
					GameClient.Instance.SpecialCard = info.v_SpecialCard[0];
					GameClient.Instance.SpecialType = info.iSpecialTypeTotal;
				}


                #if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
                Loom.QueueOnMainThread (delegate() {
					if (GameClient.Instance.IsInMahjongGame ()) {
						GameClient.Instance.MahjongGamePlayer.Index = data.usTableNumExtra;
						GameClient.Instance.MG.OnRestoreGameState (info);
						//游戏状态 换牌：0x50, 定缺:0x53 等待吃胡碰杠状态:0x08 等待出牌:0x09
//						if(info.iGameStates == 0x50)
//						{
//							UIGameSetingController.Instance.OnRefreshPlayerState();
//
//						}else if(info.iGameStates == 0x53)
//						{
//							UIGameSetingController.Instance.OnRefreshHeadDingqueState();
//						}
						UIGameSetingController.Instance.OnRefreshHeadAll();
					} else {
						GameClient.Instance.MahjongGamePlayer.Index = data.usTableNumExtra;
						GameLoading.SwitchScene (3);
					}
				});
				#endif
			});

			_gsProxy.on (BaseMessage.AGAIN_LOGIN_OTHER_NOTICE_MSG, delegate (Message obj) {
				OdaoMessage msg = (OdaoMessage)obj;
				Debug.Log ("AGAIN_LOGIN_OTHER_NOTICE_MSG");
				//userid
				/*
             * typedef struct OtherAgainLoginNotice                  
             * //AGAIN_LOGIN_OTHER_NOTICE_MSG重入通知其他玩家 {    
					* MsgHeader      msgHeadInfo;
					* int            iUserID;
					* } OtherAgainLoginNoticeDef;
				*/
			});


			_gsProxy.on (BaseMessage.SITDOWN_RES_MSG, delegate (Message obj) {
				Debug.Log ("NotImplementedException : SITDOWN_RES_MSG");
				return;
				OdaoMessage msg = (OdaoMessage)obj;
				BaseMessage.SitDownResDef data = new BaseMessage.SitDownResDef ();
				data = XConvert.ConvertToObject<BaseMessage.SitDownResDef> (msg.data);
				Debug.Log (string.Format("NotImplementedException : SITDOWN_RES_MSG -> {0},{1},{2},{3}", data.iTableNum, data.cError, data.usTableNumExtra, data.cPlayerNum));
			});

			_gsProxy.on (BaseMessage.TABLE_PLAYER_JOIN_MSG, delegate (Message obj) {
				Debug.Log ("NotImplementedException : TABLE_PLAYER_JOIN_MSG");
			});

			_gsProxy.on (BaseMessage.TABLE_PLAYER_LEAVE_MSG, delegate (Message obj) {
				Debug.Log ("NotImplementedException : TABLE_PLAYER_LEAVE_MSG");
			});

			_gsProxy.on (BaseMessage.READY_RES_MSG, delegate (Message obj) {
				Debug.Log ("NotImplementedException : READY_RES_MSG");
				return;
				OdaoMessage msg = (OdaoMessage)obj;
				BaseMessage.ReadyResDef data = new BaseMessage.ReadyResDef ();
				data = XConvert.ConvertToObject<BaseMessage.ReadyResDef> (msg.data);
				Debug.Log (string.Format("椅子号{0}", data.usTableNumExtra));
			});

			//玩家输赢数据，经验值
			_gsProxy.on (BaseMessage.USER_SCORE_INFO_MSG, delegate (Message obj) {
				Debug.Log ("NotImplementedException : USER_SCORE_INFO_MSG");
			});

			//旁观玩家进入桌子
			_gsProxy.on (BaseMessage.JOIN_TABLE_AS_VISITOR, delegate (Message obj) {
				Debug.Log ("NotImplementedException : JOIN_TABLE_AS_VISITOR");
			});

			//更新一桌玩家身份
			_gsProxy.on (BaseMessage.UPDATE_TABLE_PLAYER_STATUS, delegate (Message obj) {
				Debug.Log ("NotImplementedException : UPDATE_TABLE_PLAYER_STATUS");
			});

			//VIP踢人请求
			_gsProxy.on (BaseMessage.VIP_KICKOUT_PLAYER_REQ, delegate (Message obj) {
				Debug.Log ("NotImplementedException : VIP_KICKOUT_PLAYER_REQ");
			});

			//VIP踢人回应
			_gsProxy.on (BaseMessage.VIP_KICKOUT_PLAYER_RES, delegate (Message obj) {
				Debug.Log ("NotImplementedException : VIP_KICKOUT_PLAYER_RES");
			});

			//自由设定桌子人数  0XA013后在Odao_SiteActivity_Msg.h中用
			_gsProxy.on (BaseMessage.TABLE_SET_PLAYER_NUMBER, delegate (Message obj) {
				Debug.Log ("NotImplementedException : TABLE_SET_PLAYER_NUMBER");
			});

			//充值同步积分
			_gsProxy.on (BaseMessage.CLIENT_CHARGE_REFRESH_MONEY_REQ, delegate (Message obj) {
				Debug.Log ("NotImplementedException : CLIENT_CHARGE_REFRESH_MONEY_REQ");
			});

			//服务器踢除玩家
			_gsProxy.on (BaseMessage.KICK_OUT_SERVER_MSG, delegate (Message obj) {
				Debug.Log ("KICK_OUT_SERVER_MSG");
				OdaoMessage msg = (OdaoMessage)obj;
				BaseMessage.TablePlayerLeaveDef data = new BaseMessage.TablePlayerLeaveDef ();
				data = XConvert.ConvertToObject<BaseMessage.TablePlayerLeaveDef> (msg.data);
				Debug.Log("***************KICK_OUT_SERVER_MSG type:"+data.cLeaveType);
				#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
				Loom.QueueOnMainThread (delegate() {
					GameClient.Instance.MahjongGamePlayer.LeaveOut(data.cLeaveType);
					UIDebugViewController.Instance.SetText ("你为啥把我踢下去？？？？？？？？KICK_OUT_SERVER_MSG");
					UIDebugViewController.Instance.OnRefresh ();
				});
				#endif
			});

			//入座请求密码
			_gsProxy.on (BaseMessage.CHECK_SITDOWN_PASSWD_REQ, delegate (Message obj) {
				Debug.Log ("NotImplementedException : CHECK_SITDOWN_PASSWD_REQ");
			});

			//请求设定桌底柱消息
			_gsProxy.on (BaseMessage.SET_TABLE_BASEPOINT_REQ, delegate (Message obj) {
				Debug.Log ("NotImplementedException : SET_TABLE_BASEPOINT_REQ");
			});

			_gsProxy.on (BaseMessage.TABLE_PLAYER_LEAVE_NOTICE, delegate (Message obj) {
				Debug.Log ("NotImplementedException : TABLE_PLAYER_LEAVE_NOTICE");
			});

			_gsProxy.on (BaseMessage.SEND_ALL_USERINFO_TO_CLIENT, delegate (Message obj) {
				Debug.Log ("NotImplementedException : SEND_ALL_USERINFO_TO_CLIENT");
				return;

				OdaoMessage msg = (OdaoMessage)obj;
				int offset = 0;

				BaseMessage.SendAllUserInfo data = XConvert.ConvertToObject<BaseMessage.SendAllUserInfo> (msg.data, ref offset);
				for (int i = 0; i < data.iUserCount; ++i) {
					BaseMessage.UserInfo userInfo = XConvert.ConvertToObject<BaseMessage.UserInfo> (msg.data, ref offset);
					byte[] bytes = System.Text.Encoding.GetEncoding ("GBK").GetBytes (userInfo.szUserName);
					string name = System.Text.Encoding.UTF8.GetString (bytes);
					Debug.Log ("SEND_ALL_USERINFO_TO_CLIENT---->" + name);
				}
			});

			//游戏结束服务器通知 所有游戏公用
			_gsProxy.on (BaseMessage.GAME_RESULT_SERVER_MSG, delegate (Message obj) {
				Debug.Log ("NotImplementedException : GAME_RESULT_SERVER_MSG");
			});

			//游戏公告消息
			_gsProxy.on (BaseMessage.GAME_BULL_NOTICE_MSG, delegate (Message obj) {
				Debug.Log ("NotImplementedException : GAME_BULL_NOTICE_MSG");
			});

			//对应玩家的催牌消息
			_gsProxy.on (BaseMessage.URGR_CARD_MSG, delegate (Message obj) {
				Debug.Log ("NotImplementedException : URGR_CARD_MSG");
			});

			//玩家的喊话消息
			_gsProxy.on (BaseMessage.SHOUT_INFO_RES_MSG, delegate (Message obj) {
				Debug.Log ("NotImplementedException : SHOUT_INFO_RES_MSG");
			});

			//游戏基本房间信息
			_gsProxy.on (BaseMessage.SEND_GAME_ROOM_INFO, delegate (Message obj) {
				Debug.Log ("NotImplementedException : SEND_GAME_ROOM_INFO");
			});

			//发送所有玩家的桌信息
			_gsProxy.on (BaseMessage.SEND_ALL_TABLE_INFO, delegate (Message obj) {
				Debug.Log ("NotImplementedException : SEND_ALL_TABLE_INFO");
				/*
					OdaoMessage msg = (OdaoMessage)obj;
					int offset = 0;
					BaseMessage.TableInfo tableInfo = XConvert.ConvertToObject<BaseMessage.TableInfo>(msg.data, ref offset);
					Debug.Log(tableInfo.iAllTableCount);
					for (int i = 0; i < tableInfo.iAllTableCount; ++i)
					{
						BaseMessage.TableStatusInfo tableStatusInfo = XConvert.ConvertToObject<BaseMessage.TableStatusInfo>(msg.data, ref offset);
						//Debug.Log(i + "," + offset);
					}
					*/
			});

			//群发消息处理
			//发送所有玩家信息给客户端
			_gsProxy.on (BaseMessage.SEND_ALLUSER_STATUS_MSG, delegate (Message obj) {
				Debug.Log ("NotImplementedException : SEND_ALLUSER_STATUS_MSG");
				return;
				OdaoMessage msg = (OdaoMessage)obj;
				BaseMessage.UserStatusInfo userStatusInfo = XConvert.ConvertToObject<BaseMessage.UserStatusInfo> (msg.data);
				Debug.Log (string.Format("{0}", userStatusInfo.iMsgCount));
			});

			_gsProxy.on (BaseMessage.SEND_TABLE_STAUTS_TO_CLIENT, delegate (Message obj) {
				Debug.Log ("NotImplementedException : SEND_TABLE_STAUTS_TO_CLIENT");
			});

			_gsProxy.on (BaseMessage.SEND_TABLE_LOCKED_TO_CLIENT, delegate (Message obj) {
				Debug.Log ("NotImplementedException : SEND_TABLE_LOCKED_TO_CLIENT");
			});

			_gsProxy.on (BaseMessage.SEND_TABLE_LEVEL_TO_CLIENT, delegate (Message obj) {
				Debug.Log ("NotImplementedException : SEND_TABLE_LEVEL_TO_CLIENT");
				return;
				OdaoMessage msg = (OdaoMessage)obj;
				BaseMessage.TableLevel tableLevel = XConvert.ConvertToObject<BaseMessage.TableLevel> (msg.data);
				Debug.Log (string.Format("{0},{1}", tableLevel.cTableLevel, tableLevel.usTableNum));
			});

			_gsProxy.on (BaseMessage.SEND_TABLE_SETTING_TO_CLIENT, delegate (Message obj) {
				Debug.Log ("NotImplementedException : SEND_TABLE_SETTING_TO_CLIENT");
			});

			_gsProxy.on (BaseMessage.SEND_TABLE_ANTE_TO_CLIENT, delegate (Message obj) {
				Debug.Log ("NotImplementedException : SEND_TABLE_ANTE_TO_CLIENT");
			});

			//是否需要发送底注
			_gsProxy.on (BaseMessage.SET_BASE_POINT_SERVER, delegate (Message obj) {
				Debug.Log ("NotImplementedException : SET_BASE_POINT_SERVER");
			});

			//游戏开始通知
			_gsProxy.on (BaseMessage.GAME_START_NOTICE_MSG, delegate (Message obj) {
				Debug.Log ("NotImplementedException : GAME_START_NOTICE_MSG");
			});

			//入座成功收到道具信息
			_gsProxy.on (BaseMessage.SIT_DOWN_SUCCESS_MSG, delegate (Message obj) {
				Debug.Log ("NotImplementedException : SIT_DOWN_SUCCESS_MSG");
			});

			//同意底注请求
			_gsProxy.on (BaseMessage.TABLE_BASEINFO_MSG, delegate (Message obj) {
				Debug.Log ("NotImplementedException : TABLE_BASEINFO_MSG");
			});

			//是否需要发送底注(特殊)
			_gsProxy.on (BaseMessage.SET_BASE_POINT_SPE_SERVER, delegate (Message obj) {
				Debug.Log ("NotImplementedException : SET_BASE_POINT_SPE_SERVER");
			});

			//同意底注请求(特殊)
			_gsProxy.on (BaseMessage.TABLE_BASEINFO_SPE_MSG, delegate (Message obj) {
				Debug.Log ("NotImplementedException : TABLE_BASEINFO_SPE_MSG");
			});

			//是否需要发送底注(特殊)
			_gsProxy.on (BaseMessage.HONGBAO_TABLE_INFO, delegate (Message obj) {
				Debug.Log ("NotImplementedException : HONGBAO_TABLE_INFO");
			});

			//玩家登录银行请求
			_gsProxy.on (BaseMessage.USER_LOGIN_BANK_REQ, delegate (Message obj) {
				Debug.Log ("NotImplementedException : USER_LOGIN_BANK_REQ");
			});

			//银行登录结果相应
			_gsProxy.on (BaseMessage.USER_LOGIN_RESULT_NOTICE, delegate (Message obj) {
				Debug.Log ("NotImplementedException : USER_LOGIN_RESULT_NOTICE");
			});

			//取款清求
			_gsProxy.on (BaseMessage.GET_BANK_COIN_REQ, delegate (Message obj) {
				Debug.Log ("NotImplementedException : GET_BANK_COIN_REQ");
			});

			//取款结果响应
			_gsProxy.on (BaseMessage.GET_COIN_RESULT_RES, delegate (Message obj) {
				Debug.Log ("NotImplementedException : GET_COIN_RESULT_RES");
			});

			//存款请求
			_gsProxy.on (BaseMessage.USER_SAVE_COIN_REQ, delegate (Message obj) {
				Debug.Log ("NotImplementedException : USER_SAVE_COIN_REQ");
			});

			//存款结果响应
			_gsProxy.on (BaseMessage.USER_SAVE_COIN_RES, delegate (Message obj) {
				Debug.Log ("NotImplementedException : USER_SAVE_COIN_RES");
			});

			//用户修改银行密码请求
			_gsProxy.on (BaseMessage.MODIFY_BANK_PASSWD_REQ, delegate (Message obj) {
				Debug.Log ("NotImplementedException : MODIFY_BANK_PASSWD_REQ");
			});

			//用户修改银行密码响应
			//玩家积分变化时，通知同桌其他玩家
			_gsProxy.on (BaseMessage.MODIFY_BANK_PASSWD_RES, delegate (Message obj) {
				Debug.Log ("NotImplementedException : MODIFY_BANK_PASSWD_RES");
			});

			_gsProxy.on (BaseMessage.SC_MSG_NOTIFY_TABLE_PLAYERS_COIN_CHANGE, delegate (Message obj) {
				Debug.Log ("NotImplementedException : SC_MSG_NOTIFY_TABLE_PLAYERS_COIN_CHANGE");
			});

			//玩家身上的额外道具
			_gsProxy.on (BaseMessage.GAME_USER_PROPEX_MSG, delegate (Message obj) {
				Debug.Log ("NotImplementedException : GAME_USER_PROPEX_MSG");
			});

			//客户端站点类型
			_gsProxy.on (BaseMessage.GAME_CLIENT_SITE_TYPE_MSG, delegate (Message obj) {
				Debug.Log ("NotImplementedException : GAME_CLIENT_SITE_TYPE_MSG");
			});

		    #endregion
        }
    }
}
