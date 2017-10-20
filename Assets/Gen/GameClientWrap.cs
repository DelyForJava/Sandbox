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
    public class GameClientWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			Utils.BeginObjectRegister(typeof(GameClient), L, translator, 0, 7, 9, 7);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "EventAwake", _m_EventAwake);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "EventStart", _m_EventStart);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "EventUpdate", _m_EventUpdate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "EventFixedUpdate", _m_EventFixedUpdate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "EventDestroy", _m_EventDestroy);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "EventApplicationQuit", _m_EventApplicationQuit);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsInMahjongGame", _m_IsInMahjongGame);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "CurrentGameMode", _g_get_CurrentGameMode);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "MahjongGamePlayer", _g_get_MahjongGamePlayer);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "MG", _g_get_MG);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "AssetLoader", _g_get_AssetLoader);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "SpecialCard", _g_get_SpecialCard);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "SpecialType", _g_get_SpecialType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "GameStateDef", _g_get_GameStateDef);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Port", _g_get_Port);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "UserId", _g_get_UserId);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "CurrentGameMode", _s_set_CurrentGameMode);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "AssetLoader", _s_set_AssetLoader);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "SpecialCard", _s_set_SpecialCard);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "SpecialType", _s_set_SpecialType);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "GameStateDef", _s_set_GameStateDef);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "Port", _s_set_Port);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "UserId", _s_set_UserId);
            
			Utils.EndObjectRegister(typeof(GameClient), L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(typeof(GameClient), L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			Utils.EndClassRegister(typeof(GameClient), L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "GameClient does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EventAwake(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameClient __cl_gen_to_be_invoked = (GameClient)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.EventAwake(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EventStart(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameClient __cl_gen_to_be_invoked = (GameClient)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.EventStart(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EventUpdate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameClient __cl_gen_to_be_invoked = (GameClient)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.EventUpdate(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EventFixedUpdate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameClient __cl_gen_to_be_invoked = (GameClient)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float time = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    __cl_gen_to_be_invoked.EventFixedUpdate( time );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EventDestroy(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameClient __cl_gen_to_be_invoked = (GameClient)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.EventDestroy(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EventApplicationQuit(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameClient __cl_gen_to_be_invoked = (GameClient)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.EventApplicationQuit(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsInMahjongGame(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameClient __cl_gen_to_be_invoked = (GameClient)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.IsInMahjongGame(  );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CurrentGameMode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameClient __cl_gen_to_be_invoked = (GameClient)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.CurrentGameMode);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_MahjongGamePlayer(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameClient __cl_gen_to_be_invoked = (GameClient)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.MahjongGamePlayer);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_MG(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameClient __cl_gen_to_be_invoked = (GameClient)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.MG);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_AssetLoader(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameClient __cl_gen_to_be_invoked = (GameClient)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.AssetLoader);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SpecialCard(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameClient __cl_gen_to_be_invoked = (GameClient)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.SpecialCard);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SpecialType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameClient __cl_gen_to_be_invoked = (GameClient)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.SpecialType);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_GameStateDef(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameClient __cl_gen_to_be_invoked = (GameClient)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.GameStateDef);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Port(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameClient __cl_gen_to_be_invoked = (GameClient)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.Port);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UserId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameClient __cl_gen_to_be_invoked = (GameClient)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.UserId);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CurrentGameMode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameClient __cl_gen_to_be_invoked = (GameClient)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.CurrentGameMode = (GameMode)translator.GetObject(L, 2, typeof(GameMode));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_AssetLoader(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameClient __cl_gen_to_be_invoked = (GameClient)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.AssetLoader = (IAssetLoader)translator.GetObject(L, 2, typeof(IAssetLoader));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_SpecialCard(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameClient __cl_gen_to_be_invoked = (GameClient)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.SpecialCard = (byte)LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_SpecialType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameClient __cl_gen_to_be_invoked = (GameClient)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.SpecialType = (byte)LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_GameStateDef(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameClient __cl_gen_to_be_invoked = (GameClient)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.GameStateDef = (MP.GameMessage.GameStateDef)translator.GetObject(L, 2, typeof(MP.GameMessage.GameStateDef));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Port(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameClient __cl_gen_to_be_invoked = (GameClient)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.Port = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UserId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameClient __cl_gen_to_be_invoked = (GameClient)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.UserId = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
