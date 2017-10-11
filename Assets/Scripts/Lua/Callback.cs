using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace Bean.Hall
{
    public static class Callback
    {
        // -------------------------------------------------  login ---------------------------------------------------------//
        public static void OnClickTourist()
        {
            LoginEvent.Instance.OnClickTouristLogin();

        }

        public static Action LuaOnClickTourist;
        public static void RegistLuaAction(LuaTable table)
        {
            table.Get("OnClickTouristCSCallLua", out LuaOnClickTourist);

        }


        public static void OnClickWechat()
        {

        }

        // -------------------------------------------------  hall ---------------------------------------------------------//


}

}