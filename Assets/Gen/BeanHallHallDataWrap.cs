#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class BeanHallHallDataWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			Utils.BeginObjectRegister(typeof(Bean.Hall.HallData), L, translator, 0, 0, 0, 0);
			
			
			
			
			
			Utils.EndObjectRegister(typeof(Bean.Hall.HallData), L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(typeof(Bean.Hall.HallData), L, __CreateInstance, 1, 21, 21);
			
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "iUserID", _g_get_iUserID);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "accountId", _g_get_accountId);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "serviceId", _g_get_serviceId);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "szPasswdToken", _g_get_szPasswdToken);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "cGender", _g_get_cGender);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "cVipLv", _g_get_cVipLv);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "llGameCoin", _g_get_llGameCoin);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "llBankCoin", _g_get_llBankCoin);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "llDiamondNum", _g_get_llDiamondNum);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "llGoldBean", _g_get_llGoldBean);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "szNickName", _g_get_szNickName);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "szWXIconURL", _g_get_szWXIconURL);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "szWXNickName", _g_get_szWXNickName);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "iVipExp", _g_get_iVipExp);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "cLevel", _g_get_cLevel);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "iLevelExp", _g_get_iLevelExp);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "cFllowWechat", _g_get_cFllowWechat);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "cFirstRecharge", _g_get_cFirstRecharge);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "cMonthCardCoin", _g_get_cMonthCardCoin);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "cMonthCardDiamond", _g_get_cMonthCardDiamond);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "cMonthCardSuper", _g_get_cMonthCardSuper);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "iUserID", _s_set_iUserID);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "accountId", _s_set_accountId);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "serviceId", _s_set_serviceId);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "szPasswdToken", _s_set_szPasswdToken);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "cGender", _s_set_cGender);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "cVipLv", _s_set_cVipLv);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "llGameCoin", _s_set_llGameCoin);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "llBankCoin", _s_set_llBankCoin);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "llDiamondNum", _s_set_llDiamondNum);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "llGoldBean", _s_set_llGoldBean);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "szNickName", _s_set_szNickName);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "szWXIconURL", _s_set_szWXIconURL);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "szWXNickName", _s_set_szWXNickName);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "iVipExp", _s_set_iVipExp);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "cLevel", _s_set_cLevel);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "iLevelExp", _s_set_iLevelExp);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "cFllowWechat", _s_set_cFllowWechat);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "cFirstRecharge", _s_set_cFirstRecharge);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "cMonthCardCoin", _s_set_cMonthCardCoin);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "cMonthCardDiamond", _s_set_cMonthCardDiamond);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "cMonthCardSuper", _s_set_cMonthCardSuper);
            
			Utils.EndClassRegister(typeof(Bean.Hall.HallData), L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					Bean.Hall.HallData __cl_gen_ret = new Bean.Hall.HallData();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Bean.Hall.HallData constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_iUserID(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, Bean.Hall.HallData.iUserID);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_accountId(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, Bean.Hall.HallData.accountId);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_serviceId(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, Bean.Hall.HallData.serviceId);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_szPasswdToken(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, Bean.Hall.HallData.szPasswdToken);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_cGender(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, Bean.Hall.HallData.cGender);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_cVipLv(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, Bean.Hall.HallData.cVipLv);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_llGameCoin(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushint64(L, Bean.Hall.HallData.llGameCoin);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_llBankCoin(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushint64(L, Bean.Hall.HallData.llBankCoin);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_llDiamondNum(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushint64(L, Bean.Hall.HallData.llDiamondNum);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_llGoldBean(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushint64(L, Bean.Hall.HallData.llGoldBean);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_szNickName(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, Bean.Hall.HallData.szNickName);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_szWXIconURL(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, Bean.Hall.HallData.szWXIconURL);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_szWXNickName(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, Bean.Hall.HallData.szWXNickName);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_iVipExp(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, Bean.Hall.HallData.iVipExp);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_cLevel(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, Bean.Hall.HallData.cLevel);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_iLevelExp(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, Bean.Hall.HallData.iLevelExp);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_cFllowWechat(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, Bean.Hall.HallData.cFllowWechat);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_cFirstRecharge(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, Bean.Hall.HallData.cFirstRecharge);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_cMonthCardCoin(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, Bean.Hall.HallData.cMonthCardCoin);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_cMonthCardDiamond(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, Bean.Hall.HallData.cMonthCardDiamond);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_cMonthCardSuper(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, Bean.Hall.HallData.cMonthCardSuper);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_iUserID(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.HallData.iUserID = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_accountId(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.HallData.accountId = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_serviceId(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.HallData.serviceId = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_szPasswdToken(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.HallData.szPasswdToken = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_cGender(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.HallData.cGender = (sbyte)LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_cVipLv(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.HallData.cVipLv = (sbyte)LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_llGameCoin(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.HallData.llGameCoin = LuaAPI.lua_toint64(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_llBankCoin(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.HallData.llBankCoin = LuaAPI.lua_toint64(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_llDiamondNum(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.HallData.llDiamondNum = LuaAPI.lua_toint64(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_llGoldBean(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.HallData.llGoldBean = LuaAPI.lua_toint64(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_szNickName(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.HallData.szNickName = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_szWXIconURL(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.HallData.szWXIconURL = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_szWXNickName(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.HallData.szWXNickName = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_iVipExp(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.HallData.iVipExp = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_cLevel(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.HallData.cLevel = (sbyte)LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_iLevelExp(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.HallData.iLevelExp = (sbyte)LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_cFllowWechat(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.HallData.cFllowWechat = (sbyte)LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_cFirstRecharge(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.HallData.cFirstRecharge = (sbyte)LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_cMonthCardCoin(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.HallData.cMonthCardCoin = (sbyte)LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_cMonthCardDiamond(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.HallData.cMonthCardDiamond = (sbyte)LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_cMonthCardSuper(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.HallData.cMonthCardSuper = (sbyte)LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
