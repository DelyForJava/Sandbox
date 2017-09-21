
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using MsgPack;
using MsgPack.Serialization;

namespace CPP
{
    public class GameMessage
    {
        public const int TABLE_PLAYER_NUM = 4;
        public const int MAXHUANPAINUM = 3;
        public const int HANDLE_MJ_NUM = 14;
        public const int GAME_ALL_MJ_NUM = 108;

        //没有将0加入，0有特殊意义。SPECIAL_TYPE_GETNEW 该动作不会发往客户端
        public enum SPECIAL_TYPE
        {
            //SPECIAL_TYPE_GETNEW 	= 0x01,    //从牌墙中获取一张新牌，不应记录这一动作，可让lastAction为0来表征
            PASS = 0x01,    //玩家放弃某一动作，比如可胡、可杠
            PONG = 0x02,    //可碰
            KONG = 0x04,       //明杠
            DARK_KONG = 0x08,     //暗杠
            SPECIAL_TYPE_PENGGANG = 0x10,     //碰杠，面下杠
            WIN = 0x20,     //胡
        };

        public enum WINLOST_TYPE
        {
            WINLOST_ZIMO = 1, //  自摸  
            WINLOST_BEI_ZIMO,//被自摸
            WINLOST_DIANPAO,//点炮
            WINLOST_DIANPAOHU,//点炮胡
            WINLOST_QIANGGANGHU,//抢杠胡
            WINLOST_BEI_QIANGGANGHU,//被抢杠胡
            WINLOST_GANGSHANGHUA,//杠上开花
            WINLOST_GANGSHANGPAO,//杠上炮
            WINLOST_BEI_GANGSHANGPAO,
            WINLOST_GUAFENG_ZHI,//明杠
            WINLOST_GUAFENG_XIA,//碰杠
            WINLOST_BEI_GUAFENG_ZHI,//被明杠
            WINLOST_BEI_GUAFENG_XIA,//被碰杠
            WINLOST_XIAYU,//暗杠
            WINLOST_BEI_XIAYU,//被暗杠
            WINLOST_HUAZHU,//花猪
            WINLOST_CHAHUAZHU,//查花猪
            WINLOST_CHAJIAO,//查叫
            WINLOST_BEI_CHAJIAO,//被查叫
        };

        public enum SCMJ_HU_TYPE
        {
            HU_NOHU = 0,
            HU_TIANHU,     // 6 fans
            HU_DIHU,
            HU_QINGLONG7DUI,
            HU_18LUOHAN,
            HU_LONG7DUI, // 5 fans
            HU_QING7DUI,
            HU_QING19,
            HU_JIANGJINGOUDIAO,
            HU_QINGJINGOUDIAO,
            HU_QINGDUI,     // 4 fans
            HU_JIANGDUI,
            HU_QING1SE, // 3 fans清一色
            HU_DAI19,
            HU_7DUI,
            HU_JINGOUDIAO,
            HU_DUIDUIHU,    // 2 fans大对子
            HU_PINGHU,      // 1 fans
        };



        public const ushort c2s_MatchGameDef = 0x19;
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class MatchGameDef
        {
            // 0 none
            // 1 xueliu
            // 2 xuezhan
            public short gameType;
        }

        public const ushort s2c_MatchGameRes = 0x22;
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class MatchGameRes
        {
            public int errorCode;
            //index:userid
            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            public int[] playerIndex;
            public int dealerIndex;
        }

        public const ushort c2s_SendCardReqDef = 0x11;   //玩家出牌请求
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class SendCardReqDef
        {
            public byte cCard;                 //出的牌,为0表示胡牌
        }

        //FIX BYTE ISSUE
        public const ushort c2s_SpecialCardReqDef = 0x12;   //玩吃胡碰杠听请求
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class SpecialCardReqDef
        {
            public ushort specialType;                       //碰,杠,暗杠,吃,碰杠
            public ushort card;
        }

        public const ushort c2s_DingQueDef = 0x13;  //玩家发送定缺消息
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class DingQueDef
        {
            public byte cCard;                              //定缺的麻将 万条筒
        }

        public const ushort c2s_HuanPaiDef = 0x14;    //客户端发送换牌消息
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class HuaiPaiDef
        {
            [OdaoField(3)]
            public byte[] cCards;
        }

        public const ushort s2c_TrusteeShipDef = 0x16;        //客户端托管
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class TrusteeShipDef
        {
            public byte cTableNumExtra;
            public byte flag;          //  托管 0 关闭，1打开
        }

        public const ushort c2s_TrusteeShipClientDef = 0x18;
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class TrusteeShipClientDef
        {
            public byte flag;          //  托管 0 关闭，1打开
        }

        //2：Server 服务器通知客户端消息（将某玩家的动作告诉所有桌上的玩家）
        public const ushort s2c_SendCardNoticeDef = 0x31;   //出牌通知
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class SendCardNoticeDef
        {
            public byte cTableNumExtra;
            public byte cCard;
            public byte flag;//是否托管 0/1
        }

        //血流只展现这次胡的输赢信息
        public const ushort s2c_SpecialNoticeSerDef = 0x32;   //玩家吃杠碰胡通知
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class SpecialNoticeSerDef
        {
            public byte cExtraTableNum;   //那个位置的玩家
            public byte cSpecialType;     //动作
            public byte cDianPao;       //触发这个动作的玩家
            public byte cCard;
            public int flag;    //是否托管
            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            public ushort[] huType;

            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            public ushort[] gangHuType; //抢杠胡之类用winlostType

            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            public int[] times;

            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            public int[] afterTax;

            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            public int[] earnMoney; //又正有负
        }

