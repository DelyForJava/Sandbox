
using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

//考虑到其他地方调用 消息ID 先放在BaseMessage外
public class BaseMessage
{
    public const ushort LOGIN_REQ_MSG = 0x9000;
    public const ushort LOGIN_RES_MSG = 0x9001;
    public const ushort LOBBY_LOGIN_REQ_MSG = 0x9100;
    public const ushort LOBBY_LOGIN_RES_MSG = 0x9101;
    public const ushort LOBBY_SHOP_EXCHANGE_COIN_REQ_MSG = 0x9106;
    public const ushort LOBBY_SHOP_EXCHANGE_COIN_RES_MSG = 0x9107;
    public const ushort LOBBY_MODIFY_NICK_NAME_REQ_MSG = 0x9104;
    public const ushort LOBBY_MODIFY_NICK_NAME_RES_MSG = 0x9105;
    public const ushort LOBBY_MODIFY_GENDER_REQ_MSG = 0x9108;
    public const ushort LOBBY_MODIFY_GENDER_RES_MSG = 0x9109;

    public const ushort LOBBY_PACKAGE_INFO_REQ_MSG = 0xA000;
    public const ushort LOBBY_PACKAGE_INFO_RES_MSG = 0xA001;
    public const ushort LOBBY_PACKAGE_ADD_ITEM_REQ_MSG = 0xA002;
    public const ushort LOBBY_PACKAGE_ADD_ITEM_RES_MSG = 0xA003;
    public const ushort LOBBY_PACKAGE_UPDATE_ITEM_REQ_MSG = 0xA004;
    public const ushort LOBBY_PACKAGE_UPDATE_ITEM_RES_MSG = 0xA005;
    public const ushort LOBBY_PACKAGE_REMOVE_ITEM_REQ_MSG = 0xA006;
    public const ushort LOBBY_PACKAGE_REMOVE_ITEM_RES_MSG = 0xA007;

    public const ushort LOBBY_GOLD_BEAN_EXCHANGE_REQ_MSG = 0xA010;
    public const ushort LOBBY_GOLD_BEAN_EXCHANGE_RES_MSG = 0xA011;


    public const ushort AUTHEN_REQ_MSG = 0xA0;
    public const ushort AUTHEN_RES_MSG = 0xA1;
    public const ushort AGAIN_LOGIN_RES_MSG = 0xA2;
    public const ushort AGAIN_LOGIN_OTHER_NOTICE_MSG = 0xA3;
    public const ushort SITDOWN_REQ_MSG = 0xA4;
    public const ushort SITDOWN_RES_MSG = 0xA6;

    public const ushort TABLE_PLAYER_JOIN_MSG = 0xA7;
    public const ushort TABLE_PLAYER_LEAVE_MSG = 0xA8;
    public const ushort READY_REQ_MSG = 0xA9;
    public const ushort READY_RES_MSG = 0xAA;
    public const ushort USER_SCORE_INFO_MSG = 0XA001;             //玩家输赢数据，经验值
    public const ushort JOIN_TABLE_AS_VISITOR = 0XA002;              //旁观玩家进入桌子
    public const ushort UPDATE_TABLE_PLAYER_STATUS = 0XA003;              //更新一桌玩家身份
    public const ushort VIP_KICKOUT_PLAYER_REQ = 0XA007;                 //VIP踢人请求
    public const ushort VIP_KICKOUT_PLAYER_RES = 0XA008;                 //VIP踢人回应
    public const ushort TABLE_SET_PLAYER_NUMBER = 0XA013;               //自由设定桌子人数  0XA013后在Odao_SiteActivity_Msg.h中用

    public const ushort CLIENT_CHARGE_REFRESH_MONEY_REQ = 0xB007;               //充值同步积分
    public const ushort KICK_OUT_SERVER_MSG = 0xB1;   //服务器踢除玩家
    public const ushort CHECK_SITDOWN_PASSWD_REQ = 0xB3;   //入座请求密码
    public const ushort SET_TABLE_BASEPOINT_REQ = 0xB4;   //请求设定桌底柱消息
    public const ushort TABLE_PLAYER_LEAVE_NOTICE = 0xB5;
    public const ushort SEND_ALL_USERINFO_TO_CLIENT = 0xB6;
    public const ushort GAME_RESULT_SERVER_MSG = 0xD104;   //游戏结束服务器通知 所有游戏公用
    public const ushort GAME_BULL_NOTICE_MSG = 0xBA;   //游戏公告消息
    public const ushort URGR_CARD_MSG = 0xBB;   //对应玩家的催牌消息
    public const ushort SHOUT_INFO_RES_MSG = 0xBC;   //玩家的喊话消息
    public const ushort SEND_GAME_ROOM_INFO = 0XBE;   //游戏基本房间信息
    public const ushort SEND_ALL_TABLE_INFO = 0XBF;   //发送所有玩家的桌信息

    //群发消息处理
    public const ushort SEND_ALLUSER_STATUS_MSG = 0XC1;   //发送所有玩家信息给客户端
    public const ushort SEND_TABLE_STAUTS_TO_CLIENT = 0XC3;
    public const ushort SEND_TABLE_LOCKED_TO_CLIENT = 0XC4;
    public const ushort SEND_TABLE_LEVEL_TO_CLIENT = 0XC5;
    public const ushort SEND_TABLE_SETTING_TO_CLIENT = 0XC6;
    public const ushort SEND_TABLE_ANTE_TO_CLIENT = 0XC7;

