
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using MsgPack;
using MsgPack.Serialization;

namespace MP
{
    public class GameMessage
    {
        public const int TABLE_PLAYER_NUM = 4;
        public const int MAXHUANPAINUM = 3;
        public const int HANDLE_MJ_NUM = 14;
        public const int GAME_ALL_MJ_NUM = 108;

		//定义玩家现在的状态
		public const int GAME_HAVE_READY = 0X05;					//等待游戏开始
		public const int GS_WK_HUANPAI = 6;						//游戏换牌
		public const int GS_WK_DINGQUE = 7;						//游戏定缺
		public const int GAME_WAIT_SPECIAL = GAME_HAVE_READY+3;		//等待吃胡碰杠状态
		public const int GAME_WAIT_SEND = GAME_HAVE_READY+4;        //等待出牌

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

		public enum LEAVETABLE_TYPE
		{
			LEAVETABLE_LEAVE = 0,//游戏结束离开
			LEAVETABLE_CHANGE	//换桌
		};

		public enum HUANPAI_TYPE
		{//换牌类型  1：顺时针 2：逆时针 3：对家换牌
			HUANPAI_SHUN = 1, 
			HUANPAI_NI,
			HUANPAI_DUI
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

        public const ushort s2c_MatchGameRes = 0x22;//match 返回，换桌也会返回
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
        public class SendCardReqDef
        {
            [MessagePackMember(0)]
            public byte cCard;                 //出的牌,为0表示胡牌
        }

        //FIX BYTE ISSUE
        public const ushort c2s_SpecialCardReqDef = 0x12;   //玩吃胡碰杠听请求
        public class SpecialCardReqDef
        {
            [MessagePackMember(0)]
            public byte specialType;                       //碰,杠,暗杠,吃,碰杠
            [MessagePackMember(1)]
			public byte card;
        }

        public const ushort c2s_DingQueDef = 0x13;  //玩家发送定缺消息
        public class DingQueDef
        {
            [MessagePackMember(0)]
            public byte cCard;                              //定缺的麻将 万条筒
        }

        public const ushort c2s_HuanPaiDef = 0x14;    //客户端发送换牌消息
		//0x34 通知玩家换牌 也使用此结构
        public class HuanPaiDef
        {
			[MessagePackMember(0)]
			public List<byte> vHuanPai;
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
            public byte flag;          //  托管 0 关?开
        }

        //2：Server 服务器通知客户端消息（将某玩家的动作告诉所有桌上的玩家）
        public const ushort s2c_SendCardNoticeDef = 0x31;   //出牌通知
        public class SendCardNoticeDef
        {
            [MessagePackMember(0)]
            public byte cTableNumExtra;

            [MessagePackMember(1)]
            public byte cCard;

            [MessagePackMember(2)]
            public byte flag;//是否托管 0/1
        }

        //血流只展现这次胡的输赢信息
        public const ushort s2c_SpecialNoticeSerDef = 0x32;   //玩家吃杠碰胡通知
        public class SpecialNoticeSerDef
        {
            [MessagePackMember(0)]
            public byte cExtraTableNum;   //那个位置的玩家

            [MessagePackMember(1)]
            public byte cSpecialType;     //动作

            [MessagePackMember(2)]
            public byte cDianPao;       //触发这个动作的玩家

            [MessagePackMember(3)]
            public byte cCard;

            [MessagePackMember(4)]
            public Int32 flag;    //是否托管

            [MessagePackMember(5)]
            public List<ushort> huType;

            [MessagePackMember(6)]
            public List<ushort> gangHuType; //抢杠胡之类用winlostType

            [MessagePackMember(7)]
            public List<Int32> times;

            [MessagePackMember(8)]
            public List<long> afterTax;

            [MessagePackMember(9)]
            public List<long> earnMoney; //又正有负
        }

        public const ushort s2c_DingQueDef = 0x33;  //通知玩家定缺
        public const ushort s2c_HuaiPaiDef = 0x34;  //服务器要求客户端换牌

        //3：Server 发起客户端消息（服务器端发起,告诉桌上所有玩家）
        public const ushort s2c_DealMjServerDef = 0x40;  //服务器给所有玩家发牌
        public class DealMjServerDef
        {
            [MessagePackMember(0)]
            public byte cZhuang;                               //庄家也是第一个抓牌的玩家

            [MessagePackMember(1)]
            public byte bLianZhuang;                           //是否连庄

            [MessagePackMember(2)]
            public Int32 iAllCardNum;                            //所有牌的数量

            [MessagePackMember(3)]
            public Int32 iBaseTimes;                         //底注

            [MessagePackMember(4)]
            public List<byte> cCards;              //自己的牌

            [MessagePackMember(5)]
            public List<long> llNowMoney; //玩家现在身上的钱

            [MessagePackMember(6)]
            public List<byte> dices;                              //骰子

        }