        public const ushort s2c_DingQueDef = 0x33;  //通知玩家定缺
        public const ushort s2c_HuaiPaiDef = 0x34;  //服务器要求客户端换牌

        //3：Server 发起客户端消息（服务器端发起,告诉桌上所有玩家）
        public const ushort s2c_DealMjServerDef = 0x40;  //服务器给所有玩家发牌
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class DealMjServerDef
        {
            public byte cZhuang;                               //庄家也是第一个抓牌的玩家

            [OdaoField(GameMessage.HANDLE_MJ_NUM)]
            public byte[] cCards;                 //自己的牌

            public byte bLianZhuang;                           //是否连庄

            [OdaoField(2)]
            public byte[] dices;                              //骰子

            public int iAllCardNum;                            //所有牌的数量
            public int iBaseTimes;                         //底注

            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            public int[] llNowMoney; //玩家现在身上的钱
        }

        public const ushort s2c_SendMjSerDef = 0x43;  //服务器的发牌请求 //摸牌，然后出牌消息
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class SendMjSerDef
        {
            public byte tableNumExtra;    //出牌的那个人
            public byte draw;            //摸的那张牌 0表示没有从剩余的牌墙中取牌，也就是刚才的动作是碰，然后该出牌了 
            public byte specialType;        //胡,暗杠,碰杠的二进制合
        }

        //display ui
        public const ushort s2c_SpecialCardDef = 0x44;  //吃杠碰胡请求
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class SpecialCardDef
        {
            public byte specialType;
            public byte card;
        }

        //自己的换牌结果
        public const ushort s2c_Notice_HuanPaiDef = 0x45;  //服务器给玩家发送换的牌
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class HuanPaiDef
        {
            [OdaoField(3)]
            public byte[] cards;
        }

        //所有人都订完或其他个别人订完
        public const ushort s2c_Notice_DingQueResDef = 0x47; //通知客户端玩家的定缺牌
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class DingQueResDef
        {
            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            //index is player's sit
            //0~3,num
            public byte[] cques;
        }

        public const ushort SEND_NO_MONEY_MSG = 0x49; //通知客户端玩家没钱了
                                                      /*
                                                      public const ushort SPECIAL_REQ_MSG = 0x4a;  //给玩家配牌
                                                      typedef struct PeiPaiServer		//SPECIAL_REQ_MSG 配牌请求
                                                      {
                                                          HEADER;
                                                          char cards[GAME_ALL_MJ_NUM];
                                                          int iSize;
                                                      }PeiPaiServerDef;
                                                      */

        public const ushort SUB_C_QUEDINGRENSHU = 0x15;    //客户端发送认输消息
        public const ushort SUB_S_QUEDINGRENSHU = 0x50;    //通知客户端 认输

        public const int s2c_LeaveTableDef = 0x54;    //通知客户端显示按钮
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class LeaveTableDef
        {
            int btnType;                        //类型 1-离桌 
        };

        // 血流麻将是牌全完的时候发送
        public const ushort s2c_GameResultServerDef = 0xB9;   //游戏结束服务器通知 所有游戏公用
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class GameResultServerDef
        {
            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            public int[] winScore;  //输赢总分

            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            public int[] huWin;     //杠胡的赢钱

            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            public int[] huLost;    //输钱

            public int selfScore;

            //[TABLE_PLAYER_NUM][HANDLE_MJ_NUM]
            [OdaoField(GameMessage.TABLE_PLAYER_NUM * GameMessage.HANDLE_MJ_NUM)]
            public byte[] handCars;

            public int recordCount;     //该结构体中包含多少个WinLostScoreRecord 结构体

            //[OdaoField(1)]
            //public WinLostScoreRecordDef[] records;
        }

        //积分输赢记录 
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class WinLostScoreRecordDef
        {
            public int winLostType;                        //类型
            public int huType;                             //胡的类型
            public int times;                              //倍数
            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            public long[] winScore;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class GameStateDef
        {
            public int iAllCardsNum; //游戏一共有多少张牌
            public int iNumsTakenFromFront;//从牌墙前面拿的牌的数量
            public int iNumsTakenFromEnd;//从牌墙后面拿的牌的数量
            public int iBaseTimes;//房间底注
            public ushort usBanker;//庄家
            public ushort cCurPlayer;                                   //当前操作玩家
            public ushort cTableCard;                                   //当前出的牌
            public ushort iLeftCardsNum;                        //游戏中还剩多少张牌

            [OdaoField(2)]
            public ushort[] Dices;//每局开始筛子的数值

            [OdaoField(14)]
            public byte[] cHandCards;                                //玩家手上的牌

            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            public byte[] cHandCardsNum;               //玩家手上牌的数量

            [OdaoField(GameMessage.TABLE_PLAYER_NUM * 21)]
            public byte[] cSendCards;           //玩家已经出呐盘

            [OdaoField(GameMessage.TABLE_PLAYER_NUM * 4)]
            public SpecialCardReqDef[] combo;   //碰杠信息

            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            ushort[] cPlayerStat;                   //玩家的状态 1 在玩 2 胡了 3 离开 

            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            public ushort[] cDingQueCards;               //玩家定缺信息

            [OdaoField(GameMessage.TABLE_PLAYER_NUM * 24)]
            public ushort[] cHuCards;               //各个玩家胡的牌

            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            public ushort[] usUserID;               //玩家的userid

            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            public int[] llCoin;                    //身上的钱
        }
    }
}
