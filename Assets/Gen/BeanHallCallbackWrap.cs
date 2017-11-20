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
    public class BeanHallCallbackWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			Utils.BeginObjectRegister(typeof(Bean.Hall.Callback), L, translator, 0, 0, 0, 0);
			
			
			
			
			
			Utils.EndObjectRegister(typeof(Bean.Hall.Callback), L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(typeof(Bean.Hall.Callback), L, __CreateInstance, 4, 5, 5);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "OnClickTourist", _m_OnClickTourist_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "OnClickWechat", _m_OnClickWechat_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RegistLuaAction", _m_RegistLuaAction_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "LuaOnLogin", _g_get_LuaOnLogin);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "LuaOnChangeGender", _g_get_LuaOnChangeGender);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "LuaOnChangeName", _g_get_LuaOnChangeName);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "LuaOnFirstRecharge", _g_get_LuaOnFirstRecharge);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "LuaOnMonthcard", _g_get_LuaOnMonthcard);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "LuaOnLogin", _s_set_LuaOnLogin);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "LuaOnChangeGender", _s_set_LuaOnChangeGender);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "LuaOnChangeName", _s_set_LuaOnChangeName);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "LuaOnFirstRecharge", _s_set_LuaOnFirstRecharge);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "LuaOnMonthcard", _s_set_LuaOnMonthcard);
            
			Utils.EndClassRegister(typeof(Bean.Hall.Callback), L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "Bean.Hall.Callback does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnClickTourist_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    Bean.Hall.Callback.OnClickTourist(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnClickWechat_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    Bean.Hall.Callback.OnClickWechat(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RegistLuaAction_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    XLua.LuaTable table = (XLua.LuaTable)translator.GetObject(L, 1, typeof(XLua.LuaTable));
                    
                    Bean.Hall.Callback.RegistLuaAction( table );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LuaOnLogin(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, Bean.Hall.Callback.LuaOnLogin);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LuaOnChangeGender(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, Bean.Hall.Callback.LuaOnChangeGender);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LuaOnChangeName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, Bean.Hall.Callback.LuaOnChangeName);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LuaOnFirstRecharge(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, Bean.Hall.Callback.LuaOnFirstRecharge);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LuaOnMonthcard(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, Bean.Hall.Callback.LuaOnMonthcard);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LuaOnLogin(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    Bean.Hall.Callback.LuaOnLogin = translator.GetDelegate<System.Action>(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LuaOnChangeGender(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    Bean.Hall.Callback.LuaOnChangeGender = translator.GetDelegate<System.Action>(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LuaOnChangeName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    Bean.Hall.Callback.LuaOnChangeName = translator.GetDelegate<System.Action>(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LuaOnFirstRecharge(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    Bean.Hall.Callback.LuaOnFirstRecharge = translator.GetDelegate<System.Action>(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LuaOnMonthcard(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    Bean.Hall.Callback.LuaOnMonthcard = translator.GetDelegate<System.Action<sbyte, sbyte, int>>(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
