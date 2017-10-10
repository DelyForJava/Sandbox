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
    public class BeanHallLoginEventWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			Utils.BeginObjectRegister(typeof(Bean.Hall.LoginEvent), L, translator, 0, 6, 3, 3);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnClickWechatLogin", _m_OnClickWechatLogin);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnClickTouristLogin", _m_OnClickTouristLogin);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Regist", _m_Regist);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ECBEncrypt", _m_ECBEncrypt);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ECBDecrypt", _m_ECBDecrypt);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Md5Sum", _m_Md5Sum);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "isTestModel", _g_get_isTestModel);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "message", _g_get_message);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ssdk", _g_get_ssdk);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "isTestModel", _s_set_isTestModel);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "message", _s_set_message);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ssdk", _s_set_ssdk);
            
			Utils.EndObjectRegister(typeof(Bean.Hall.LoginEvent), L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(typeof(Bean.Hall.LoginEvent), L, __CreateInstance, 1, 1, 1);
			
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "ipRes", _g_get_ipRes);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "ipRes", _s_set_ipRes);
            
			Utils.EndClassRegister(typeof(Bean.Hall.LoginEvent), L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					Bean.Hall.LoginEvent __cl_gen_ret = new Bean.Hall.LoginEvent();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Bean.Hall.LoginEvent constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnClickWechatLogin(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Bean.Hall.LoginEvent __cl_gen_to_be_invoked = (Bean.Hall.LoginEvent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.OnClickWechatLogin(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnClickTouristLogin(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Bean.Hall.LoginEvent __cl_gen_to_be_invoked = (Bean.Hall.LoginEvent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.OnClickTouristLogin(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Regist(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Bean.Hall.LoginEvent __cl_gen_to_be_invoked = (Bean.Hall.LoginEvent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Bean.Hall.Source scource;translator.Get(L, 2, out scource);
                    
                    __cl_gen_to_be_invoked.Regist( scource );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ECBEncrypt(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Bean.Hall.LoginEvent __cl_gen_to_be_invoked = (Bean.Hall.LoginEvent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string pToEncrypt = LuaAPI.lua_tostring(L, 2);
                    string sKey = LuaAPI.lua_tostring(L, 3);
                    
                        string __cl_gen_ret = __cl_gen_to_be_invoked.ECBEncrypt( pToEncrypt, sKey );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ECBDecrypt(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Bean.Hall.LoginEvent __cl_gen_to_be_invoked = (Bean.Hall.LoginEvent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string pToDecrypt = LuaAPI.lua_tostring(L, 2);
                    string sKey = LuaAPI.lua_tostring(L, 3);
                    
                        string __cl_gen_ret = __cl_gen_to_be_invoked.ECBDecrypt( pToDecrypt, sKey );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Md5Sum(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Bean.Hall.LoginEvent __cl_gen_to_be_invoked = (Bean.Hall.LoginEvent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string input = LuaAPI.lua_tostring(L, 2);
                    
                        string __cl_gen_ret = __cl_gen_to_be_invoked.Md5Sum( input );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isTestModel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Bean.Hall.LoginEvent __cl_gen_to_be_invoked = (Bean.Hall.LoginEvent)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.isTestModel);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_message(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Bean.Hall.LoginEvent __cl_gen_to_be_invoked = (Bean.Hall.LoginEvent)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.message);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ssdk(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Bean.Hall.LoginEvent __cl_gen_to_be_invoked = (Bean.Hall.LoginEvent)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.ssdk);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ipRes(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, Bean.Hall.LoginEvent.ipRes);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_isTestModel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Bean.Hall.LoginEvent __cl_gen_to_be_invoked = (Bean.Hall.LoginEvent)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.isTestModel = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_message(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Bean.Hall.LoginEvent __cl_gen_to_be_invoked = (Bean.Hall.LoginEvent)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.message = (UnityEngine.UI.Text)translator.GetObject(L, 2, typeof(UnityEngine.UI.Text));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ssdk(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Bean.Hall.LoginEvent __cl_gen_to_be_invoked = (Bean.Hall.LoginEvent)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.ssdk = (cn.sharesdk.unity3d.ShareSDK)translator.GetObject(L, 2, typeof(cn.sharesdk.unity3d.ShareSDK));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ipRes(RealStatePtr L)
        {
		    try {
                
			    Bean.Hall.LoginEvent.ipRes = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
