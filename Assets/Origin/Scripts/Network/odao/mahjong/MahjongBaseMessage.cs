using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using NetworkInterface;
using odao.scmahjong;
using SevenZip.Buffer;
using UnityEngine;
using UnityEngine.SceneManagement;
//using MahjongPackManager = odao.scmahjong.NetworkPlayer;

public partial class BaseMessage {

    public const ushort LOGIN_DISPENSE_REQ = 0xA4;
    public const ushort LOGIN_REDISPENSE_REQ = 0xAC01;
    public const ushort LOGIN_DISPENSE_RES = 0xA5;

    public const ushort LOGIN_ROOM_SERVER_REQ = 0xA0;
    public const ushort LOGIN_GET_CHANNELINFO_REQ = 0xE806;
    public const ushort LOGIN_ROOM_SERVER_RES = 0xA1;

    public const ushort GAME_MATCH_REQ = 0x19;
    //public const ushort GAME_MATCH_RES = 0xA6;
    //public const ushort GAME_MATCH_EXTRA_RES = 0xAA;

    public const ushort GAME_MATCH_SUCCESS_RES = 0x22;
    public const ushort GAME_SELFINFO_RES = 0x40;

    public const ushort GAME_START_EXCHANGE_RES = 0x34; //开始换牌倒计时
    public const ushort GAME_EXCHANGE_CARDS_REQ = 0x14; //发送换牌
    public const ushort GAME_EXCHANGE_CARDS_RES = 0x45; //获取换牌信息
    //public const ushort GAME_EXCHANGE_CARDS_TABLE_INFO_RES = 0x48; //获取换牌信息


    public const ushort GAME_START_FIX_RES = 0x33; //开始定缺倒计时
    public const ushort GAME_FIX_TYPE_REQ = 0x13; //发送定缺
    public const ushort GAME_FIX_TYPE_RES = 0x47; //获取定缺信息

    public const ushort GAME_GET_CARD_RES = 0x43; //抓牌
    public const ushort GAME_PLAY_CARD_RES = 0x31; //出牌

    public const ushort GAME_PENG_GANG_HU_POP_RES = 0x44; //碰杠胡提示
    public const ushort GAME_PENG_GANG_HU_REQ = 0x12; //打出碰杠胡
    public const ushort GAME_PENG_GANG_HU_INFO_RES = 0x32; //碰杠胡数据刷新


