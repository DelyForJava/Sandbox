using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bean.Hall
{

    public  class HallData
    {
        public static  int iUserID { get; set; }

        public static int accountId { get; set; }

        public static  int serviceId { get; set; }

        public static string szPasswdToken { get; set; }

        public static sbyte cGender { get; set; }

        public static sbyte cVipLv { get; set; }

        public static  long llGameCoin { get; set; }

        public static long llBankCoin { get; set; }

        public static long llDiamondNum { get; set; }

        public static long llGoldBean { get; set; }

        public static string szNickName { get; set; }

        public static string szWXIconURL { get; set; }

        public static string szWXNickName { get; set; }
        public static int iVipExp { get; set; }
        public static sbyte cLevel { get; set; }
        public static int iLevelExp { get; set; }
        public static sbyte cFllowWechat { get; set; }

        /// <summary>
        /// 0.未充值 1.已充未领 2.已领
        /// </summary>
        public static sbyte cFirstRecharge { get; set; }
        public static sbyte cMonthCardCoin { get; set; }
        public static sbyte cMonthCardDiamond { get; set; }
        public static sbyte cMonthCardSuper { get; set; }
    }

}
