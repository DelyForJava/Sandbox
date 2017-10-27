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
            LoginData.Instance.OnClickTouristLogin();

        }
        public static void OnClickWechat()
        {
            LoginData.Instance.OnClickWechatLogin();

        }

        public static Action LuaOnLogin;
        public static Action LuaOnChangeGender;
        public static Action LuaOnChangeName;

        public static void RegistLuaAction(LuaTable table)
        {
            var l = LoginData.Instance;
            table.Get("OnLoginCSCallLua", out LuaOnLogin);
            table.Get("OnChangeGenderCSCallLua", out LuaOnChangeGender);
            table.Get("OnChangeNameCSCallLua", out LuaOnChangeName);
        }
        // -------------------------------------------------  hall ---------------------------------------------------------//
        

    }

}