        public const ushort s2c_SendMjSerDef = 0x43;  //服务器的发牌请求 //摸牌，然后出牌消息
        public class SendMjSerDef
        {
            [MessagePackMember(0)]
            public byte tableNumExtra;    //出牌的那个人
            [MessagePackMember(1)]
            public byte draw;            //摸的那张牌 0表示没有从剩余的牌墙中取牌，也就是刚才的动作是碰，然后该出牌了 
            [MessagePackMember(2)]
            public byte specialType;        //胡,暗杠,碰杠的二进制合
        }

        //display ui
        public const ushort s2c_SpecialCardDef = 0x44;  //吃杠碰胡请求
        public class SpecialCardDef
        {
            [MessagePackMember(0)]
            public byte specialType;
            [MessagePackMember(1)]
            public byte card;
        }

        //自己的换牌结果 收到此条消息代表其他玩家也换牌成功
        public const ushort s2c_Notice_HuanPaiDef = 0x45;  //服务器给玩家发送换的牌
        public class HuanPaiResDef
        {
			[MessagePackMember(0)]
			public byte iChangeType; // 1：顺时针 2：逆时针 3：对家换牌

			[MessagePackMember(1)]
			public List<byte> cCards;//服务器换给的牌	
        }

        //所有人都昊蚱涫?别人订烷冗       
		public const ushort s2c_Notice_DingQueResDef = 0x47; //通知客户端玩家的定缺牌
        public class DingQueResDef
        {
            [MessagePackMember(0)]
            public List<byte> cques;
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

		public const int s2c_LeaveTableDef = 0x54;    //通知客户端显示按钮(血战到底会收到此消息)
        
        public class LeaveTableDef
        {
			[MessagePackMember(0)]
			public byte btnType;                        //嘈捱1 认氵2 离开 
        };

        // 血流麻将是牌全完的时候发送
        public const ushort s2c_GameResultServerDef = 0xB9;   //游戏结束服务器通知 所有游戏公用
        public class GameResultServerDef
        {
			[MessagePackMember(0)]
			public List<PlayerMessage> GameResult;  //输赢总分
        }
		public class PlayerMessage
		{
			[MessagePackMember(0)]
			public Int32			iUserID;	

			[MessagePackMember(1)]
			public Int64			llScore;	//一局的净分

			[MessagePackMember(2)]
			public byte				cHu;		//胡

			[MessagePackMember(3)]
			public byte				cHuaZhu;	//花猪

			[MessagePackMember(4)]
			public byte				cNOTING;	//未听

			[MessagePackMember(5)]
			public byte				cWinCounts;//胡牌次数

			[MessagePackMember(6)]
			public List<HuCards>		vHuCards;//所有胡的牌

			[MessagePackMember(7)]
			public List<byte>	vhandCards; //手牌

			[MessagePackMember(8)]
			public List<SpecialCardReqDef> vStructMj; //碰杠的牌 

			[MessagePackMember(9)]
			public List<ScoreRecord> vAllRecors; //详细信息
		}
		public class HuCards
		{
			[MessagePackMember(0)]
			public byte cCard;
			[MessagePackMember(1)]
			public byte cNum;
		}
		public class ScoreRecord
		{
			[MessagePackMember(0)]
			public Int32 winLostType;						//类型

			[MessagePackMember(1)]
			public Int32 huType;							//胡的类型

			[MessagePackMember(2)]
			public Int32 iGenNum;							//根的个数

			[MessagePackMember(3)]
			public Int32 times;								//倍数

			[MessagePackMember(4)]
			public Int64 winScore;					//其他三人的输赢积分，

			[MessagePackMember(5)]					//来判断赢的是那几个玩家（三家，上家，下家，对家）
			public List<byte> vIndex;	
		}

		public class PengGangCards
		{
			[MessagePackMember(0)]
			public List<SpecialCardReqDef>  SpecialCards;
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
        public class OutCards
        {
            [MessagePackMember(0)]
            public List<byte> cCards;
        }

		public class ComboCards
		{            
			[MessagePackMember(0)]
			public List<SpecialCardReqDef> list;   //碰杠信息
		}

        public class GameStateDef
        {
            [MessagePackMember(0)]
            public Int32 iAllCardsNum; //游戏一共有多少张牌

            [MessagePackMember(1)]
            public Int32 iNumsTakenFromFront;//从牌墙前面拿的牌的数量

            [MessagePackMember(2)]
            public Int32 iNumsTakenFromEnd;//从牌墙后面拿的牌的数量

            [MessagePackMember(3)]
            public Int32 iBaseTimes;//房间底注

            [MessagePackMember(4)]
            public ushort usBanker;//庄家

            [MessagePackMember(5)]
            public ushort cCurPlayer;                                   //当前操作玩家

            [MessagePackMember(6)]
            public ushort cTableCard;                                   //当前出的牌

            [MessagePackMember(7)]
            public ushort iLeftCardsNum;                        //游戏中还剩多少张牌