    public const ushort SET_BASE_POINT_SERVER = 0XC0;   //是否需要发送底注
    public const ushort GAME_START_NOTICE_MSG = 0XC8;    //游戏开始通知
    public const ushort SIT_DOWN_SUCCESS_MSG = 0xCA;    //入座成功收到道具信息
    public const ushort TABLE_BASEINFO_MSG = 0XCD;    //同意底注请求
    public const ushort SET_BASE_POINT_SPE_SERVER = 0XCE;    //是否需要发送底注(特殊)
    public const ushort TABLE_BASEINFO_SPE_MSG = 0XCF;    //同意底注请求(特殊)
    public const ushort HONGBAO_TABLE_INFO = 0XC001;    //是否需要发送底注(特殊)
    public const ushort USER_LOGIN_BANK_REQ = 0xD3;   //玩家登录银行请求
    public const ushort USER_LOGIN_RESULT_NOTICE = 0xD4;   //银行登录结果相应
    public const ushort GET_BANK_COIN_REQ = 0xD5;   //取款清求
    public const ushort GET_COIN_RESULT_RES = 0xD6;   //取款结果响应
    public const ushort USER_SAVE_COIN_REQ = 0xD7;   //存款请求
    public const ushort USER_SAVE_COIN_RES = 0xD8;   //存款结果响应
    public const ushort MODIFY_BANK_PASSWD_REQ = 0xD9;   //用户修改银行密码请求
    public const ushort MODIFY_BANK_PASSWD_RES = 0xDA;   //用户修改银行密码响应
                                                         //玩家积分变化时，通知同桌其他玩家
    public const ushort SC_MSG_NOTIFY_TABLE_PLAYERS_COIN_CHANGE = 0xE800 + 40;

    public const ushort GAME_USER_PROPEX_MSG = 0xD106;         //玩家身上的额外道具
    public const ushort GAME_CLIENT_SITE_TYPE_MSG = 0xD107;		//客户端站点类型

    
    /// <summary>
    /// 登录发包
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class LoginReqDef
    {
        public LoginReqDef()
        {
        }
        //public byte msgType;
        [MessagePackMember(0)]
        public int accountId;
        [MessagePackMember(1)]
        public int serviceId;
        [MessagePackMember(2)]
        public int sourceId;
        
        [OdaoFieldAttribute(64)]  //string 序列化处理
        [MessagePackMember(3)]
        public string token;
        
        [OdaoFieldAttribute(64)]  //string 序列化处理
        [MessagePackMember(4)]
        public string machine;
    };

    /// <summary>
    /// 登录收包
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class LoginResDef
    {
        public LoginResDef()
        {
        }
        [MessagePackMember(0)]
        public sbyte cErrorCode;
        [MessagePackMember(1)]
        public int iUserID;
        [MessagePackMember(2)]
        public int accountId;
        [MessagePackMember(3)]
        public int serviceId;
        [OdaoFieldAttribute(33)]
        [MessagePackMember(4)]
        public string szPasswdToken;
        [OdaoFieldAttribute(33)]
        [MessagePackMember(5)]
        public sbyte cGender;
        [OdaoFieldAttribute(33)]
        [MessagePackMember(6)]
        public sbyte cVipLv;
        [MessagePackMember(7)]
        public long llGameCoin;
        [MessagePackMember(8)]
        public long llBankCoin;
        [MessagePackMember(9)]
        public long llDiamondNum;
        [MessagePackMember(10)]
        public long llGoldBean;
        [OdaoFieldAttribute(33)]
        [MessagePackMember(11)]
        public string szNickName;
        [OdaoFieldAttribute(33)]
        [MessagePackMember(12)]
        public string szWXIconURL;
        [OdaoFieldAttribute(33)]
        [MessagePackMember(13)]
        public string szWXNickName;
        [MessagePackMember(14)]
        public int iVipExp;
        [MessagePackMember(15)]
        public sbyte cLevel;
        [MessagePackMember(16)]
        public sbyte iLevelExp;
        [MessagePackMember(17)]
        public sbyte cFllowWechat;
    };

    /// <summary>
    /// 大厅长链接发包
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class LobbyLoginReqDef
    {
        public LobbyLoginReqDef()
        {
        }
        [MessagePackMember(0)]
        public int userId;
        [MessagePackMember(1)]
        public string token;
    };

    /// <summary>
    /// 大厅长链接收包
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class LobbyLoginResDef
    {
        public LobbyLoginResDef()
        {
        }
        [MessagePackMember(0)]
        public sbyte cErrorCode;
        [MessagePackMember(1)]
        public int iUserID;
    };

    /// <summary>
    /// 商城购买发包
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class BuyGoldReqDef
    {
        public BuyGoldReqDef()
        {
        }
        [MessagePackMember(0)]
        public short sShopID;
        [MessagePackMember(1)]
        public short sItemIndex;
    };

    /// <summary>
    /// 商城购买收包
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class BuyGoldResDef
    {
        public BuyGoldResDef()
        {
        }
        [MessagePackMember(0)]
        public sbyte cErrorCode;
        [MessagePackMember(1)]
        public int iAddCoin;
        [MessagePackMember(2)]
        public int iSubIDiamond;
    };

    /// <summary>
    /// 个人信息请求昵称修改发包
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class ChangeNameReqDef
    {
        public ChangeNameReqDef()
        {
        }
        [MessagePackMember(0)]
        public string szNickName;
    };

