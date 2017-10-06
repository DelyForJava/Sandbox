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
    public class BeanHallGlobalDataWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			Utils.BeginObjectRegister(typeof(Bean.Hall.GlobalData), L, translator, 0, 0, 1, 1);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Localization", _g_get_Localization);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "Localization", _s_set_Localization);
            
			Utils.EndObjectRegister(typeof(Bean.Hall.GlobalData), L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(typeof(Bean.Hall.GlobalData), L, __CreateInstance, 1, 16, 16);
			
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "CompressIndex", _g_get_CompressIndex);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Url", _g_get_Url);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "ChannelId", _g_get_ChannelId);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "AppId", _g_get_AppId);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "CurrentVersionId", _g_get_CurrentVersionId);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "VerifyVersionId", _g_get_VerifyVersionId);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "ChannelName", _g_get_ChannelName);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "DownloadUrl", _g_get_DownloadUrl);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "LobbyConfigUrl", _g_get_LobbyConfigUrl);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "UpdateDesription", _g_get_UpdateDesription);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "ModifyTime", _g_get_ModifyTime);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "SwitchDns", _g_get_SwitchDns);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "HotupdateTips", _g_get_HotupdateTips);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "StepTips", _g_get_StepTips);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Cdn", _g_get_Cdn);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "CompressTips", _g_get_CompressTips);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "CompressIndex", _s_set_CompressIndex);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "Url", _s_set_Url);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "ChannelId", _s_set_ChannelId);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "AppId", _s_set_AppId);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "CurrentVersionId", _s_set_CurrentVersionId);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "VerifyVersionId", _s_set_VerifyVersionId);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "ChannelName", _s_set_ChannelName);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "DownloadUrl", _s_set_DownloadUrl);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "LobbyConfigUrl", _s_set_LobbyConfigUrl);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "UpdateDesription", _s_set_UpdateDesription);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "ModifyTime", _s_set_ModifyTime);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "SwitchDns", _s_set_SwitchDns);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "HotupdateTips", _s_set_HotupdateTips);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "StepTips", _s_set_StepTips);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "Cdn", _s_set_Cdn);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "CompressTips", _s_set_CompressTips);
            
			Utils.EndClassRegister(typeof(Bean.Hall.GlobalData), L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					Bean.Hall.GlobalData __cl_gen_ret = new Bean.Hall.GlobalData();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Bean.Hall.GlobalData constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CompressIndex(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, Bean.Hall.GlobalData.CompressIndex);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Url(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, Bean.Hall.GlobalData.Url);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ChannelId(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, Bean.Hall.GlobalData.ChannelId);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_AppId(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, Bean.Hall.GlobalData.AppId);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CurrentVersionId(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, Bean.Hall.GlobalData.CurrentVersionId);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_VerifyVersionId(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, Bean.Hall.GlobalData.VerifyVersionId);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ChannelName(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, Bean.Hall.GlobalData.ChannelName);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DownloadUrl(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, Bean.Hall.GlobalData.DownloadUrl);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LobbyConfigUrl(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, Bean.Hall.GlobalData.LobbyConfigUrl);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UpdateDesription(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, Bean.Hall.GlobalData.UpdateDesription);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ModifyTime(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, Bean.Hall.GlobalData.ModifyTime);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SwitchDns(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, Bean.Hall.GlobalData.SwitchDns);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Localization(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Bean.Hall.GlobalData __cl_gen_to_be_invoked = (Bean.Hall.GlobalData)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.Localization);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_HotupdateTips(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, Bean.Hall.GlobalData.HotupdateTips);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_StepTips(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, Bean.Hall.GlobalData.StepTips);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Cdn(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, Bean.Hall.GlobalData.Cdn);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CompressTips(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, Bean.Hall.GlobalData.CompressTips);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CompressIndex(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.GlobalData.CompressIndex = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Url(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.GlobalData.Url = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ChannelId(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.GlobalData.ChannelId = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_AppId(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.GlobalData.AppId = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CurrentVersionId(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.GlobalData.CurrentVersionId = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_VerifyVersionId(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.GlobalData.VerifyVersionId = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ChannelName(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.GlobalData.ChannelName = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DownloadUrl(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.GlobalData.DownloadUrl = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LobbyConfigUrl(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.GlobalData.LobbyConfigUrl = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UpdateDesription(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.GlobalData.UpdateDesription = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ModifyTime(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.GlobalData.ModifyTime = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_SwitchDns(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.GlobalData.SwitchDns = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Localization(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Bean.Hall.GlobalData __cl_gen_to_be_invoked = (Bean.Hall.GlobalData)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.Localization = (System.Collections.Generic.Dictionary<string, string>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<string, string>));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_HotupdateTips(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    Bean.Hall.GlobalData.HotupdateTips = (string[])translator.GetObject(L, 1, typeof(string[]));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_StepTips(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    Bean.Hall.GlobalData.StepTips = (string[])translator.GetObject(L, 1, typeof(string[]));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Cdn(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.GlobalData.Cdn = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CompressTips(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    Bean.Hall.GlobalData.CompressTips = (string[])translator.GetObject(L, 1, typeof(string[]));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