            [MessagePackMember(8)]
            public List<ushort> Dices;//每局开始筛子的数值

            [MessagePackMember(9)]
            public List<byte> cHandCards;                                //玩家稚系呐盘

            [MessagePackMember(10)]
            public List<byte> cHandCardsNum;               //玩家手上牌的数量

            [MessagePackMember(11)]
            public List<OutCards> cSendCards;           //玩家已?的排壬            
//			public List<ComboCards> combo;   //碰杠信息
			[MessagePackMember(12)]
			public List<PengGangStruct> combo;   //碰杠信息

            [MessagePackMember(13)]
            public List<ushort> cPlayerStat;                   //玩家的状态 1 在玩 2 胡了 3 离开 

            [MessagePackMember(14)]
            public List<ushort> cDingQueCards;               //玩家定缺信息

            [MessagePackMember(15)]
            public List<OutCards> cHuCards;               //各个玩家胡的牌

            [MessagePackMember(16)]
            public List<ushort> usUserID;               //玩家的userid

            [MessagePackMember(17)]
            public List<long> llCoin;                    //身上的钱

			[MessagePackMember(18)]
			public Int32 iGameStates;					//游戏状态 换牌：0x50, 定缺:0x53 等待吃胡碰杠状态:0x08 等待出牌:0x09

			[MessagePackMember(19)]
			public List<byte> v_RecommendCard;			//如果是换牌状态：换的三张牌 定缺：4个玩家的定缺类型

			[MessagePackMember(20)]
			public byte iSpecialTypeTotal;				//吃胡碰杠状态

			[MessagePackMember(21)]
			public List<byte> v_SpecialCard;			//吃胡碰的牌（先定义成数组，目前只有一个值）
        }

		public class SpecialStrut   	
		{
			[MessagePackMember(0)]
			public byte cStructType;						//碰,杠,暗杠,吃,碰杠

			[MessagePackMember(1)]
			public byte Mj;

			[MessagePackMember(2)]
			public byte cPosition;							//谁打的碰杠

		};


		public class PengGangStruct
		{
			[MessagePackMember(0)]
			public List<SpecialStrut>  SpecialCards;
		};
		// 血流麻将是牌全完的时候发送
		public const ushort c2s_LeaveTableReqDef = 0x67;   //玩家离桌
		public const ushort c2s_ChangeTableReqDef = 0x68;   //玩家换桌
		public const ushort c2s_ContinueGameReqDef = 0x69;   //继续游戏 ready 返回 0xa6

		public const ushort s2c_Notice_LeaveTableDef = 0xB5; //玩家离开，换桌返回
		public class NoticeLeaveTableDef
		{
			[MessagePackMember(0)]
			public Int32 iUserID;

			[MessagePackMember(1)]
			public byte cLeaveType;                       //离开类型 0:游戏结束离开 1: 换桌
		}
			
		//胡牌提示信息
		public const ushort s2c_HuTipsResDef = 0x70; //通知客户端玩家的定缺牌
		public class HuTips
		{
			[MessagePackMember(0)]
			public byte cCard;

			[MessagePackMember(1)]
			public byte cTimes;

			[MessagePackMember(2)]
			public byte cLeftNum;
		}
		public class AllTips
		{
			[MessagePackMember(0)]
			public byte cOutCard;

			[MessagePackMember(1)]
			public List<HuTips> v_Tips;
		
		};
		public class AllOutCard
		{
			[MessagePackMember(0)]
			public List<AllTips> v_OutCards;
		};

		// MSGPACK_TEST
		public class TEST_MSGPACK_TableNum
		{
			[MessagePackMember(0)]
			public int iNum;
			[MessagePackMember(1)]
			public byte cTable;
		}

		public class TEST_MSGPACK_PlayerCard
		{
			[MessagePackMember(0)]
			public List<TEST_MSGPACK_TableNum> palyerNum;
		}
		//0x57
        public class TEST_MSGPACK
        {
            [MessagePackMember(0)]
            public byte cType;
            [MessagePackMember(1)]
            public ushort usType;
            [MessagePackMember(2)]
            public Int32 iType;
            [MessagePackMember(3)]
            public long llType;
            [MessagePackMember(4)]
            public List<byte> m_vCType;
            [MessagePackMember(5)]
            public List<long> m_vLLType;
            [MessagePackMember(6)]
            public List<ushort> m_vusType;
            [MessagePackMember(7)]
            public List<int> m_viType;
            [MessagePackMember(8)]
            public List<TEST_MSGPACK_TableNum> m_vPosition;
            [MessagePackMember(9)]
            public List<TEST_MSGPACK_PlayerCard> m_allPlayerCard;
			[MessagePackMember(10)]
			public string m_string;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class TEST_RAW
        {
            public byte b;
            public ushort us;
            public Int32 i32;
            public long l;
        }
    }
}