    public const ushort GAME_BALANCE_RES = 0xB9; //结算




    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class DispenseReqDef
    {
        public DispenseReqDef()
        {
            
        }
        public int iUserID;
        public ushort usGameID;
        public ushort usLevel;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class RedispenseReqDef
    {
        public RedispenseReqDef()
        {

        }
        public int iUserID;
        public ushort usGameID;
        public ushort usLevel;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class DispenseResDef
    {
        public DispenseResDef()
        {

        }
        public sbyte cError;
        public int iUserID;
        public ushort usGameID;
        public ushort usServerID;
        public ushort usLevel;
        [OdaoFieldAttribute(32)]
        public string szServerIP;
        public uint ulServerPort;
        [OdaoFieldAttribute(32)]
        public string szDsnInfo;
    }
    //[OdaoFieldAttribute(length)] string


    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class LoginRoomServerReqDef
    {
        public LoginRoomServerReqDef()
        {

        }
        public int iUserID;
        public int iRoomID;
        [OdaoFieldAttribute(33)]
        public string szPasswd;
        public sbyte cLoginType;
        public int iClientSiteType;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class GetChannelInfoReqDef
    {
        public GetChannelInfoReqDef()
        {

        }
        [OdaoFieldAttribute(32)]
        public string channelId;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class LoginRoomServerResDef
    {
        public LoginRoomServerResDef()
        {

        }
        public sbyte cError;
        public int iPlayServerID;
        public int iPlayGameID;
        public long lFirstMoney;
        public uint ulCurLevelExperience;
        public uint ulNextLevelExperience;
        public int iSingalGameExperience;
        public int iUserID;            // "value_type": "OP_INT",     
        public int iSecondMony;        // "value_type": "OP_INT",     
        public uint lThirdMoney;      //  "value_type": "OP_LONG",    
        public int iYBNum;             // "value_type": "OP_INT;     
        public int iPrizeNum;          // "value_type": "OP_INT;     
        public int iAreaID;            // "value_type": "OP_INT;     
        public int iMatchTicket;       // "value_type": "OP_INT;     
        public int iCompeteAmount;     // "value_type": "OP_INT;     
        public int iWinNum;            // "value_type": "OP_INT;     
        public int iLostNum;           // "value_type": "OP_INT;     
        public int iDrawNum;           // "value_type": "OP_INT;     
        public int iDisNum;            // "value_type": "OP_INT;     
        public int iLastResult;        // "value_type": "OP_INT;     
        public uint iPhotoKey;         //  "value_type": "OP_UINT;    
        public ushort iTableNum;       //    "value_type": "OP_USHORT;  
        public ushort usIconNum;       //    "value_type": "OP_USHORT;  
        public ushort usTableNumExtra; //    "value_type": "OP_USHORT;  
        public ushort usExpLevel;      //    "value_type": "OP_USHORT;  
        public sbyte cSexType;         //   "value_type": "OP_CHAR;    
        public sbyte cVipType;         //   "value_type": "OP_CHAR;    
        public sbyte cPhotoVerify;     //   "value_type": "OP_CHAR;    
        public sbyte cMasterType;      //   "value_type": "OP_BYTE;    
        public sbyte ucSpecialIdentify;//   "value_type": "OP_BYTE;    
        public sbyte cUserStatus;      //   "value_type": "OP_CHAR;    
        public sbyte cIfReady;         //   "value_type": "OP_CHAR;    
        public sbyte cDisconnectType;  //   "value_type": "OP_CHAR;    
        [OdaoFieldAttribute(32)]       //
        public string szUserName;      //    "value_type": "OP_CS;      
        [OdaoFieldAttribute(32)]       //
        public string szNickName;      //    "value_type": "OP_CS;      
        [OdaoFieldAttribute(32)]       //
        public string szAreaName;      //    "value_type": "OP_CS;      
        public sbyte cIsBindMobile;    //   "value_type": "OP_CHAR;    
        public sbyte cIsVisitor;       //   "value_type": "OP_CHAR;    
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class GameMatchReqDef
    {
        public GameMatchReqDef()
        {

        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class GameMatchResDef
    {
        public GameMatchResDef()
        {

        }
        public ushort iTableNum;
        public ushort cError;
        public ushort usTableNumExtra;
        public sbyte cPlayerNum;
        public PlayerInfoExtraDef playerInfoExtraDef;
    }

    public class PlayerInfoExtraDef
    {
        public long lFirstMoney;          
        public uint ulCurLevelExperience; 
        public uint ulNextLevelExperience;
        public int iSingalGameExperience; 
        public int iUserID;               
        public int iSecondMony;           
        public int lThirdMoney;           
        public int iYBNum;                
        public int iPrizeNum;             
        public int iAreaID;               
        public int iMatchTicket;          
        public int iCompeteAmount;        
        public int iWinNum;               
        public int iLostNum;              
        public int iDrawNum;              
        public int iDisNum;               
        public int iLastResult;           
        public uint iPhotoKey;            
        public ushort iTableNum;          
        public ushort usIconNum;          
        public ushort usTableNumExtra;    
        public ushort usExpLevel;         
        public sbyte cSexType;            
        public sbyte cVipType;            
        public sbyte cPhotoVerify;        
        public sbyte cMasterType;         
        public sbyte ucSpecialIdentify;   
        public sbyte cUserStatus;         
        public sbyte cIfReady;            
        public sbyte cDisconnectType;
        [OdaoFieldAttribute(32)]
        public string szUserName;
        [OdaoFieldAttribute(32)]
        public string szNickName;
        [OdaoFieldAttribute(20)]
        public string szAreaName;         
        public sbyte cIsBindMobile;       
        public sbyte cIsVisitor;           
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class GameMatchReadyResDef
    {
        public GameMatchReadyResDef()
        {

        }
        public ushort usTableNumExtra;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class GameMatchSuccessResDef
    {
        public GameMatchSuccessResDef()
        {

        }
        public int errorCode;
        public int playerIndex_1;
        public int playerIndex_2;
        public int playerIndex_3;
        public int playerIndex_4;
        public long room_tax;
        public int iBankerIndex;
        [OdaoFieldAttribute(500)]
        public string imageurl_1;
        [OdaoFieldAttribute(500)]
        public string imageurl_2;
        [OdaoFieldAttribute(500)]
        public string imageurl_3;
        [OdaoFieldAttribute(500)]
        public string imageurl_4;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class GameSelfInfoResDef
    {
        public GameSelfInfoResDef()
        {

        }
        public sbyte BankerID;
        [OdaoFieldAttribute(14)]
        public sbyte[] cCards;
        public sbyte BankerRemain;
        [OdaoFieldAttribute(2)]
        public sbyte[] dices;
        public int iAllCardNum;
        public int iBaseTimes;
        public long llNowMoney;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class GameStartExchangeResDef
    {
        public GameStartExchangeResDef()
        {

        }
        public sbyte hadDo;
        public sbyte time_left;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class GameExchangeCardsReqDef
    {
        public GameExchangeCardsReqDef()
        {

        }
        public sbyte HuanPai_1;
        public sbyte HuanPai_2;
        public sbyte HuanPai_3;
        public sbyte iCardNum;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class GameExchangeCardsTableInfoResDef
    {
        public GameExchangeCardsTableInfoResDef()
        {

        }
        public sbyte cTableNumExtra;
        public sbyte iCardNum;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class GameExchangeCardsResDef
    {
        public GameExchangeCardsResDef()
        {

        }
        [OdaoFieldAttribute(3)]
        public sbyte[] sCards;
        [OdaoFieldAttribute(3)]
        public sbyte[] cCards;
        public sbyte iCardNum;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class GameStartFixResDef
    {
        public GameStartFixResDef()
        {

        }
        public sbyte hadDo;
        public sbyte time_left;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class GameFixTypeReqDef
    {
        public GameFixTypeReqDef()
        {

        }
        public sbyte cDingqueCard;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class GameFixTypeResDef
    {
        public GameFixTypeResDef()
        {

        }
        [OdaoFieldAttribute(4)]
        public sbyte[] cques;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class GameGetCardResDef
    {
        public GameGetCardResDef()
        {

        }
        public sbyte cTableNumExtra;
        public sbyte cGetMj;
        public int iSpeciaType;
        public sbyte time_left;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class GamePlayCardResDef
    {
        public GamePlayCardResDef()
        {

        }
        public sbyte cTableNumExtra;
        public sbyte cCard;
        public sbyte flag;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class GamePengGangHuPopRes
    {
        public GamePengGangHuPopRes()
        {

        }
        public sbyte cExtraTableNum;
        public int iSpecialTypeTotal;
        public sbyte time_left;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class GamePengGangHuReq
    {
        public GamePengGangHuReq()
        {

        }
        public sbyte cSpecialType;
        public sbyte cSpecialCards;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class GamePengGangHuInfoRes
    {
        public GamePengGangHuInfoRes()
        {

        }
        public sbyte cExtraTableNum;  
        public sbyte cSpecialType;    
        public sbyte cDianPaoer;      
        public sbyte cCard;           
        public ushort huType_1;       
        public ushort huType_2;       
        public ushort huType_3;       
        public ushort huType_4;       
        public ushort gangHuType_1;   
        public ushort gangHuType_2;   
        public ushort gangHuType_3;   
        public ushort gangHuType_4;   
        public int times_1;           
        public int times_2;           
        public int times_3;           
        public int times_4;           
        public long afterTax_1;       
        public long afterTax_2;       
        public long afterTax_3;       
        public long afterTax_4;       
        public long earnMoney_1;      
        public long earnMoney_2;      
        public long earnMoney_3;      
        public long earnMoney_4;          
    }

    
    public class WinLostScoreRecordDef
    {
        public WinLostScoreRecordDef()
        {

        }
        public int winLostType;
        public int huType;
        public int times;
        public int gen;
        public long winScore_1;
        public long winScore_2;
        public long winScore_3;
        public long winScore_4;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class GameBalanceRes
    {
        public GameBalanceRes()
        {

        }
        public long winScore_1;         
        public long winScore_2;         
        public long winScore_3;         
        public long winScore_4;         
        public long huWin_1;            
        public long huWin_2;            
        public long huWin_3;            
        public long huWin_4;            
        public long huLost_1;           
        public long huLost_2;           
        public long huLost_3;           
        public long huLost_4;           
        public long selfScore;          
        [OdaoFieldAttribute(14)]
        public sbyte[] handCars_1;      
        [OdaoFieldAttribute(14)]
        public sbyte[] handCars_2;      
        [OdaoFieldAttribute(14)]
        public sbyte[] handCars_3;      
        [OdaoFieldAttribute(14)]
        public sbyte[] handCars_4;
        [OdaoFieldAttribute(4)]
        public sbyte[] playerRecordOffset;
        public int recordCount;
        public WinLostScoreRecordDef winLostScoreRecordDef;
    }




}

namespace odao.scmahjong
{
    public partial class NetworkPlayer : Player
    { 
        public void DispenseSend()
        {
            BaseMessage.DispenseReqDef data = new BaseMessage.DispenseReqDef();
            data.iUserID = 20000;
            data.usGameID = 172;
            data.usLevel = 11;

            byte[] msg = XConvert.ConvertToByte(data);


            _gsProxy.notify(BaseMessage.LOGIN_DISPENSE_REQ, msg);
        }

        public void ReDispenseSend()
        {
            BaseMessage.RedispenseReqDef data = new BaseMessage.RedispenseReqDef();
            data.iUserID = 20000;
            data.usGameID = 172;
            data.usLevel = 11;

            byte[] msg = XConvert.ConvertToByte(data);


            _gsProxy.notify(BaseMessage.LOGIN_REDISPENSE_REQ, msg);
        }

        public void LoginToRoomServerSend()
        {
            BaseMessage.LoginRoomServerReqDef data = new BaseMessage.LoginRoomServerReqDef();
            data.iUserID = 20000;
            data.iRoomID = 102;
            data.szPasswd = "670b14728ad9902aecba32e22fa4f6bd";
            data.cLoginType = 2;
            data.iClientSiteType = 1;

            byte[] msg = XConvert.ConvertToByte(data);


            _gsProxy.notify(BaseMessage.LOGIN_ROOM_SERVER_REQ, msg);
        }

        public void GetChannelInfoSend()
        {
            BaseMessage.GetChannelInfoReqDef data = new BaseMessage.GetChannelInfoReqDef();
            data.channelId = "1621000000500000";

            byte[] msg = XConvert.ConvertToByte(data);


            _gsProxy.notify(BaseMessage.LOGIN_GET_CHANNELINFO_REQ, msg);
        }

        public void GameMatchSend()
        {
            BaseMessage.GameMatchReqDef data = new BaseMessage.GameMatchReqDef();

            byte[] msg = XConvert.ConvertToByte(data);


            _gsProxy.notify(BaseMessage.GAME_MATCH_REQ, msg);
        }

        public void GameExchangeCardsSend(sbyte HuanPai_1, sbyte HuanPai_2, sbyte HuanPai_3, sbyte iCardNum)
        {
            BaseMessage.GameExchangeCardsReqDef data = new BaseMessage.GameExchangeCardsReqDef();
            data.HuanPai_1 = HuanPai_1;
            data.HuanPai_2 = HuanPai_2;
            data.HuanPai_3 = HuanPai_3;
            data.iCardNum = iCardNum;
            byte[] msg = XConvert.ConvertToByte(data);


            _gsProxy.notify(BaseMessage.GAME_EXCHANGE_CARDS_REQ, msg);
        }


        public void GameFixTypeSend(sbyte cDingqueCard)
        {
            BaseMessage.GameFixTypeReqDef data = new BaseMessage.GameFixTypeReqDef();
            data.cDingqueCard = cDingqueCard;
            byte[] msg = XConvert.ConvertToByte(data);


            _gsProxy.notify(BaseMessage.GAME_FIX_TYPE_REQ, msg);
        }

        public void GamePengGangHuSend(sbyte cSpecialType,sbyte cSpecialCards)
        {
            BaseMessage.GamePengGangHuReq data = new BaseMessage.GamePengGangHuReq();
            data.cSpecialType = cSpecialType;
            data.cSpecialCards = cSpecialCards;
            byte[] msg = XConvert.ConvertToByte(data);


            _gsProxy.notify(BaseMessage.GAME_PENG_GANG_HU_REQ, msg);
        }














        public void InitCallBack()
        {
            _gsProxy.on(BaseMessage.LOGIN_DISPENSE_RES, delegate(Message obj)
            {
                OdaoMessage msg = (OdaoMessage) obj;
                BaseMessage.DispenseResDef data = XConvert.ConvertToObject<BaseMessage.DispenseResDef>(msg.data);

                Debug.LogError("LOGIN_BEST_ROOM_RES: " + BaseMessage.LOGIN_DISPENSE_RES);
                Debug.LogError("cError:" + data.cError);
                Debug.LogError("iUserID:" + data.iUserID);
                Debug.LogError("usGameID:" + data.usGameID);
                Debug.LogError("usServerID:" + data.usServerID);
                Debug.LogError("usLevel:" + data.usLevel);
                Debug.LogError("szServerIP:" + data.szServerIP);
                Debug.LogError("ulServerPort:" + data.ulServerPort);
                Debug.LogError("szDsnInfo:" + data.szDsnInfo);

                RoomData.szServerIP = data.szServerIP;
                RoomData.ulServerPort = data.ulServerPort;

                if (data.cError == 0)
                {
                    Loom.QueueOnMainThread(delegate () {
                        GameObject.Find("Canvas").transform.Find("Waiting").gameObject.SetActive(false);
                        HallDataManager.Instance.LoadScene();
                    });
                }
            });

            _gsProxy.on(BaseMessage.LOGIN_ROOM_SERVER_RES, delegate (Message obj)
            {
                OdaoMessage msg = (OdaoMessage)obj;
                BaseMessage.LoginRoomServerResDef data = XConvert.ConvertToObject<BaseMessage.LoginRoomServerResDef>(msg.data);

                Debug.LogError("LOGIN_BEST_ROOM_RES: " + BaseMessage.LOGIN_ROOM_SERVER_RES);
                Debug.LogError("cError:" + data.cError);
                Debug.LogError("iPlayServerID:" + data.iPlayServerID);
                Debug.LogError("usGameID:" + data.iPlayGameID);
                Debug.LogError("lFirstMoney:" + data.lFirstMoney);
                Debug.LogError("ulCurLevelExperience:" + data.ulCurLevelExperience);
                Debug.LogError("ulNextLevelExperience:" + data.ulNextLevelExperience);
                Debug.LogError("iSingalGameExperience:" + data.iSingalGameExperience);
                Debug.LogError("iUserID:" + data.iUserID);
                Debug.LogError("iSecondMony:" + data.iSecondMony);
                Debug.LogError("lThirdMoney:" + data.lThirdMoney);
                Debug.LogError("iYBNum:" + data.iYBNum);
                Debug.LogError("iPrizeNum:" + data.iPrizeNum);
                Debug.LogError("iAreaID:" + data.iAreaID);
                Debug.LogError("iMatchTicket:" + data.iMatchTicket);
                Debug.LogError("iCompeteAmount:" + data.iCompeteAmount);
                Debug.LogError("iWinNum:" + data.iWinNum);
                Debug.LogError("iLostNum:" + data.iLostNum);
                Debug.LogError("iDrawNum:" + data.iDrawNum);
                Debug.LogError("iDisNum:" + data.iDisNum);
                Debug.LogError("iLastResult:" + data.iLastResult);
                Debug.LogError("iPhotoKey:" + data.iPhotoKey);
                Debug.LogError("iTableNum:" + data.iTableNum);
                Debug.LogError("usIconNum:" + data.usIconNum);
                Debug.LogError("usTableNumExtra:" + data.usTableNumExtra);
                Debug.LogError("usExpLevel:" + data.usExpLevel);
                Debug.LogError("cSexType:" + data.cSexType);
                Debug.LogError("cVipType:" + data.cVipType);
                Debug.LogError("cPhotoVerify:" + data.cPhotoVerify);
                Debug.LogError("cMasterType:" + data.cMasterType);
                Debug.LogError("ucSpecialIdentify:" + data.ucSpecialIdentify);
                Debug.LogError("cUserStatus:" + data.cUserStatus);
                Debug.LogError("cIfReady:" + data.cIfReady);
                Debug.LogError("cDisconnectType:" + data.cDisconnectType);
                Debug.LogError("szUserName:" + data.szUserName);
                Debug.LogError("szNickName:" + data.szNickName);
                Debug.LogError("szAreaName:" + data.szAreaName);
                Debug.LogError("cIsBindMobile:" + data.cIsBindMobile);
                Debug.LogError("cIsVisitor:" + data.cIsVisitor);


                if (data.cError == 0)
                {

                    GameMatchSend();
                    Loom.QueueOnMainThread(delegate ()
                    {
                        //开始计时（匹配）
                        MahjongDisplay.Instance.ShowTime("匹配中...",0);
                    });
                }
            });


            //_gsProxy.on(BaseMessage.GAME_MATCH_RES, delegate (Message obj)
            //{
            //    OdaoMessage msg = (OdaoMessage)obj;
            //    BaseMessage.GameMatchResDef data = XConvert.ConvertToObject<BaseMessage.GameMatchResDef>(msg.data);

            //    Debug.LogError("GAME_MATCH_RES: " + BaseMessage.GAME_MATCH_RES);
            //    Debug.LogError("iTableNum:" + data.iTableNum);
            //    Debug.LogError("cError:" + data.cError);
            //    Debug.LogError("usTableNumExtra:" + data.usTableNumExtra);
            //    Debug.LogError("cPlayerNum:" + data.cPlayerNum);
            //    Debug.LogError("PlayerInfoExtraDef.lFirstMoney:" + data.playerInfoExtraDef.lFirstMoney);
            //    Debug.LogError("PlayerInfoExtraDef.ulCurLevelExperience:" + data.playerInfoExtraDef.ulCurLevelExperience);
            //    Debug.LogError("PlayerInfoExtraDef.iUserID:" + data.playerInfoExtraDef.iUserID);
            //    Debug.LogError("PlayerInfoExtraDef.iSecondMony:" + data.playerInfoExtraDef.iSecondMony);
            //    Debug.LogError("PlayerInfoExtraDef.szUserName:" + data.playerInfoExtraDef.szUserName);
            //    Debug.LogError("PlayerInfoExtraDef.szNickName:" + data.playerInfoExtraDef.szNickName);
            //    Debug.LogError("PlayerInfoExtraDef.iWinNum:" + data.playerInfoExtraDef.iWinNum);
            //    Debug.LogError("PlayerInfoExtraDef.iDisNum:" + data.playerInfoExtraDef.iDisNum);


            //    if (data.cError == 0)
            //    {
            //        Loom.QueueOnMainThread(delegate () {
            //        });
            //    }
            //});

            //_gsProxy.on(BaseMessage.GAME_MATCH_EXTRA_RES, delegate (Message obj)
            //{
            //    OdaoMessage msg = (OdaoMessage)obj;
            //    BaseMessage.GameMatchReadyResDef data = XConvert.ConvertToObject<BaseMessage.GameMatchReadyResDef>(msg.data);

            //    Debug.LogError("GAME_MATCH_EXTRA_RES: " + BaseMessage.GAME_MATCH_EXTRA_RES);
            //    Debug.LogError("usTableNumExtra:" + data.usTableNumExtra);
            //});


            _gsProxy.on(BaseMessage.GAME_MATCH_SUCCESS_RES, delegate (Message obj)
            {
                OdaoMessage msg = (OdaoMessage)obj;
                BaseMessage.GameMatchSuccessResDef data = XConvert.ConvertToObject<BaseMessage.GameMatchSuccessResDef>(msg.data);

                Debug.LogError("GAME_MATCH_SUCCESS_RES: " + BaseMessage.GAME_MATCH_SUCCESS_RES);
                Debug.LogError("errorCode:" + data.errorCode);
                Debug.LogError("playerIndex_1:" + data.playerIndex_1);
                Debug.LogError("playerIndex_2:" + data.playerIndex_2);
                Debug.LogError("playerIndex_3:" + data.playerIndex_3);
                Debug.LogError("playerIndex_4:" + data.playerIndex_4);
                Debug.LogError("room_tax:" + data.room_tax);
                Debug.LogError("iBankerIndex:" + data.iBankerIndex);
                Debug.LogError("imageurl_1:" + data.imageurl_1);
                Debug.LogError("imageurl_2:" + data.imageurl_2);
                Debug.LogError("imageurl_3:" + data.imageurl_3);
                Debug.LogError("imageurl_4:" + data.imageurl_4);

                //结束倒计时
                if (data.errorCode==0)
                {
                    Loom.QueueOnMainThread(delegate ()
                    {
                        MahjongDisplay.Instance.CloseTime();
                    });
                }
            });

            _gsProxy.on(BaseMessage.GAME_SELFINFO_RES, delegate (Message obj)
            {
                OdaoMessage msg = (OdaoMessage)obj;
                BaseMessage.GameSelfInfoResDef data = XConvert.ConvertToObject<BaseMessage.GameSelfInfoResDef>(msg.data);

                Debug.LogError("GAME_SELFINFO_RES: " + BaseMessage.GAME_SELFINFO_RES);
                Debug.LogError("BankerID:" + data.BankerID);
                for (int i = 0; i < 14; i++)
                {
                    Debug.LogError("cCards:" + data.cCards[i]);
                    //  val/10 = 0万1条2筒
                }
                Debug.LogError("BankerRemain:" + data.BankerRemain);
                Debug.LogError("dices:" + data.dices[0]);
                Debug.LogError("dices:" + data.dices[1]);
                Debug.LogError("iAllCardNum:" + data.iAllCardNum);
                Debug.LogError("iBaseTimes:" + data.iBaseTimes);
                Debug.LogError("llNowMoney:" + data.llNowMoney);

                //牌局开始
                Loom.QueueOnMainThread(delegate ()
                {
                    MahjongGameManager.Instance.GetHandCardData(data.cCards);
                });
            });

            _gsProxy.on(BaseMessage.GAME_START_EXCHANGE_RES, delegate (Message obj)
            {
                OdaoMessage msg = (OdaoMessage)obj;
                BaseMessage.GameStartExchangeResDef data = XConvert.ConvertToObject<BaseMessage.GameStartExchangeResDef>(msg.data);

                Debug.LogError("GAME_START_EXCHANGE_RES: " + BaseMessage.GAME_START_EXCHANGE_RES);
                Debug.LogError("hadDo:" + data.hadDo);
                Debug.LogError("time_left:" + data.time_left);
                Loom.QueueOnMainThread(delegate ()
                {
                    MahjongDisplay.Instance.ShowTime("请换牌",data.time_left);
                });

            });

            _gsProxy.on(BaseMessage.GAME_EXCHANGE_CARDS_RES, delegate (Message obj)
            {
                //return;
                OdaoMessage msg = (OdaoMessage)obj;
                BaseMessage.GameExchangeCardsResDef data = XConvert.ConvertToObject<BaseMessage.GameExchangeCardsResDef>(msg.data);

                Debug.LogError("GAME_EXCHANGE_CARDS_RES: " + BaseMessage.GAME_EXCHANGE_CARDS_RES);
                for (int i = 0; i < data.sCards.Length; i++)
                {
                    Debug.LogError("sCards:" + data.sCards[i]);
                }
                for (int i = 0; i < data.cCards.Length; i++)
                {
                    Debug.LogError("cCards:" + data.cCards[i]);
                }
                Debug.LogError("iCardNum:" + data.iCardNum);
                
                
            });

            //_gsProxy.on(BaseMessage.GAME_EXCHANGE_CARDS_TABLE_INFO_RES, delegate (Message obj)
            //{
            //    OdaoMessage msg = (OdaoMessage)obj;
            //    BaseMessage.GameExchangeCardsTableInfoResDef data = XConvert.ConvertToObject<BaseMessage.GameExchangeCardsTableInfoResDef>(msg.data);

            //    Debug.LogError("GAME_EXCHANGE_CARDS_TABLE_INFO_RES: " + BaseMessage.GAME_EXCHANGE_CARDS_TABLE_INFO_RES);

            //    Debug.LogError("cTableNumExtra:" + data.cTableNumExtra);
            //    Debug.LogError("iCardNum:" + data.iCardNum);

            //    Loom.QueueOnMainThread(delegate ()
            //    {

            //    });
            //});


            _gsProxy.on(BaseMessage.GAME_START_FIX_RES, delegate (Message obj)
            {
                OdaoMessage msg = (OdaoMessage)obj;
                BaseMessage.GameStartFixResDef data = XConvert.ConvertToObject<BaseMessage.GameStartFixResDef>(msg.data);

                Debug.LogError("GAME_START_FIX_RES: " + BaseMessage.GAME_START_FIX_RES);

                Debug.LogError("hadDo:" + data.hadDo);
                Debug.LogError("time_left:" + data.time_left);

                Loom.QueueOnMainThread(delegate ()
                {
                    MahjongDisplay.Instance.ShowTime("请定缺", data.time_left);
                });
            });


            _gsProxy.on(BaseMessage.GAME_FIX_TYPE_RES, delegate (Message obj)
            {
                OdaoMessage msg = (OdaoMessage)obj;
                BaseMessage.GameFixTypeResDef data = XConvert.ConvertToObject<BaseMessage.GameFixTypeResDef>(msg.data);

                Debug.LogError("GAME_FIX_TYPE_RES: " + BaseMessage.GAME_FIX_TYPE_RES);

                for (int i = 0; i < 3; i++)
                {
                    Debug.LogError("cques:" + data.cques[i]);
                }

                Loom.QueueOnMainThread(delegate ()
                {

                });
            });


            _gsProxy.on(BaseMessage.GAME_GET_CARD_RES, delegate (Message obj)
            {
                OdaoMessage msg = (OdaoMessage)obj;
                BaseMessage.GameGetCardResDef data = XConvert.ConvertToObject<BaseMessage.GameGetCardResDef>(msg.data);

                Debug.LogError("GAME_GET_CARD_RES: " + BaseMessage.GAME_GET_CARD_RES);


                Debug.LogError("cTableNumExtra:" + data.cTableNumExtra);
                Debug.LogError("cGetMj:" + data.cGetMj);
                Debug.LogError("iSpeciaType:" + data.iSpeciaType);
                Debug.LogError("time_left:" + data.time_left);

                Loom.QueueOnMainThread(delegate ()
                {

                });
            });


            _gsProxy.on(BaseMessage.GAME_PLAY_CARD_RES, delegate (Message obj)
            {
                OdaoMessage msg = (OdaoMessage)obj;
                BaseMessage.GamePlayCardResDef data = XConvert.ConvertToObject<BaseMessage.GamePlayCardResDef>(msg.data);

                Debug.LogError("GAME_PLAY_CARD_RES: " + BaseMessage.GAME_PLAY_CARD_RES);

                Debug.LogError("cTableNumExtra:" + data.cTableNumExtra);
                Debug.LogError("cCard:" + data.cCard);
                Debug.LogError("flag:" + data.flag);

                Loom.QueueOnMainThread(delegate ()
                {

                });
            });


            _gsProxy.on(BaseMessage.GAME_PENG_GANG_HU_POP_RES, delegate (Message obj)
            {
                OdaoMessage msg = (OdaoMessage)obj;
                BaseMessage.GamePengGangHuPopRes data = XConvert.ConvertToObject<BaseMessage.GamePengGangHuPopRes>(msg.data);

                Debug.LogError("GAME_PENG_GANG_HU_POP_RES: " + BaseMessage.GAME_PENG_GANG_HU_POP_RES.ToString("x8"));

                Debug.LogError("cExtraTableNum:" + data.cExtraTableNum);
                Debug.LogError("iSpecialTypeTotal:" + data.iSpecialTypeTotal);
                Debug.LogError("time_left:" + data.time_left);

                Loom.QueueOnMainThread(delegate ()
                {

                });
            });

            _gsProxy.on(BaseMessage.GAME_PENG_GANG_HU_INFO_RES, delegate (Message obj)
            {
                OdaoMessage msg = (OdaoMessage)obj;
                BaseMessage.GamePengGangHuInfoRes data = XConvert.ConvertToObject<BaseMessage.GamePengGangHuInfoRes>(msg.data);

                Debug.LogError("GAME_PENG_GANG_HU_INFO_RES: " + BaseMessage.GAME_PENG_GANG_HU_INFO_RES.ToString("x8"));

                Debug.LogError("cExtraTableNum:" + data.cExtraTableNum);
                Debug.LogError("iSpecialTypeTotal:" + data.cSpecialType);
                Debug.LogError("iSpecialTypeTotal:" + data.cDianPaoer);
                Debug.LogError("time_left:" + data.cCard);
                Debug.LogError("huType_1:"  + data.huType_1);
                Debug.LogError("huType_2:"  + data.huType_2);
                Debug.LogError("huType_3:"  + data.huType_3);
                Debug.LogError("huType_4:"  + data.huType_4);
                Debug.LogError("gangHuType_1:"  + data.gangHuType_1);
                Debug.LogError("gangHuType_2:"  + data.gangHuType_2);
                Debug.LogError("gangHuType_3:"  + data.gangHuType_3);
                Debug.LogError("gangHuType_4:"  + data.gangHuType_4);
                Debug.LogError("times_1:"  + data.times_1);
                Debug.LogError("times_2:"  + data.times_2);
                Debug.LogError("times_3:"  + data.times_3);
                Debug.LogError("times_4:"  + data.times_4);
                Debug.LogError("afterTax_1:"  + data.afterTax_1);
                Debug.LogError("afterTax_2:"  + data.afterTax_2);
                Debug.LogError("afterTax_3:"  + data.afterTax_3);
                Debug.LogError("afterTax_4:"  + data.afterTax_4);
                Debug.LogError("earnMoney_1:"  + data.earnMoney_1);
                Debug.LogError("earnMoney_2:"  + data.earnMoney_2);
                Debug.LogError("earnMoney_3:"  + data.earnMoney_3);
                Debug.LogError("earnMoney_4:" + data.earnMoney_4);

                Loom.QueueOnMainThread(delegate ()
                {
                    
                });
            });



            _gsProxy.on(BaseMessage.GAME_BALANCE_RES, delegate (Message obj)
            {
                OdaoMessage msg = (OdaoMessage)obj;
                BaseMessage.GameBalanceRes data = XConvert.ConvertToObject<BaseMessage.GameBalanceRes>(msg.data);

                Debug.LogError("GAME_BALANCE_RES: " + BaseMessage.GAME_BALANCE_RES.ToString("x8"));

                Debug.LogError("winScore_1:" + data.winScore_1);
                Debug.LogError("winScore_2:" + data.winScore_2);
                Debug.LogError("winScore_3:" + data.winScore_3);
                Debug.LogError("winScore_4:" + data.winScore_4);
                Debug.LogError("huWin_1:" + data.huWin_1);
                Debug.LogError("huWin_2:" + data.huWin_2);
                Debug.LogError("huWin_3:" + data.huWin_3);
                Debug.LogError("huWin_4:" + data.huWin_4);
                Debug.LogError("huLost_1:" + data.huLost_1);
                Debug.LogError("huLost_2:" + data.huLost_2);
                Debug.LogError("huLost_3:" + data.huLost_3);
                Debug.LogError("huLost_4:" + data.huLost_4);
                Debug.LogError("selfScore:" + data.selfScore);

                foreach (var card in data.handCars_1)
                {
                    Debug.LogError("handCars_1:" + card);
                }
                foreach (var card in data.handCars_2)
                {
                    Debug.LogError("handCars_2:" + card);
                }
                foreach (var card in data.handCars_3)
                {
                    Debug.LogError("handCars_3:" + card);
                }
                foreach (var card in data.handCars_4)
                {
                    Debug.LogError("handCars_4:" + card);
                }
                foreach (var val in data.playerRecordOffset)
                {
                    Debug.LogError("playerRecordOffset:" + val);
                }
                //Debug.LogError("handCars_1:" + data.handCars_1);
                //Debug.LogError("handCars_2:" + data.handCars_2);
                //Debug.LogError("handCars_3:" + data.handCars_3);
                //Debug.LogError("handCars_4:" + data.handCars_4);
                //Debug.LogError("playerRecordOffset:" + data.playerRecordOffset);
                Debug.LogError("recordCount:" + data.recordCount);


                Debug.LogError("winLostType:" + data.winLostScoreRecordDef.winLostType);
                Debug.LogError("huType:" + data.winLostScoreRecordDef.huType);
                Debug.LogError("times:" + data.winLostScoreRecordDef.times);
                Debug.LogError("gen:" + data.winLostScoreRecordDef.gen);
                Debug.LogError("winScore_1:" + data.winLostScoreRecordDef.winScore_1);
                Debug.LogError("winScore_2:" + data.winLostScoreRecordDef.winScore_2);
                Debug.LogError("winScore_3:" + data.winLostScoreRecordDef.winScore_3);
                Debug.LogError("winScore_4:" + data.winLostScoreRecordDef.winScore_4);



                Loom.QueueOnMainThread(delegate ()
                {

                });
            });


            
        }
    }

    public class RoomData
    {
        public static string szServerIP { get; set; }
        public static uint ulServerPort { get; set; }
    }
}
