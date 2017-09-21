namespace odao.scmahjong
{
    public class TileDef
    {
        public enum Kind
        {
            CRAK = 0,      //万子(1万 - 9万)
            BAM = 1,      //条子(1条 - 9条)
            DOT = 2,      //筒子(1筒 - 9筒)
            FENG = 3,      //风牌和箭牌(东、南、西、北 中、发、白)
            HUA = 4,       //花牌(春夏秋冬)
            NONE = 12,     //没有赋给类型，即空类型
        }

        /*
        //麻将的点数
        const char MJ_POINT_1 = 1;          //东 春
        const char MJ_POINT_2 = 2;          //南 夏
        const char MJ_POINT_3 = 3;          //西 秋
        const char MJ_POINT_4 = 4;          //北 冬
        const char MJ_POINT_5 = 5;          //中 梅
        const char MJ_POINT_6 = 6;          //发 兰			
        const char MJ_POINT_7 = 7;          //白 竹
        const char MJ_POINT_8 = 8;          //	 菊
        const char MJ_POINT_9 = 9;
        const char MJ_POINT_NONE = 12;

        const int INVALID_CARD_ID		=   -1;			//无效牌ID
        const int MJ_TYPE_BACK			=	1;			//背面
        const int MJ_POINT_BACK			=	7;			//背面

        //指定哪种牌是花牌
        const char MJ_HUA_TYPE			=	MJ_TYPE_HUA;		//花牌(春夏秋冬)
        
        //台湾麻将中关于吃牌的方式的定义
        // const char MJ_EAT_FRONT			=	1;						//吃牌的是前面两张 如1 2 吃3
        // const char MJ_EAT_MIDDLE			=	2;						//吃牌的是夹的两张 如1 3 吃2
        // const char MJ_EAT_BEHAND			=	4;						//吃牌的是后面两张 如2 3 吃1
        */

		public enum ComboType
		{
			NONE = -1,
			CHOW = 0, 
			PONG, 
			KONG, 
			KONG_DARK, 
			KONG_TURN, 
			BAO_TING,
			WIN, 
			WIN_AFTER_KONG_TURN,
			WIN_SELF, 
			PASS,
			PASS_CANCEL,
			NUM
		}

        //high 4bit is type
        //low 4bit is point
        byte _value;
        public byte Value { get { return _value; } }
        TileDef.Kind _kind;

        public int SortID = 0;

        private TileDef() { }

        private TileDef(byte value)
        {
            _value = value;
            _kind = (TileDef.Kind)((_value >> 4) & 0x0f);
            SortID = _value;
        }

        public static bool IsValid(byte value)
        {
            return value > 0;
        }

        public static TileDef Create(byte value)
        {
            TileDef def = new TileDef(value);
            return def;
        }

		public static TileDef Create()
		{
			TileDef def = null;
			System.Random RNG = new System.Random();
			int kind = RNG.Next (0, 3);
			int point = RNG.Next (1, 10);
			def = Create ((byte)(kind << 4 | point));
			UnityEngine.Debug.Log (def.ToString ());
			return def;
		}

        public TileDef.Kind GetKind()
        {
            return _kind;
        } 

        public int GetPoint()
        {
            return (int)(_value & 0x0f);
        }

        public static int Comparison(TileDef a, TileDef b)
        {
            if ((int)a.SortID > (int)b.SortID)
            {
                return 1;
            }

            if ((int)a.SortID < (int)b.SortID)
            {
                return -1;
            }

            return 0;
        }

        public string ToString()
        {
			string k = "unvalid kind";
            switch(GetKind())
            {
			case Kind.CRAK:
				k = "wan";
				break;
			case Kind.BAM:
				k = "tiao";
				break;
			case Kind.DOT:
				k = "tong";
				break;
			default:
				break;
            }
            return GetPoint() + k;
        }
    }
}


