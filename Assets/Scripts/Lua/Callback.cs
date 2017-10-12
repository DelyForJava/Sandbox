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
        public static void OnClickWechat()
        {
            LoginEvent.Instance.OnClickWechatLogin();

        }

        public static Action LuaOnLogin;
        public static void RegistLuaAction(LuaTable table)
        {
            var l = LoginEvent.Instance;
            table.Get("OnLoginCSCallLua", out LuaOnLogin);

        }
        // -------------------------------------------------  hall ---------------------------------------------------------//


    }

}