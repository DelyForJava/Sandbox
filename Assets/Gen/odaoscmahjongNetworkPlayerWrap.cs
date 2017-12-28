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
    public class odaoscmahjongNetworkPlayerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			Utils.BeginObjectRegister(typeof(odao.scmahjong.NetworkPlayer), L, translator, 0, 32, 1, 1);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DispenseSend", _m_DispenseSend);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ReDispenseSend", _m_ReDispenseSend);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoginToRoomServerSend", _m_LoginToRoomServerSend);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetChannelInfoSend", _m_GetChannelInfoSend);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GameMatchSend", _m_GameMatchSend);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GameExchangeCardsSend", _m_GameExchangeCardsSend);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InitCallBack", _m_InitCallBack);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InitBaseMessage", _m_InitBaseMessage);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InitGameMessage", _m_InitGameMessage);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OriginMsgLoginReqDef", _m_OriginMsgLoginReqDef);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "BuyGoldReqDef", _m_BuyGoldReqDef);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ChangeNameReqDef", _m_ChangeNameReqDef);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ChangeGenderReqDef", _m_ChangeGenderReqDef);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PackageInfoReqDef", _m_PackageInfoReqDef);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PackageAddItemReqDef", _m_PackageAddItemReqDef);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PackageRemoveItemReqDef", _m_PackageRemoveItemReqDef);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PackageUpdateItemReqDef", _m_PackageUpdateItemReqDef);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ExchangeReqDef", _m_ExchangeReqDef);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "FirstRechargeReqDef", _m_FirstRechargeReqDef);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "MonthCardReqDef", _m_MonthCardReqDef);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TaskInfoReqDef", _m_TaskInfoReqDef);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TaskSubmitReqDef", _m_TaskSubmitReqDef);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TaskProgressUpdateReqDef", _m_TaskProgressUpdateReqDef);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TaskDegreeGetReqDef", _m_TaskDegreeGetReqDef);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoginReqDef", _m_LoginReqDef);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StreamToBytes", _m_StreamToBytes);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "BytesToStream", _m_BytesToStream);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LobbyLoginReqDef", _m_LobbyLoginReqDef);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddEventListener", _m_AddEventListener);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ConnectGameServer", _m_ConnectGameServer);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsSpecialType", _m_IsSpecialType);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ToComboType", _m_ToComboType);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Info", _g_get_Info);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "Info", _s_set_Info);
            
			Utils.EndObjectRegister(typeof(odao.scmahjong.NetworkPlayer), L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(typeof(odao.scmahjong.NetworkPlayer), L, __CreateInstance, 2, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "Create", _m_Create_xlua_st_);
            
			
            
			
			
			Utils.EndClassRegister(typeof(odao.scmahjong.NetworkPlayer), L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "odao.scmahjong.NetworkPlayer does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DispenseSend(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.DispenseSend(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReDispenseSend(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.ReDispenseSend(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoginToRoomServerSend(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.LoginToRoomServerSend(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetChannelInfoSend(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.GetChannelInfoSend(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GameMatchSend(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.GameMatchSend(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GameExchangeCardsSend(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    sbyte HuanPai_1 = (sbyte)LuaAPI.xlua_tointeger(L, 2);
                    sbyte HuanPai_2 = (sbyte)LuaAPI.xlua_tointeger(L, 3);
                    sbyte HuanPai_3 = (sbyte)LuaAPI.xlua_tointeger(L, 4);
                    sbyte iCardNum = (sbyte)LuaAPI.xlua_tointeger(L, 5);
                    
                    __cl_gen_to_be_invoked.GameExchangeCardsSend( HuanPai_1, HuanPai_2, HuanPai_3, iCardNum );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InitCallBack(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.InitCallBack(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InitBaseMessage(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    NetworkInterface.OdaoClient client = (NetworkInterface.OdaoClient)translator.GetObject(L, 2, typeof(NetworkInterface.OdaoClient));
                    
                    __cl_gen_to_be_invoked.InitBaseMessage( client );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InitGameMessage(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    NetworkInterface.OdaoClient client = (NetworkInterface.OdaoClient)translator.GetObject(L, 2, typeof(NetworkInterface.OdaoClient));
                    
                    __cl_gen_to_be_invoked.InitGameMessage( client );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OriginMsgLoginReqDef(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int accountId = LuaAPI.xlua_tointeger(L, 2);
                    string token = LuaAPI.lua_tostring(L, 3);
                    string machineId = LuaAPI.lua_tostring(L, 4);
                    Bean.Hall.Source source;translator.Get(L, 5, out source);
                    
                    __cl_gen_to_be_invoked.OriginMsgLoginReqDef( accountId, token, machineId, source );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_BuyGoldReqDef(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    short sShopID = (short)LuaAPI.xlua_tointeger(L, 2);
                    short sItemIndex = (short)LuaAPI.xlua_tointeger(L, 3);
                    
                    __cl_gen_to_be_invoked.BuyGoldReqDef( sShopID, sItemIndex );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ChangeNameReqDef(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string szNickName = LuaAPI.lua_tostring(L, 2);
                    
                    __cl_gen_to_be_invoked.ChangeNameReqDef( szNickName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ChangeGenderReqDef(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int cGender = LuaAPI.xlua_tointeger(L, 2);
                    
                    __cl_gen_to_be_invoked.ChangeGenderReqDef( cGender );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PackageInfoReqDef(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int iUserID = LuaAPI.xlua_tointeger(L, 2);
                    
                    __cl_gen_to_be_invoked.PackageInfoReqDef( iUserID );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PackageAddItemReqDef(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int iItemID = LuaAPI.xlua_tointeger(L, 2);
                    int iAddNum = LuaAPI.xlua_tointeger(L, 3);
                    
                    __cl_gen_to_be_invoked.PackageAddItemReqDef( iItemID, iAddNum );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PackageRemoveItemReqDef(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int iItemID = LuaAPI.xlua_tointeger(L, 2);
                    int iRemoveNum = LuaAPI.xlua_tointeger(L, 3);
                    
                    __cl_gen_to_be_invoked.PackageRemoveItemReqDef( iItemID, iRemoveNum );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PackageUpdateItemReqDef(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int iItemID = LuaAPI.xlua_tointeger(L, 2);
                    sbyte cIsNew = (sbyte)LuaAPI.xlua_tointeger(L, 3);
                    
                    __cl_gen_to_be_invoked.PackageUpdateItemReqDef( iItemID, cIsNew );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ExchangeReqDef(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    ushort usType = (ushort)LuaAPI.xlua_tointeger(L, 2);
                    ushort usItemIndex = (ushort)LuaAPI.xlua_tointeger(L, 3);
                    string szUserMobile = LuaAPI.lua_tostring(L, 4);
                    string szUserTel = LuaAPI.lua_tostring(L, 5);
                    string szUserAddress = LuaAPI.lua_tostring(L, 6);
                    
                    __cl_gen_to_be_invoked.ExchangeReqDef( usType, usItemIndex, szUserMobile, szUserTel, szUserAddress );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FirstRechargeReqDef(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.FirstRechargeReqDef(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_MonthCardReqDef(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    sbyte cCardType = (sbyte)LuaAPI.xlua_tointeger(L, 2);
                    sbyte cOption = (sbyte)LuaAPI.xlua_tointeger(L, 3);
                    
                    __cl_gen_to_be_invoked.MonthCardReqDef( cCardType, cOption );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TaskInfoReqDef(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.TaskInfoReqDef(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TaskSubmitReqDef(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int iTaskID = LuaAPI.xlua_tointeger(L, 2);
                    
                    __cl_gen_to_be_invoked.TaskSubmitReqDef( iTaskID );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TaskProgressUpdateReqDef(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int iTaskID = LuaAPI.xlua_tointeger(L, 2);
                    int iAddProgress = LuaAPI.xlua_tointeger(L, 3);
                    
                    __cl_gen_to_be_invoked.TaskProgressUpdateReqDef( iTaskID, iAddProgress );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TaskDegreeGetReqDef(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    sbyte cType = (sbyte)LuaAPI.xlua_tointeger(L, 2);
                    sbyte cIndex = (sbyte)LuaAPI.xlua_tointeger(L, 3);
                    
                    __cl_gen_to_be_invoked.TaskDegreeGetReqDef( cType, cIndex );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoginReqDef(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int userid = LuaAPI.xlua_tointeger(L, 2);
                    
                    __cl_gen_to_be_invoked.LoginReqDef( userid );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StreamToBytes(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.IO.Stream stream = (System.IO.Stream)translator.GetObject(L, 2, typeof(System.IO.Stream));
                    
                        byte[] __cl_gen_ret = __cl_gen_to_be_invoked.StreamToBytes( stream );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_BytesToStream(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    byte[] bytes = LuaAPI.lua_tobytes(L, 2);
                    
                        System.IO.Stream __cl_gen_ret = __cl_gen_to_be_invoked.BytesToStream( bytes );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LobbyLoginReqDef(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int userId = LuaAPI.xlua_tointeger(L, 2);
                    string token = LuaAPI.lua_tostring(L, 3);
                    
                    __cl_gen_to_be_invoked.LobbyLoginReqDef( userId, token );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Create_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        odao.scmahjong.NetworkPlayer __cl_gen_ret = odao.scmahjong.NetworkPlayer.Create(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddEventListener(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Action<NetworkInterface.NetWorkState> callback = translator.GetDelegate<System.Action<NetworkInterface.NetWorkState>>(L, 2);
                    
                        int __cl_gen_ret = __cl_gen_to_be_invoked.AddEventListener( callback );
                        LuaAPI.xlua_pushinteger(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ConnectGameServer(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string host = LuaAPI.lua_tostring(L, 2);
                    int port = LuaAPI.xlua_tointeger(L, 3);
                    System.Action callback = translator.GetDelegate<System.Action>(L, 4);
                    
                    __cl_gen_to_be_invoked.ConnectGameServer( host, port, callback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsSpecialType(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    byte specialType = (byte)LuaAPI.xlua_tointeger(L, 2);
                    byte value = (byte)LuaAPI.xlua_tointeger(L, 3);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.IsSpecialType( specialType, value );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToComboType(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    byte specialType = (byte)LuaAPI.xlua_tointeger(L, 2);
                    
                        odao.scmahjong.TileDef.ComboType __cl_gen_ret = __cl_gen_to_be_invoked.ToComboType( specialType );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Info(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.Info);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Info(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                odao.scmahjong.NetworkPlayer __cl_gen_to_be_invoked = (odao.scmahjong.NetworkPlayer)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.Info = (odao.scmahjong.PlayerInfo)translator.GetObject(L, 2, typeof(odao.scmahjong.PlayerInfo));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
