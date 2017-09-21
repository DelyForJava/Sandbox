#define USE_MSGPACK2
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
using UnityEngine;
#endif
using NetworkInterface;
using MsgPack;
using MsgPack.Serialization;
using MP;

namespace odao.scmahjong
{	
	public partial class NetworkPlayer : Player
	{	
		public void InitGameMessage(OdaoClient client)
		{
			_gsProxy = client;

			//客户端发送换牌消息
			_gsProxy.on(GameMessage.c2s_HuanPaiDef, delegate (Message obj)
				{
					Debug.Log("NotImplementedException : SUB_C_HUANPAI");
				});

			//客户端发送认输消息
			_gsProxy.on(GameMessage.SUB_C_QUEDINGRENSHU, delegate (Message obj)
				{
					Debug.Log("NotImplementedException : SUB_C_QUEDINGRENSHU");
				});

			//客户端托管
			_gsProxy.on(GameMessage.s2c_TrusteeShipDef, delegate (Message obj)
				{
					Debug.Log("TRUSTEESHIP_NOTICE_MSG");
					OdaoMessage msg = (OdaoMessage)obj;
					GameMessage.TrusteeShipDef data = XConvert.ConvertToObject<GameMessage.TrusteeShipDef>(msg.data);
					Debug.Log("托管 " + (data.flag > 0 ? "开启" : "关闭") + "," + "椅子号 " + data.cTableNumExtra);					
					#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
                    Loom.QueueOnMainThread(delegate() {
					    GameClient.Instance.MG.ComputerManageFor(data.cTableNumExtra, data.flag);
                    },delegate(){
                        return GameClient.Instance.MG!=null;
                    });
					#endif
				});

			//2：Server 服务器通知客户端消息（将某玩家的动作告诉所有桌上的玩家）
			//出牌通知
			_gsProxy.on(GameMessage.s2c_SendCardNoticeDef, delegate (Message obj)
				{
                    Debug.Log("SEND_CARDS_NOTICE_MSG");
                    OdaoMessage msg = (OdaoMessage)obj;
#if USE_MSGPACK2

                    var serializer = MsgPack.Serialization.MessagePackSerializer.Get<GameMessage.SendCardNoticeDef>();
                    var data = serializer.UnpackSingleObject(msg.data);
#else 
                    GameMessage.SendCardNoticeDef data = XConvert.ConvertToObject<GameMessage.SendCardNoticeDef>(msg.data);
#endif



                    TileDef def = TileDef.Create(data.cCard);
					Debug.Log(string.Format("椅子号{0},出牌{1}", data.cTableNumExtra, def.ToString()));

					#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
					Loom.QueueOnMainThread(delegate() {
						GameClient.Instance.MG.AutoPlay(data.cTableNumExtra, def, data.flag);
					},delegate() {
						return GameClient.Instance.MG!=null;
					});
					#endif
				});

			//吃杠碰胡请求
			_gsProxy.on(GameMessage.s2c_SpecialCardDef, delegate (Message obj)
				{
                    OdaoMessage msg = (OdaoMessage)obj;
#if USE_MSGPACK2
                    var serializer = MsgPack.Serialization.MessagePackSerializer.Get<GameMessage.SpecialCardDef>();
                    var data = serializer.UnpackSingleObject(msg.data);
#else
                    GameMessage.SpecialCardDef data = XConvert.ConvertToObject<GameMessage.SpecialCardDef>(msg.data);
					
#endif
                    Debug.Log(string.Format("s2c_SpecialCardDef0x{0:x} --> {1:x}", data.specialType, data.card));


#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
                    Loom.QueueOnMainThread(delegate() {

						GameClient.Instance.SpecialCard = data.card;

						if(IsSpecialType(data.specialType, (byte)GameMessage.SPECIAL_TYPE.PASS))
						{
                            Debug.Log("---OpenCombo PASS---");
							UIGameComboController.Instance.OpenCombo(TileDef.ComboType.PASS,GameClient.Instance.MG.Self);
						}

						if(IsSpecialType(data.specialType,(byte)GameMessage.SPECIAL_TYPE.PONG))
						{
                            Debug.Log("---OpenCombo PONG---");
							UIGameComboController.Instance.OpenCombo(TileDef.ComboType.PONG,GameClient.Instance.MG.Self);
						}

						if(IsSpecialType(data.specialType,(byte)GameMessage.SPECIAL_TYPE.KONG))
						{
                            Debug.Log("---OpenCombo KONG---");
							UIGameComboController.Instance.OpenCombo(TileDef.ComboType.KONG_DARK,GameClient.Instance.MG.Self);
						}

						if(IsSpecialType(data.specialType,(byte)GameMessage.SPECIAL_TYPE.DARK_KONG))
						{
                            Debug.Log("---OpenCombo DARK_KONG---");
							UIGameComboController.Instance.OpenCombo(TileDef.ComboType.KONG_DARK,GameClient.Instance.MG.Self);
						}

						if(IsSpecialType(data.specialType,(byte)GameMessage.SPECIAL_TYPE.SPECIAL_TYPE_PENGGANG))
						{
                            Debug.Log("---OpenCombo SPECIAL_TYPE_PENGGANG---");
							UIGameComboController.Instance.OpenCombo(TileDef.ComboType.KONG_TURN,GameClient.Instance.MG.Self);
						}

						if(IsSpecialType(data.specialType,(byte)GameMessage.SPECIAL_TYPE.WIN))
						{
                            Debug.Log("---OpenCombo WIN---");
							UIGameComboController.Instance.OpenCombo(TileDef.ComboType.WIN,GameClient.Instance.MG.Self);
						}
					}, delegate() {
						return GameClient.Instance.MG!=null;
					});
					#endif
				});

			//玩家吃杠碰胡通知
			_gsProxy.on(GameMessage.s2c_SpecialNoticeSerDef, delegate (Message obj)
				{
					Debug.Log("********************s2c_SpecialNoticeSerDef********************");
                    OdaoMessage msg = (OdaoMessage)obj;
#if USE_MSGPACK2
                    var serializer = MsgPack.Serialization.MessagePackSerializer.Get<GameMessage.SpecialNoticeSerDef>();
                    var data = serializer.UnpackSingleObject(msg.data);
#else
                    GameMessage.SpecialNoticeSerDef data = XConvert.ConvertToObject<GameMessage.SpecialNoticeSerDef>(msg.data);
#endif

                    TileComboDef tileComboDef = null;

					if(IsSpecialType(data.cSpecialType,(byte)GameMessage.SPECIAL_TYPE.PONG))
					{
						tileComboDef = TileComboDef.Create(data.cDianPao, TileDef.ComboType.PONG, TileDef.Create(data.cCard));
					}

					if(IsSpecialType(data.cSpecialType,(byte)GameMessage.SPECIAL_TYPE.KONG))
					{
						tileComboDef = TileComboDef.Create(data.cDianPao, TileDef.ComboType.KONG, TileDef.Create(data.cCard));
					}

					if(IsSpecialType(data.cSpecialType,(byte)GameMessage.SPECIAL_TYPE.DARK_KONG))
					{
						tileComboDef = TileComboDef.Create(data.cDianPao, TileDef.ComboType.KONG_DARK, TileDef.Create(data.cCard));
					}

					if(IsSpecialType(data.cSpecialType,(byte)GameMessage.SPECIAL_TYPE.SPECIAL_TYPE_PENGGANG))
					{
						tileComboDef = TileComboDef.Create(data.cDianPao, TileDef.ComboType.KONG_TURN, TileDef.Create(data.cCard));
					}

					if(IsSpecialType(data.cSpecialType,(byte)GameMessage.SPECIAL_TYPE.WIN))
					{
						WinState = true;
						tileComboDef = TileComboDef.Create(data.cDianPao, TileDef.ComboType.WIN, TileDef.Create(data.cCard));
					}
					if(null != tileComboDef)
						Debug.Log("--------------------->s2c_SpecialNoticeSerDef   " + tileComboDef.ToString());
					#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
					Loom.QueueOnMainThread(delegate() {
						UIGameComboController.Instance.Close();
						if(!IsSpecialType(data.cSpecialType,(byte)GameMessage.SPECIAL_TYPE.PASS)){
							GameClient.Instance.MG.AutoCombo(data.cExtraTableNum,tileComboDef,data.flag);
							if(data.cExtraTableNum == Index)
								UIGameEffectController.Instance.ShowComboEffect(tileComboDef.Combo);
						}

					});
					#endif
				});

			//通知玩家定缺
			_gsProxy.on(GameMessage.s2c_DingQueDef, delegate (Message obj)
				{
					Debug.Log("******************s2c_DingQueDef");
#if USE_MSGPACK2
                    OdaoMessage msg = (OdaoMessage)obj;
                    var serializer = MsgPack.Serialization.MessagePackSerializer.Get<GameMessage.DingQueDef>();
                    var lactKind = serializer.UnpackSingleObject(msg.data);
                    
#else
                    Debug.Log("s2c_DingQueDef");
					OdaoMessage msg = (OdaoMessage)obj;
					GameMessage.DingQueDef lactKind = XConvert.ConvertToObject<GameMessage.DingQueDef>(msg.data);
#endif


#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
                    
                    for (int i = 0; i < GameMessage.TABLE_PLAYER_NUM; i++)
                    {
                        UIOperation.Instance._headDatas[i].gameState = UIOperation.gameState.DINGQUE;
                        UIOperation.Instance._headDatas[i].isShow = true;
                    }

                    Loom.QueueOnMainThread(delegate() {
						UIDQSettingController.Instance.Open();
                        UIGameSetingController.Instance.OnRefreshPlayerState();
						//default lackKind
					},delegate() {
						if(GameClient.Instance.MG!=null)
						{
							//UnityEngine.Debug.Log("!!!&&&&&&&&&"+GameClient.Instance.MG.GameInitDone);
						}
						return GameClient.Instance.MG!=null && GameClient.Instance.MG.GameInitDone;
					});
					#endif
				});

			//服务器通知玩家换牌
			_gsProxy.on(GameMessage.s2c_HuaiPaiDef, delegate (Message obj)
				{
					OdaoMessage msg = (OdaoMessage)obj;
					var serializer = MsgPack.Serialization.MessagePackSerializer.Get<GameMessage.HuanPaiDef>();
					var data = serializer.UnpackSingleObject(msg.data); 
					for(int i = 0; i < GameMessage.MAXHUANPAINUM; i++)
					{
						if(i < data.vHuanPai.Count)
						{
							TileDef def = TileDef.Create(data.vHuanPai[i]);
							UIOperation.Instance._HuanPaiOldDef[i] = def;
							Debug.Log("开始换牌["+i+"]:"+def.ToString());
						}
					}
					for(int i = 0; i < GameMessage.TABLE_PLAYER_NUM; i++)
					{
						UIOperation.Instance._headDatas[i].gameState = UIOperation.gameState.XUANPAI;
						UIOperation.Instance._headDatas[i].isShow = true;
					}

					Loom.QueueOnMainThread(delegate() {
						UIGameSetingController.Instance.OnRefreshPlayerState();
						MahjongTile.TotalClickCount = 3;
					}, delegate() {
						return GameClient.Instance.MG!=null && UIGameSetingController.Instance.IsOpen() && GameClient.Instance.MG.GameInitDone;
					});
				});

			//const int  SC_MSG_NOTIFY_TABLE_PLAYERS_COIN_CHANGE  = 59432;//当玩家取钱后更新玩家身上的钱
			//ADD
			//通知玩家有人定缺完毕
			//_gsProxy.on(GameMessage.SEND_SOMEONEDINGQUE_MSG, delegate (Message obj)
			//	{
			//		Debug.Log("SEND_SOMEONEDINGQUE_MSG");
			//	});

			//服务器的发牌请求
			_gsProxy.on(GameMessage.s2c_SendMjSerDef, delegate (Message obj)
				{
                    OdaoMessage msg = (OdaoMessage)obj;
#if USE_MSGPACK2
                    var serializer = MsgPack.Serialization.MessagePackSerializer.Get<GameMessage.SendMjSerDef>();
                    var data = serializer.UnpackSingleObject(msg.data);
#else
                    GameMessage.SendMjSerDef data = XConvert.ConvertToObject<GameMessage.SendMjSerDef>(msg.data);
#endif


                    Debug.Log("s2c_SendMjSerDef " + "椅子号"+data.tableNumExtra+"需要出牌" + data.draw + "#######" + data.specialType);
					/*
    public class SendMjSerDef
    {
        public byte cTableNumExtra;    //出牌的那个人
        public byte cGetMj;            //摸的那张牌 0表示没有从剩余的牌墙中取牌，也就是刚才的动作是碰，然后该出牌了 
        public int iSpeciaType;        //胡,暗杠,碰杠的二进制合
    }*/
                    //*只有胡和转弯杠*
                    GameClient.Instance.SpecialCard = data.draw;

					#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
					Loom.QueueOnMainThread(delegate() {
						GameClient.Instance.MG.TurnTo(data.tableNumExtra);
						// dont draw, because last operation is pong, now you should play
						if(data.draw == 0)
						{
						}
						else
						{
							TileDef def = TileDef.Create(data.draw);
							GameClient.Instance.MG.Draw(data.tableNumExtra, def);
						}

						//GameClient.Instance.SpecialCard = data.draw;

//						if(IsSpecialType(data.specialType, (byte)GameMessage.SPECIAL_TYPE.PASS))
//						{
//							UIGameComboController.Instance.OpenCombo(TileDef.ComboType.PASS,GameClient.Instance.MG.Self);
//						}
//
//						if(IsSpecialType(data.specialType,(byte)GameMessage.SPECIAL_TYPE.PONG))
//						{
//							UIGameComboController.Instance.OpenCombo(TileDef.ComboType.PONG,GameClient.Instance.MG.Self);
//						}
//
//						if(IsSpecialType(data.specialType,(byte)GameMessage.SPECIAL_TYPE.KONG))
//						{
//							UIGameComboController.Instance.OpenCombo(TileDef.ComboType.KONG_DARK,GameClient.Instance.MG.Self);
//						}
//
//						if(IsSpecialType(data.specialType,(byte)GameMessage.SPECIAL_TYPE.DARK_KONG))
//						{
//							UIGameComboController.Instance.OpenCombo(TileDef.ComboType.KONG_DARK,GameClient.Instance.MG.Self);
//						}
//
//						if(IsSpecialType(data.specialType,(byte)GameMessage.SPECIAL_TYPE.SPECIAL_TYPE_PENGGANG))
//						{
//							UIGameComboController.Instance.OpenCombo(TileDef.ComboType.KONG_TURN,GameClient.Instance.MG.Self);
//						}
//
//						if(IsSpecialType(data.specialType,(byte)GameMessage.SPECIAL_TYPE.WIN))
//						{
//							UIGameComboController.Instance.OpenCombo(TileDef.ComboType.WIN,GameClient.Instance.MG.Self);
//						}
						UIGameComboController.Instance.OpenAllCombos(data.specialType);
					},delegate() {
						return GameClient.Instance.MG != null;
					}
					);
					#endif
				});

			//服务器给玩家发送换的牌
			_gsProxy.on(GameMessage.s2c_Notice_HuanPaiDef, delegate (Message obj)
				{
					OdaoMessage msg = (OdaoMessage)obj;
					var serializer = MsgPack.Serialization.MessagePackSerializer.Get<GameMessage.HuanPaiResDef>();
					var data = serializer.UnpackSingleObject(msg.data); 
					MahjongTile.TotalClickCount = 1;
					for(int i = 0; i < GameMessage.MAXHUANPAINUM; i++)
					{
						if(i < data.cCards.Count)
						{
							TileDef def = TileDef.Create(data.cCards[i]);	
							UIOperation.Instance._HuanPaiNewDef[i] = def;
							Debug.Log("***********************换的牌："+def.ToString());
							Debug.Log("***********************删除的牌："+UIOperation.Instance._HuanPaiOldDef[i].ToString());
						}
					}
					List<TileDef> newPocketList = new List<TileDef>();
					int[] removeIndex = new int[GameMessage.MAXHUANPAINUM]{0, 0, 0};

					for(int i = 0; i < PocketList.Count; i++)
					{
						bool canAdd = true;
						for(int j = 0; j < GameMessage.MAXHUANPAINUM; j++)
						{
							if(PocketList[i].Value == UIOperation.Instance._HuanPaiOldDef[j].Value && removeIndex[j] == 0)
							{
								Debug.Log("**********************删除手牌："+UIOperation.Instance._HuanPaiOldDef[j].ToString());
								canAdd = false;
								removeIndex[j] = 1;
								break;
							}
						}
						if(canAdd)
						{
							newPocketList.Add(PocketList[i]);
						}
					}
					for(int i = 0; i < GameMessage.MAXHUANPAINUM; i++)
					{
						newPocketList.Add(UIOperation.Instance._HuanPaiNewDef[i]);
					}
					string oldStr = "*********************换牌前手牌"+PocketList.Count+":";
					foreach(var def in PocketList)
					{
						oldStr += def.ToString()+", ";
					}
					PocketList = newPocketList;
					string newStr = "*********************换牌后手牌"+PocketList.Count+":";
					foreach(var def in PocketList)
					{
						newStr += def.ToString()+", ";
					}

					Debug.Log(oldStr);
					Debug.Log(newStr);
					UIOperation.Instance._HuanPaiType = (GameMessage.HUANPAI_TYPE)data.iChangeType;

					Loom.QueueOnMainThread(delegate() {
						GameClient.Instance.MG.Self.ResetPocketList(PocketList.Count == GameMessage.HANDLE_MJ_NUM);
						UIGameSetingController.Instance.CloseHuanpaiTips();
					}, delegate() {
						return GameClient.Instance.MG != null;
					});
				});

			//通知客户端玩家的定缺牌
			_gsProxy.on(GameMessage.s2c_Notice_DingQueResDef, delegate (Message obj)
				{
                    int dataCount = 0;
#if USE_MSGPACK2
                    OdaoMessage msg = (OdaoMessage)obj;
                    var serializer = MsgPack.Serialization.MessagePackSerializer.Get<GameMessage.DingQueResDef>();
                    var data = serializer.UnpackSingleObject(msg.data);
                    dataCount = data.cques.Count;
#else
                    OdaoMessage msg = (OdaoMessage)obj;
					GameMessage.DingQueResDef data = XConvert.ConvertToObject<GameMessage.DingQueResDef>(msg.data);
                    dataCount = data.cques.Length;
#endif


					#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
                    for (int i = 0; i < dataCount; ++i)
					{
						TileDef.Kind k = (TileDef.Kind)data.cques[i];
						if(k != TileDef.Kind.NONE)
						{
							GameClient.Instance.MG.SetLackTile(i,k);
                            UIOperation.Instance._headDatas[i].dingque = k;
                            UIOperation.Instance._headDatas[i].gameState = UIOperation.gameState.NONE;
						}
                        
					}

					Loom.QueueOnMainThread(delegate() {
						GameClient.Instance.MG.UpdateLackState ();
                        UIGameSetingController.Instance.OnRefreshHeadAll();
						UIDQSettingController.Instance.Close();
					});
					#endif
				});

			//通知客户端玩家没钱了
			_gsProxy.on(GameMessage.SEND_NO_MONEY_MSG, delegate (Message obj)
				{
					Debug.Log("NotImplementedException : SEND_NO_MONEY_MSG");
				});

			//通知客户端 认输
			_gsProxy.on(GameMessage.SUB_S_QUEDINGRENSHU, delegate (Message obj)
				{
					Debug.Log("NotImplementedException : SUB_S_QUEDINGRENSHU");
				});

			//3：Server 发起客户端消息（服务器端发起,告诉桌上所有玩家）
			//服务器给所有玩家发牌
			_gsProxy.on(GameMessage.s2c_DealMjServerDef, delegate (Message obj)
            {
#if USE_MSGPACK2
                    OdaoMessage msg = (OdaoMessage)obj;
                    var serializer = MsgPack.Serialization.MessagePackSerializer.Get<GameMessage.DealMjServerDef>();
                    GameMessage.DealMjServerDef data = serializer.UnpackSingleObject(msg.data);

					PocketList.Clear();
					
                    for (int i = 0; i < data.cCards.Count; i++ )
                    {
                        var valueTemp = data.cCards[i];
                        Debug.Log(((valueTemp >> 4) & 0x0f) + ":" + (valueTemp & 0x0f) + "value:" + valueTemp);
                        if (TileDef.IsValid(valueTemp))
                        {
                            TileDef def = TileDef.Create(valueTemp);
                            PocketList.Add(def);
                        }
                    }

#else
                Debug.Log("s2c_DealMjServerDef***************************");
                OdaoMessage msg = (OdaoMessage)obj;
                GameMessage.DealMjServerDef data = XConvert.ConvertToObject<GameMessage.DealMjServerDef>(msg.data);
                PocketList.Clear();
                for (int i = 0; i < GameMessage.HANDLE_MJ_NUM; ++i)
                {
                    Debug.Log(((data.cCards[i] >> 4) & 0x0f) + ":" + (data.cCards[i] & 0x0f));
                    if (TileDef.IsValid(data.cCards[i]))
                    {
                        TileDef def = TileDef.Create(data.cCards[i]);
                        PocketList.Add(def);
                    }
                }
#endif


#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
                //BUG
					UnityEngine.Debug.Log("DEAL_CARD_SERVER_MSG---->>>>"+ToString());
					SortPocketList(TileDef.Comparison);
					UnityEngine.Debug.Log("DEAL_CARD_SERVER_MSG---->>>> SORTED "+ToString());
					Loom.QueueOnMainThread(delegate() {
						GameClient.Instance.MG.DealerIndex = data.cZhuang;
						GameClient.Instance.MG.StartRun();
						UIGameSetingController.Instance.OnRefreshHeadZhuangState();
					},delegate() {
						return GameClient.Instance.MG != null;
					}
					);
					#endif
					
				});

			//match 返回，换桌也会返回
			_gsProxy.on (GameMessage.s2c_MatchGameRes, delegate(Message obj) {
				Debug.Log("MatchGameRes");
				OdaoMessage msg = (OdaoMessage)obj;
				GameMessage.MatchGameRes data= XConvert.ConvertToObject<GameMessage.MatchGameRes>(msg.data);

				#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
				if(data.errorCode<0)
				{
					Debug.Log("ErrorCode " + data.errorCode);
				}

				Loom.QueueOnMainThread(()=> {

					UIOperation.Instance.Matching = false;

					for(int i=0; i<data.playerIndex.Length; ++i)
					{
						if(data.playerIndex[i] == Info.UserId)
						{
							if(!GameClient.Instance.IsInMahjongGame())
							{
								Index = i;
								UIOperation.Instance.SelfIndex = Index;
								GameLoading.SwitchScene(3);
							}
							else
							{
								GameClient.Instance.MG.ArrangePlayer(i,4);
							}
							break;
						}
					}
				});
				#endif
			});

			_gsProxy.on (GameMessage.s2c_GameResultServerDef, delegate(Message obj) {
				#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
				UnityEngine.Debug.Log("NotImplementedException : s2c_GameResultServerDef ");
				OdaoMessage msg = (OdaoMessage)obj;
				var serializer = MsgPack.Serialization.MessagePackSerializer.Get<GameMessage.GameResultServerDef>();
				GameMessage.GameResultServerDef data = serializer.UnpackSingleObject(msg.data);
				UIOperation.Instance._gameResultDatas = data.GameResult;
				UIOperation.Instance.Reset();
				Loom.QueueOnMainThread(delegate() {
					UIGameResultController.Instance.Open();
					UIGameHuPromptController.Instance.Close();
					for(int i = 0; i < GameMessage.TABLE_PLAYER_NUM; i++)
					{
						GameClient.Instance.MG.ShowPocketList(i);
					}
					UIGameEffectController.Instance.DespawnAllEffect ();

				},delegate() {
					return GameClient.Instance.MG != null;
				}
				);

				#endif
			});

			//离开游戏，换桌返回（换桌还会返回0x22;）其他玩家离开也会收到此消息
			_gsProxy.on (GameMessage.s2c_Notice_LeaveTableDef, delegate(Message obj) {
				#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
				UnityEngine.Debug.Log("======================= NotImplementedException : s2c_Notice_LeaveTableDef ======================= ");
				OdaoMessage msg = (OdaoMessage)obj;
				var serializer = MsgPack.Serialization.MessagePackSerializer.Get<GameMessage.NoticeLeaveTableDef>();
				GameMessage.NoticeLeaveTableDef data = serializer.UnpackSingleObject(msg.data);


				Loom.QueueOnMainThread(delegate() {
					if((int)data.cLeaveType == (int)GameMessage.LEAVETABLE_TYPE.LEAVETABLE_LEAVE){
						if(data.iUserID == Info.UserId && GameClient.Instance.IsInMahjongGame())
						{
							UIGameSetingController.Instance.Close();
							UIGameResultController.Instance.Close();
							GameLoading.SwitchScene(2);
						}
					}
				},delegate() {
					return true;
				}
				);

				#endif
			});
			//胡牌提示返回，每次发手牌会收到此消息
			_gsProxy.on (GameMessage.s2c_HuTipsResDef, delegate(Message obj) {
				//#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
				UnityEngine.Debug.Log("======================= NotImplementedException : s2c_HuTipsResDef ======================= ");
				OdaoMessage msg = (OdaoMessage)obj;
				var serializer = MsgPack.Serialization.MessagePackSerializer.Get<GameMessage.AllOutCard>();
				GameMessage.AllOutCard data = serializer.UnpackSingleObject(msg.data);
				UIOperation.Instance._huTipsCards = data.v_OutCards;
				if(UIOperation.Instance._huTipsCards.Count > 0)
				{
					foreach(var card in data.v_OutCards)
					{
						TileDef def = TileDef.Create(card.cOutCard);
						string ss = "******************************打出：" + def.ToString() + "胡：";
						foreach(var tips in card.v_Tips)
						{
							TileDef def1 = TileDef.Create(tips.cCard);

							ss += def1.ToString()+"，倍数"+tips.cTimes+",张数："+tips.cLeftNum+"------";
						}
						Debug.Log(ss);
					}
				}
				Loom.QueueOnMainThread(delegate() {
					
				},delegate() {
					return GameClient.Instance.MG != null;
				}
				);

				//#endif
			});
			//通知客户端显示按钮(血战到底会收到此消息)
			_gsProxy.on (GameMessage.s2c_LeaveTableDef, delegate(Message obj) {
				//#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
				UnityEngine.Debug.Log("======================= NotImplementedException : s2c_LeaveTableDef ======================= ");
				OdaoMessage msg = (OdaoMessage)obj;
				var serializer = MsgPack.Serialization.MessagePackSerializer.Get<GameMessage.LeaveTableDef>();
				GameMessage.LeaveTableDef data = serializer.UnpackSingleObject(msg.data);

				Loom.QueueOnMainThread(delegate() {

				},delegate() {
					return GameClient.Instance.MG != null;
				}
				);

				//#endif
			});

			_gsProxy.on (0x57, delegate(Message obj) {

                List<GameMessage.TEST_MSGPACK_PlayerCard> cardList = new List<GameMessage.TEST_MSGPACK_PlayerCard>();
                List<byte> m_vCType = new List<byte>();
                List<int> m_viType = new List<int>();
                List<long> m_vLLType = new List<long>();
                List<GameMessage.TEST_MSGPACK_TableNum> m_vPosition = new List<GameMessage.TEST_MSGPACK_TableNum>();
                GameMessage.TEST_MSGPACK testMsgPack = new GameMessage.TEST_MSGPACK();
                testMsgPack.cType = 0;
                testMsgPack.iType = 1;
                testMsgPack.llType = 2;
                testMsgPack.m_allPlayerCard = cardList;
                testMsgPack.m_string = "ok";
                testMsgPack.m_vCType = m_vCType;
                testMsgPack.m_viType = m_viType;
                testMsgPack.m_vLLType = m_vLLType;
                testMsgPack.m_vPosition = m_vPosition;
                testMsgPack.m_vusType = new List<ushort> { 1,2,3,4};
                testMsgPack.usType = 3;




                OdaoMessage msg = (OdaoMessage)obj;

				var serializer = MsgPack.Serialization.MessagePackSerializer.Get<GameMessage.TEST_MSGPACK> ();



                var recvbuffer = new System.IO.MemoryStream(msg.data);
                var test = serializer.Unpack(recvbuffer);

                var test2 = serializer.UnpackSingleObject(msg.data);
                Debug.Log(test);
                Debug.Log("****************MessagePack StringTest:" + test2.m_string + ", len:" + test2.m_string.Length);
                //				byte[] strByte = Encoding.Unicode.GetBytes(test2.m_string);
                //				foreach(var temp in strByte)
                //				{
                //					Debug.Log("****:"+temp);
                //				}

                var sendbuffer = new System.IO.MemoryStream();
				serializer.Pack(sendbuffer, testMsgPack);
				byte[] t = serializer.PackSingleObject(testMsgPack);
				byte[] a = sendbuffer.GetBuffer();
				sendbuffer.Position=0;
				byte[] b = sendbuffer.GetBuffer();
				/*
				for(int i=0;i<107;++i)
				{
					if(t[i]!=msg.data[i])
					{
						UnityEngine.Debug.LogError("byte match error  " + i);
					}

					if(a[i]!=msg.data[i])
					{
						UnityEngine.Debug.LogError("AAA byte match error  " + i);
					}

					if(b[i]!=msg.data[i])
					{
						UnityEngine.Debug.LogError("BBB byte match error  " + i);
					}
				}
				*/
				_gsProxy.notifyMP(0x57, t);

				/*
				GameMessage.TEST_RAW data= XConvert.ConvertToObject<GameMessage.TEST_RAW>(msg.data);

				for(int i=0;i<msg.data.Length;++i)
				{
					//Debug.Log(string.Format("{0：x}", msg.data[i]));
					UnityEngine.Debug.Log(msg.data[i]);
				}
				UnityEngine.Debug.Log("=========================================");

				byte[] list = XConvert.ConvertToByte(data);
				for(int i=0;i<list.Length;++i)
				{
					UnityEngine.Debug.Log(list[i]);
				}
				_gsProxy.notify(0x57, list);
				*/
			});
		}

	}
}