    /// <summary>
    /// 个人信息请求昵称修改收包
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class ChangeNameResDef
    {
        public ChangeNameResDef()
        {
        }
        [MessagePackMember(0)]
        public sbyte cErrorCode;//0成功 1用户不存在 2钻石不足 3更新名字出错
        [MessagePackMember(1)]
        public int iSubDiamond;
        [MessagePackMember(2)]
        public string szNickName;
    };

    /// <summary>
    /// 个人信息请求性别修改发包
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class ChangeGenderReqDef
    {
        public ChangeGenderReqDef()
        {
        }
        [MessagePackMember(0)]
        public sbyte cGender;
    };

    /// <summary>
    /// 个人信息请求性别修改收包
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class ChangeGenderResDef
    {
        public ChangeGenderResDef()
        {
        }
        [MessagePackMember(0)]
        public sbyte cErrorCode; //0成功 1用户不存在 2 性别大于1
        [MessagePackMember(1)]
        public sbyte cGender;
    };

    /// <summary>
    /// 获取背包道具信息发包
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class PackageInfoReqDef
    {
        public PackageInfoReqDef()
        {
        }
        [MessagePackMember(0)]
        public int iUserID;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class ItemInfo
    {
        [MessagePackMember(0)]
        public int iItemID;
        [MessagePackMember(1)]
        public int iItemNum;
        [MessagePackMember(2)]
        public long LExpireTime;
        [MessagePackMember(3)]
        public long LGetTime;
        [MessagePackMember(4)]
        public sbyte cIsNew;
    }

    /// <summary>
    /// 获取背包道具信息收包
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class PackageInfoResDef
    {
        public PackageInfoResDef()
        {
        }
        [MessagePackMember(0)]
        public List<ItemInfo> ItemInfoList;
    };

    /// <summary>
    /// 添加背包道具信息发包
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class PackageAddItemReqDef
    {
        public PackageAddItemReqDef()
        {
        }
        [MessagePackMember(0)]
        public int iItemID;
        [MessagePackMember(1)]
        public int iAddNum;
    };

    /// <summary>
    /// 添加背包道具信息收包
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class PackageAddItemResDef
    {
        public PackageAddItemResDef()
        {
        }
        [MessagePackMember(0)]
        public sbyte cErrorCode;
        [MessagePackMember(1)]
        public int iItemID;
        [MessagePackMember(2)]
        public int iAddNum;
        [MessagePackMember(3)]
        public long LExpireTime;
        [MessagePackMember(4)]
        public long LGetTime;
    };

    /// <summary>
    /// 删除背包道具信息发包
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class PackageRemoveItemReqDef
    {
        public PackageRemoveItemReqDef()
        {
        }
        [MessagePackMember(0)]
        public int iItemID;
        [MessagePackMember(1)]
        public int iRemoveNum;
    };

    /// <summary>
    /// 删除背包道具信息收包
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class PackageRemoveItemResDef
    {
        public PackageRemoveItemResDef()
        {
        }
        [MessagePackMember(0)]
        public sbyte cErrorCode;
        [MessagePackMember(1)]
        public int iItemID;
        [MessagePackMember(2)]
        public int iRemoveNum;
    };

    /// <summary>
    /// 更新背包道具信息发包
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class PackageUpdateItemReqDef
    {
        public PackageUpdateItemReqDef()
        {
        }
        [MessagePackMember(0)]
        public int iItemID;
        [MessagePackMember(1)]
        public sbyte cIsNew;
    };

    /// <summary>
    /// 更新背包道具信息收包
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class PackageUpdateItemResDef
    {
        public PackageUpdateItemResDef()
        {
        }
        [MessagePackMember(0)]
        public int iItemID;
        [MessagePackMember(1)]
        public sbyte cIsNew;
    };

    /// <summary>
    /// 兑换发包
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class ExchangeReqDef
    {
        public ExchangeReqDef()
        {
        }
        [MessagePackMember(0)]
        public UInt16 usType;
        [MessagePackMember(1)]
        public UInt16 usItemIndex;
        [MessagePackMember(2)]
        public string szUserMobile;
        [MessagePackMember(3)]
        public string szUserTel;
        [MessagePackMember(4)]
        public string szUserAddress;
    };

    /// <summary>
    /// 兑换收包
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class ExchangeResDef
    {
        public ExchangeResDef()
        {
        }
        [MessagePackMember(0)]
        public sbyte cErrorCode;//0,ok;1,noIni;3,notWhchat;4,noTel;5,noAdd
        [MessagePackMember(1)]
        public int iSubGoldBean;
        [MessagePackMember(2)]
        public int iAddCoin;
    };














    #region 历史


    public class AuthenType
    {
        public const sbyte AUTHEN_SUCCSSE = 0;                    //登录成功
        public const sbyte AUTHEN_SERVER_CLOSE_ERROR = 101;    //房间被关闭
        public const sbyte AUTHEN_SERVER_FULL_ERROR = 102;    //房间满员
        public const sbyte AUTHEN_RADIUS_NOFIND_USERID = 103;    //用户名不存在
        public const sbyte AUTHEN_RADIUS_PASSWORD_ERROR = 104;    //密码错误
        public const sbyte AUTHEN_RADIUS_ACCOUNT_DISABLE = 105;    //帐号被禁用
        public const sbyte AUTHEN_RADIUS_ALREADY_INGAME = 106;   //帐号已经在游戏中
        public const sbyte AUTHEN_RADIUS_DB_ERROR = 107;   //数据库存在异常
        public const sbyte AUTHEN_NO_ENOUGH_MONEY = 108;   //帐号银子不够该房间限制
        public const sbyte AUTHEN_ANGIN_NOFIND_OLDNODE = 109;   //掉线重入找不到以前节点了
        public const sbyte AUTHEN_EXCEED_ONE_TIEM = 110;   //玩家非法发送认证消息，即为多次发送认证消息
        public const sbyte AUTHEN_INSERT_ONLINE_FAIL = 111;   //玩家插入在线状态
        public const sbyte AUTHEN_MACHINE_ERROR = 112;   //机器码错误
        public const sbyte AUTHEN_IP_ERROR = 113;   //IP错误已经被禁用
        public const sbyte AUTHEN_DEFAULT_LOGIN = 114;   //固定机器 
        public const sbyte AUTHEN_MONEY_OVER = 115;   //身上钱超过入房间上限
        public const sbyte AUTHEN_SPECIALROOM_FORBBIDEN = 116;    //特殊房间限制
        public const sbyte AUTHEN_COMPETE_ERROR = 117;   //比赛模块认证失败
    };

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class UserPointLimitDef
    {
        public int iMinPoint; //0                                  //最小底注
        public int iMaxPoint; //0                                  //最大底注
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class SitDownReqDef
    {
        public SitDownReqDef()
        {
            userPointLimit = new UserPointLimitDef();
        }

        public int iBindUserID; //0
        public ushort iTableNum;    //0(系统随机找一个) ~0 自己选哪里坐哪里
        public ushort usTableNumExtra;//0椅子号
        public UserPointLimitDef userPointLimit;               //用户入座限制
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class SitDownResDef
    {
        public SitDownResDef()
        {
        }

        public short iTableNum; // 哪个桌子
        public byte cError;         
        public short usTableNumExtra;   //哪个椅子
        public sbyte cPlayerNum;    //目前桌子上有几个人 包括自己
    }

    public class SitDownType
    {
        public const byte SRD_SUCCESS = 0;          //入座成功
        public const byte SRD_USER_STATE_ERROR = 131;          //玩家状态错
        public const byte SDR_NO_FIND_TABLE = 132;          //无法找到合适的座位
        public const byte SDR_TABLE_ALREADY_USING = 133;          //指定的位置已经有玩家了
        public const byte SDR_TABLENUM_ERROR = 134;          //请求桌号不在房间桌范围
        public const byte SDR_TABLENUMEXTRA_ERROR = 135;          //请求的桌上位置不在游戏桌座位
        public const byte SDR_TABLE_LOCK_STATUS = 136;          //桌子锁定状态
        public const byte SDR_TABLE_PASSWD_ERROR = 137;          //桌密码错误
        public const byte SDR_TABLE_NEED_SET_BASEPOINT = 138;          //桌需要设置底柱
        public const byte SDR_TABLE_SETTING_BASEPOINT = 139;          //表示这个桌子正在设置底柱
        public const byte SDR_TABLE_NO_ENOUGH_MONEY = 140;          //玩家身上不满足本桌底注定
        public const byte SDR_TABLE_IN_THE_GAMEING = 141;          //选择入座 该桌正在游戏中
        public const byte SDR_TABLE_FORBBIDEN_VISITE = 142;          //禁止旁观
        public const byte SDR_TABLE_VISITE_GAMEEND = 143;          //游戏未开始，不能旁观
        public const byte SDR_TABLE_VISITOR_FULL = 144;          //旁观座位已满
        public const byte SDR_CHOOSEPLAYER_ISVISITOR = 145;          //旁观的玩家正在等待下一局开始
        public const byte SDR_PLAYE_CHECK_IP = 146;          //玩家不允许IP相同玩家一桌
        public const byte SDR_PLAYER_CHECK_MONEY = 147;          //不符合积分范围
        public const byte SDR_PLAYER_CHECK_RUNRATE = 148;          //不符合逃跑率设置
        public const byte SDR_PLAYER_CHECK_LIMIT_WINRATE = 149;         //不符合最低胜率
        public const byte SDR_PLAYER_CHECK_NETSPEED = 150;            //有玩家延时过高
    };

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class CheckSitDownReq
    {
        public CheckSitDownReq()
        {
            iTableNum = 0;
            usTableNumExtra = 0;
            szTablePasswd = new byte[NetworkInterface.OdaoMessageHeaderId.PASSWD_LEN];
        }

        public ushort iTableNum;                    //桌号 ,为0表示请求服务器分配座位
        public ushort usTableNumExtra;               //桌位置
        [OdaoFieldAttribute(NetworkInterface.OdaoMessageHeaderId.PASSWD_LEN)]
        public byte[] szTablePasswd;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class ReadyReqDef
    {
		public byte cReady;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class ReadyResDef
    {
        public ReadyResDef()
        {
        }

        public ushort usTableNumExtra;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class TablePlayerJoinDef
    {
        public TablePlayerJoinDef()
        {
        }

        public ushort usTableNumExtra;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class TablePlayerLeaveDef
    {
        public TablePlayerLeaveDef()
        {
        }

        public int iUserID;
        public sbyte cLeaveType;
    }


    public class QuitType
    {
        public const sbyte TPL_QUIT_PROGRAME = 1;
        public const sbyte TPL_READY_LEAVE_FOR_CHANGE = 2;
        public const sbyte TPL_GAME_DISCONNECT = 3;
        public const sbyte TPL_GAME_CLIENT_LEAVE = 4;
        public const sbyte TPL_GAME_RESULT_LEAVE = 5;
    }


    //强制设置对齐参数 Pack = 4 ,long 为8个字节
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class PlayerInfoExtra
    {
        public PlayerInfoExtra()
        {
            //szUserName = new byte[32];
            //szNickName = new byte[32];
            //szAreaName = new byte[20];
        }
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
        public int iCompeteAmount;       //比赛积分
        public int iWinNum;              //胜利次数
        public int iLostNum;                 //输的次数
        public int iDrawNum;               //和局次数
        public int iDisNum;				 //掉线
        public int iLastResult;            //上把结果,只客户端有
        public int iPhotoKey;              //自定义图像ID
        public ushort iTableNum;
        public ushort usIconNum;              //人物图像图标号
        public short usTableNumExtra;
        public ushort usExpLevel;             //用户经验等级
        public sbyte cSexType;
        public sbyte cVipType;               //VIP类
        public sbyte cPhotoVerify;
        public sbyte cMasterType;            //管理登记
        public byte ucSpecialIdentify;      //管理员特殊身份标识  
        public sbyte cUserStatus;            //用户状态  
        public sbyte cIfReady;
        public sbyte cDisconnectType;
        [OdaoField(32)]
        public string szUserName;
        [OdaoField(32)]
        public string szNickName;
        [OdaoField(20)]
        public string szAreaName;
        public sbyte cIsBindMobile;           //是否绑定过手机
        public sbyte cIsVisitor;              //是否是旁观 0:非旁观 1: 本局暂时旁观 2: 只能旁观，不能游戏
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class AgainLoginRes
    {
        public AgainLoginRes()
        {
            iUserID = 0;
            iServerTime = 0;
            iTableNum = 0;
            usTableNumExtra = 0;
            cPlayerNum = 0;
        }

        public int iUserID;
        public int iServerTime;
        public ushort iTableNum;
        public ushort usTableNumExtra;
        public sbyte cPlayerNum;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class OtherAgainLoginNotice
    {
        public OtherAgainLoginNotice()
        {
            iUserID = 0;
        }

        public int iUserID;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class JoinAsVisitorRes
    {
        public JoinAsVisitorRes()
        {
            iUserID = 0;
            iTableNum = 0;
            usTableNumExtra = 0;
            cPlayerNum = 0;
        }

        public int iUserID;
        public ushort iTableNum;
        public ushort usTableNumExtra;
        public sbyte cPlayerNum;
    }

    public class KICKOUTTYPE
    {
        public const int KOS_OTHER_PLAYER_LOGIN = 146;       //帐号被其他玩家登录
        public const int USER_WRONG_ACTION = 147;       //服务器验证不通过
        public const int NO_ENOUGH_MONEY_IN_GAME = 148;       //没有足够的钱在游戏中继续玩
        public const int KICK_ACCOUNT_BY_GM = 149;      //帐号被管理员剔除
        public const int KICK_ACCOUNT_OTHER = 150;      //你强行登录剔除别人
        public const int CONTINUOUS_THREETIMES_OUT = 151;      //连续三次不出牌
        public const int ROOM_HAVE_CLOSED = 152;       //房间已经关闭 踢人到其它房间继续游戏
        public const int ILLege_SEND_CARD = 153;      //游戏中非法出牌
        public const int CLINET_TIME_OUT = 154;      //网络超时
        public const int MONEY_OVER_IN_GAME = 155;      //身上钱过多
        public const int SEND_LOCAL_SPEAKER_NO_SPEAKER = 156;      //发送小喇叭，喇叭数量不足
        public const int PLAYER_NOT_ENOUGH = 157;      //游戏人数不足（通比牛牛）
    }
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class KickOutServer
    {
        public KickOutServer()
        {
            ucType = 0;
            iKickUserID = 0;
        }

        public byte ucType;      //剔除类型
        public int iKickUserID; //被踢玩家            
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class GameStartNotice
    {
        public GameStartNotice()
        {
            iPlayerNum = 0;
        }

        public int iPlayerNum;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class SetPointServer
    {
        public SetPointServer()
        {
        }

        public ushort usTableNumExtra;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class TableBaseInfo
    {
        public TableBaseInfo()
        {
            iBasePoint = 0;
            iBaseTimes = 0;
        }

        public int iBasePoint;			// 底注
        public int iBaseTimes;			// 倍率
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class TableBaseInfoEx
    {
        public TableBaseInfoEx()
        {
            iBasePoint = 0;
            iBaseTimes = 0;
            iPointLimitTimes = 0;
            iBasePointCoin = new int[3];
            iHongBaoRMB = new int[3];
            iCostExtraPoint = new int[3];
        }

        public int iBasePoint;			// 底注
        public int iBaseTimes;			// 倍率
        public int iPointLimitTimes;
        [OdaoFieldAttribute(3)]
        public int[] iBasePointCoin;		//红包底注对应的游戏底注
        [OdaoFieldAttribute(3)]
        public int[] iHongBaoRMB;		//红包底注金额 单位为"分" 需要客户端转化为元
        [OdaoFieldAttribute(3)]
        public int[] iCostExtraPoint;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class SetBasePointReq
    {
        public SetBasePointReq()
        {
            iBasePointType = 0;
            iTableNum = 0;
            iBasePoint = 0;
            iSendCardTime = 0;
        }

        public ushort iTableNum;                  //桌号
        public int iBasePoint;                 //桌子底注
        public int iSendCardTime;              //设定出牌时间
        public int iBasePointType;		        //底注类型 1为红包 0为普通底注
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class UrgeCard
    {
        public UrgeCard()
        {
            cType = 0;
            cIndex = 0;
            cAbsPos = 0;
        }

        public byte cType;  //2 - 得意, 3 - 郁闷, 1 - 催促
        public byte cIndex; //语音索引, 对应哪个语音文件播放
        public byte cAbsPos; //玩家(在服务器上的)绝对位置
    }


    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class GameBullNotice
    {
        public GameBullNotice()
        {
            iUserID = 0;
            iGameID = 0;
            iServerID = 0;
            //szContent = new sbyte[200];
        }

        public int iUserID;
        public ushort iGameID;
        public ushort iServerID;
        //[OdaoFieldAttribute(UnmanagedType.ByValArray, SizeConst = 200)]
        //public    sbyte[]  szContent;     //最多允许100汉字
        [OdaoFieldAttribute(200)]
        public string szContent;
    }

    public class VIPKickError
    {
        public const int ERROR_IN_PLAYING = 1;            //游戏中，不能踢人
        public const int ERROR_NO_RIGHT = 2;                  //没有踢人权限
        public const int ERROR_KICKING_VIP = 3;                  //不能踢VIP
        public const int ERROR_KICKEDPLAYER_NOTIN = 4;         //被踢玩家不在线
        public const int ERROR_FULLKICK_CHANCE = 5;                //今天踢人次数已满
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class VIPKickOutPlayerReq
    {
        public VIPKickOutPlayerReq()
        {
            iKickedUserID = 0;
        }

        public int iKickedUserID;               //被踢玩家
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class VIPKickOutPlayerRes
    {
        public VIPKickOutPlayerRes()
        {
            iVIPUserID = 0;
            iError = 0;
        }

        public int iVIPUserID;            //踢人VIP帐号
        public int iError;                //失败原因
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class TableSetPlayerNumber
    {
        public TableSetPlayerNumber()
        {
            iTablePlayerNumber = 0;
        }

        public int iTablePlayerNumber;       //桌子上人数
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class PlayerTableStatus
    {
        public PlayerTableStatus()
        {
            iUserID = 0;
            iIsVisitor = 0;
        }
        public int iUserID;
        public sbyte iIsVisitor;     //是否是旁观者
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class UpdateTablePlayerStatus
    {
        public UpdateTablePlayerStatus()
        {
            iPlayerCount = 0;
            //playerInfo = new PlayerTableStatus[10];
        }

        public int iPlayerCount;
        //[OdaoFieldAttribute(UnmanagedType.Struct, SizeConst = 10)]
        //public PlayerTableStatus[]  playerInfo;  
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class UserBankLoginReq
    {
        public UserBankLoginReq()
        {
            iUserID = 0;
            szBankPass = "";
        }

        public int iUserID;
        [OdaoFieldAttribute(NetworkInterface.OdaoMessageHeaderId.PASSWD_LEN)]
        public string szBankPass;        //银行密码
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class LoginBankResult
    {
        public LoginBankResult()
        {
            ucResultType = 0;
            llGameCoin = 0;
            llBankCoin = 0;
        }

        public byte ucResultType;                  //返回结果编号类型  1 success ...代表各种出错原因
        public long llGameCoin;                    //成功玩家游戏货币  否则返回为0
        public long llBankCoin;                    //成功返回实际数目  否则返回为0
    }

    public class BankError
    {
        public static int LBR_NOFIND_USERID_ERROR = 150;        //用户输入ID错误 userID不存在
        public static int LBR_PASSWORD_ERROR = 151;         //用户密码错误 
        public static int LBR_DATEBASE_ERROR = 152;         //数据库操作(存储过程失败)失败
        public static int LBR_NOMORE_ENOUGH_COIN = 153;         //取钱银行没有足够的钱,存钱玩家手中不足存储金额
        public static int LBR_SAVE_ERROR_STATE = 154;         //银行状态错误
        public static int LBR_CHARGE_REFRESING_MONEY = 155;         //充值同步状态中
        public static int LBR_DBCONNECT_ERROR = 255;         //数据库连接失败
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class GetCoinResult
    {
        public GetCoinResult()
        {
            ucResultType = 0;
            llGetCoinNum = 0;
        }

        public byte ucResultType;                  //取款结果类型 1成功,对应的返回错误的类型
        public long llGetCoinNum;                  //success 返回所取的货币数目 否则返回都为0
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class GetBankCoinReq
    {
        public GetBankCoinReq()
        {
            iUserID = 0;
            llGetNum = 0;
            a = 0;
        }

        public int iUserID;
        //public long llGetNum;
        public int llGetNum;
        public int a;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class SaveCoinReq
    {
        public SaveCoinReq()
        {
            iUserID = 0;
            llSavingNum = 0;
            a = 0;
        }

        public int iUserID;
        //public long  llSavingNum;                       //存款数目
        public int llSavingNum;
        public int a;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class SaveCoinRes
    {
        public SaveCoinRes()
        {
            ucResultType = 0;
            llSelfCoin = 0;
        }

        public byte ucResultType;                 //存款结果类型  1成功  其他返回对应的错误类型
        public long llSelfCoin;                   //返回玩家身上的前数目
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class ModifyBankPasswdReq
    {
        public ModifyBankPasswdReq()
        {
            iUserID = 0;
            szOldPassWd = "";
            szNewPassWd = "";
        }

        public int iUserID;
        [OdaoFieldAttribute(NetworkInterface.OdaoMessageHeaderId.PASSWD_LEN)]
        public string szOldPassWd;
        [OdaoFieldAttribute(NetworkInterface.OdaoMessageHeaderId.PASSWD_LEN)]
        public string szNewPassWd;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class ModifyBankPasswdRes
    {
        public ModifyBankPasswdRes()
        {
            ucResultType = 0;
        }

        public byte ucResultType;               //1success其他返回结果
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class GSNotifyTablePlayersCoinChange
    {
        public GSNotifyTablePlayersCoinChange()
        {
            chair = -1;
            score = 0;
        }

        public int chair;
        public long score;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class UserBaseInfo
    {
        public UserBaseInfo()
        {
            iTableNum = 0;
            iUserID = 0;
        }
        public int iUserID;                        //用户ID
        public ushort iTableNum;                      //桌子位置
        public ushort usTableNumExtra;                //椅子位置
        public ushort usNetDelay;                     //网络延时
        public sbyte iCurUserStatus;                 //当前用户状态
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class UserStatusInfo
    {
        public UserStatusInfo()
        {
            iMsgCount = 0;
        }

        public int iMsgCount;                   //一半情况下都是1....群发时候需要拼包
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class RoomTableStatus
    {
        public RoomTableStatus()
        {
            usTableNum = 0;
        }

        public ushort usTableNum;            //桌子号码
        public sbyte bPlayStatus;			  //游戏状态
        public sbyte bTableType;			   //桌子类型(0普通桌，1红包桌)
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class TableLocked
    {
        public TableLocked()
        {
            usTableNum = 0;
        }

        public ushort usTableNum;            //桌子号码
        public sbyte bTableLock;		     //锁的状态
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class TableLevel
    {
        public TableLevel()
        {
            usTableNum = 0;
        }

        public ushort usTableNum;            //桌子号码
        public sbyte cTableLevel;		     //桌子等级
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class TableSetAnte
    {
        public TableSetAnte()
        {
            usTableNum = 0;
        }

        public ushort usTableNum;            //桌子号码
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class TableAnte
    {
        public TableAnte()
        {
            usTableNum = 0;
            iHongBaoPointRMB = 0;
        }

        public uint ulTableAnte;		//桌子底注
        public ushort usTableNum;            //桌子号码
        public sbyte cDynamicConfig;     //桌子模型 0：正常 1：横向两人桌 2：纵向两人桌
        public int iHongBaoPointRMB;	//红包底注金额单位分
    }


    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class GameRoomInfo
    {
        public GameRoomInfo()
        {
        }

        public uint ulVideoAddr;          //视频地址
        public ushort usGameID;             //类型ID
        public ushort usTableCount;         //桌子数目
        public ushort usChairCount;         //椅子数目
        public ushort usVideoPort;          //视频端口
        public sbyte ucVideoGame;          //视频游戏

        public int iRoomBasePoint;      //房间底注
        public int iRoomLimit;          //房间限制
                                        // int               iMaxTime;          //最大倍率
                                        //扩展配置
        public ushort usServerType;         //房间类型
        public byte ucAntiCheatTable;     //房间入座类型
        public byte ucHideUserInfo;       //隐藏信息
        public byte ucSupportLadderMatch;	//阶梯闯关式比赛
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class SendAllUserInfo
    {
        public SendAllUserInfo()
        {
            iUserCount = 0;
        }
        public int iUserCount;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class UserIntegralInfo
    {
        public UserIntegralInfo()
        {
        }
        public long lScore;                 //用户分数/比赛分数/游戏币
        public int iWinCount;               //胜利盘数
        public int iLostCount;              //失败盘数
        public int iDrawCount;              //和局盘数
        public int iFleeCount;              //断线数目

        public int iExpValue;               //用户经验值
        public int iExpLevel;               //用户经验等级
        public int iCurLevelExperience;     //用户当前等级经验值
        public int iNextLevelExperience;    //下一个等级经验值
        public int iPrizeNum;               //奖品券数量
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class UserInfo
    {
        public UserInfo()
        {
            //userIntegralInfo = new UserIntegralInfo();
        }
        public int iIConID;                  //头像索引
        public int iUserID;                  //用户 I D
        public int iUserLevel;               //用户等级
        public int iUserMasterRight;         //管理权限
        public int iTableNum;                //桌子号码
        public int iNetDelay;                //网络延时
        public int iPhotoKey;
        [OdaoFieldAttribute(NetworkInterface.OdaoMessageHeaderId.NAME_LEN)]
        public string szUserName;     //玩家帐号
        public byte cGender;         //用户性别
        public byte cVIPType;        //会员等级
        public byte cMasterType;     //管理等级

        public byte ucSpecialIdentify; //特殊身份
        public byte cSpecialAvatar;  //特殊头像
        public sbyte cPhotoVerify;
        public ushort usTableNumExtra; //椅子位置
        public byte cbUserStatus;    //用户状态

        //public UserIntegralInfo  userIntegralInfo;//积分信息  //modify 2015/5/26 不同于服务器

        public long lScore;                 //用户分数/比赛分数/游戏币
        public int iWinCount;               //胜利盘数
        public int iLostCount;              //失败盘数
        public int iDrawCount;              //和局盘数
        public int iFleeCount;              //断线数目

        public int iExpValue;               //用户经验值
        public int iExpLevel;               //用户经验等级
        public int iCurLevelExperience;     //用户当前等级经验值
        public int iNextLevelExperience;    //下一个等级经验值
        public int iPrizeNum;               //奖品券数量
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class TableInfo
    {
        public TableInfo()
        {
            iAllTableCount = 0;
        }

        public int iAllTableCount;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class TableStatusInfo
    {
        public TableStatusInfo()
        {
        }
        public sbyte bTableLock;                         //是否锁定
        public sbyte bPlayStatus;                          //是否是游戏状态
        public sbyte iTableLevel;                          //桌子等级
        public sbyte bInSettingPoint;                      //是否正在设置底注
        public int iBasePoint;                           //桌子底注
        public sbyte cDynamicConfig;     //桌子模型 0：正常 1：横向两人桌 2：纵向两人桌
        //public int iHongBaoRMB;				  //红包RMB(单位分)
    }

    public class UserScore
    {
        public int iUserID;
        public int lWinCount;                           //胜利盘数
        public int lLostCount;                          //失败盘数
        public int lDrawCount;                          //和局盘数
        public int lFleeCount;                          //断线数目
        public int lExperience;                     //用户经验
        public uint dwExperienceLevel;                  //经验等级
        public uint dwCurrLevelExperience;              //当前等级经验值
        public uint dwNextLevelExperience;              //下一个等级经验值
        public long llScore;                            //身上钱
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class LobbyUserScore
    {
        public LobbyUserScore()
        {
            //info = new UserScore[10];
        }
        public int iPlayerCount;
        //[OdaoFieldAttribute(UnmanagedType.ByValArray, SizeConst = 10)]
        //[OdaoFieldAttribute(10)]
        //public	UserScore[]   info;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class HongBaoTablePointInfo
    {
        public HongBaoTablePointInfo()
        {
            usTableNumExtra = 0;
            iPointLimitTimes = 0;
            iBasePoint = new int[3];
            iHongBaoRMB = new int[3];
            iCostPoint = new int[3];
            iLimitEnterScore = new int[3];
            fEntryfeeDiscout = 0.9f;

        }
        public ushort usTableNumExtra;
        public int iPointLimitTimes;
        [OdaoFieldAttribute(3)]
        public int[] iBasePoint;		//红包底注对应的游戏底注
        [OdaoFieldAttribute(3)]
        public int[] iHongBaoRMB;		//红包底注金额 单位为"分" 需要客户端转化为元
        [OdaoFieldAttribute(3)]
        public int[] iCostPoint;
        [OdaoFieldAttribute(3)]
        public int[] iLimitEnterScore;   //入桌积分限制   added by hud on 2016-5-27
        public float fEntryfeeDiscout;
    }

    //GAME_USER_PROPEX_MSG 身上的额外道具
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class UserPropExNotice
    {
        public UserPropExNotice()
        {
            lRedPacket = 0;
        }
        public long lRedPacket;                 //红包道具
        public long lProp1;
        public long lProp2;
        public long lProp3;
        public long lProp4;
    }

    public class Server2ClientGetHongBaoServer
    {
        public Server2ClientGetHongBaoServer()
        {
            cTabelNum = 0;
            cPlayerPos = 0;
            iHongBaoRMB = 0;
        }
        public sbyte cTabelNum;	//桌号
        public sbyte cPlayerPos; //玩家位置
        public int iHongBaoRMB; //红包金额
    }

    public class GameClientSiteTypeMsg
    {
        public GameClientSiteTypeMsg()
        {
        }
    }
    //基础消息类型

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class TestConnect
    {
        public byte identity;
        public byte encode;
        public ushort length;
        public byte version;
        public byte reserve;
        public ushort type;
        public byte b1;
    }

    public class LoginType
    {
        public const sbyte USER_LOGIN_ONE = 0;      ///<自动入座登录
        public const sbyte USER_LOGIN_TWO = 1;      ///<选择座位入座 
        public const sbyte USER_LOGIN_THREE = 2;       ///<手机登陆入座
        // USER_LOGIN_ROBOT = 3       ///<机器人登陆类型
    };

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class AuthenReqDef
    {
        public AuthenReqDef()
        {
            iClientSiteType = 1;
        }

        public int iUserID;
        public int iRoomID;//服务器里的房间
        [OdaoFieldAttribute(33)]  //string 序列化处理
        public string szPasswd;
        public sbyte cLoginType;
        //public int iAgentID;
        public int iClientSiteType;				///* chw 160415 begin */
    };

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class AuthenResDef
    {
        public AuthenResDef()
        {
        }

        public sbyte cError;
        public int iPlayerServerID;
        public int iPlayerGameID;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class LobbyLoginReq
    {
        [MessagePackMember(0)]
        public Int32 iAccountID;
        [MessagePackMember(1)]
        public Int32 iServiceID;
        [MessagePackMember(2)]
        public string strToken;
    }
}

/// <summary>
/// 服务器是否开启IOS充值功能
/// </summary>
public class Server2ClientIfOpenRecharge
{
    public Server2ClientIfOpenRecharge()
    {
        cIfOpenRecharge = 0;
    }
    //是否开启注册
    public sbyte cIfOpenRecharge;


}

#endregion

