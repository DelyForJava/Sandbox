
local SocketManager = {
    functable = { },
    listenerFuncTable = { },
    fConnectedcb = nil,
    fErrorcb = nil,
    fSocketStatecb = nil,
    iScheduler = - 1,
    iDt = 0,
    sCurIp = "",
    iCurPort = 0,
    iRepeatTime = 0,
    iKeepAlive = 10,
    dnsScheduler = - 1,-- dns的定时器
    iConnectType,-- 1是连接DNS的IP的地址  2是连接自己域名的IP地址
    isLobbySocket,-- 是不是大厅的socket
    listenerDomainAnalysisError = nil,
    connectScheduler = -1, --网络链接定时器
}


function SocketManager:init(msgCfgPath, iType)

    if self.listenerDomainAnalysisError == nil then 
        --域名解析失败
        self.listenerDomainAnalysisError = cc.EventListenerCustom:create("DOMAIN_ANALYSIS_ERROR", handler(self, self.onListenerDomainAnalysisError))
        cc.Director:getInstance():getEventDispatcher():addEventListenerWithFixedPriority(self.listenerDomainAnalysisError, 1)
    end
    -- iType = 0就是文件 1就是json
    local localType =((type(iType) ~= "number") and { 0 } or { iType })[1]

    if localType ~= 0 then
        local tableBase = { }
        table.insert(tableBase, msgCfgPath)
        table.insert(tableBase, Od.Res.Config_ActivityNetMessage)
         dump(tableJson, "tableJson : ")
        local tableJson = SocketManager:initPath(tableBase)

       

        od.NetMessageManager:getInstance():loadCfg(json.encode(tableJson), localType)
    else
        printInfo("msgCfgPath : %s", msgCfgPath)
        od.NetMessageManager:getInstance():loadCfg(msgCfgPath, localType)
    end

    od.OdNetworkImpl:getInstance():setStateFunc(handler(self, self.onConnected), handler(self, self.onDisconnected), handler(self, self.onError))
    od.OdNetworkImpl:getInstance():setReceivedDataFunc(handler(self, self.onReceivedData))

    if self.iScheduler < 0 then
        local scheduler = cc.Director:getInstance():getScheduler()
        self.iScheduler = scheduler:scheduleScriptFunc(handler(self, self.onSocketTime), 1, false)
    end

end

function SocketManager:onListenerDomainAnalysisError()
   printInfo("onListenerDomainAnalysisError........")
    if self.fErrorcb ~= nil then
        local iSocketError = -1
        self.fErrorcb(iSocketError)
    end
end    

function SocketManager:initPath(tablePath)
    local tableJson = { }
    local len = #tablePath

    if len >= 1 then
        local filestr = cc.FileUtils:getInstance():getStringFromFile(tablePath[1])
        local jsonObj = json.decode(filestr)
        tableJson = jsonObj
    else
        print("tablePath len : " .. len .. "error!!!")
    end


    for i = 2, table.maxn(tablePath) do
        local filestr = cc.FileUtils:getInstance():getStringFromFile(tablePath[i])
        local jsonObj = json.decode(filestr)
        self:table_add(tableJson, jsonObj)
    end


    return tableJson
end

-- 合并两个table2
function SocketManager:table_add(table1, table2)
    if type(table1) ~= "table" or type(table2) ~= "table" then
        return
    end

    -- table1没有的key
    for k2, v2 in pairs(table2) do
        if table1[k2] == nil then
            table1[k2] = v2
        end
    end

    -- table2有的需要插入到table1中
    for k1, v1 in pairs(table1) do
        if table2[k1] ~= nil then
            self:table_add(v1, table2[k1])
        end
    end

    dump(table1, "table1")
end

function SocketManager:setConnected(cb)
    printInfo("SocketManager:setConnected.........")
    self.fConnectedcb = cb
end

function SocketManager:setError(cb)
    self.fErrorcb = cb
end    

function SocketManager:setSocketState(cb)
    self.fSocketStatecb = cb
end  

function SocketManager:setKeepAliveTime(iTime)
    self.iKeepAlive = iTime
end  


function SocketManager:connect(szAddr, wPort, connectType, isLobbySocket)

    -- connectType 1是通过httpdns连接 2是通过域名 3是直接用ipv4连接  isLobbySocket是不是大厅的socket
    self.sCurIp = szAddr
    self.iCurPort = wPort
    self.isLobbySocket =((type(isLobbySocket) ~= "number") and { 0 } or { isLobbySocket })[1]

    -- self.iConnectType = 2
    -- od.OdNetworkImpl:getInstance():connect(szAddr, wPort)
    if LoadPreambleManager:getInstance():getIsVerifyVersionLobby() or (LoadPreambleManager:getInstance().getSwitchDNS ~= nil and LoadPreambleManager:getInstance():getSwitchDNS() == 0) then
        self.iConnectType = 2
    else
        self.iConnectType = ((type(connectType) ~= "number") and { 1 } or { connectType })[1]
    end
   
    printInfo("SocketManager:connect szAddr[%s], wPort[%d], iConnectType[%d], isLobbySocket[%d]", szAddr, wPort, self.iConnectType, self.isLobbySocket)

    if self.iConnectType == 1 then
        -- 做个定时器防止返回过慢
        if self.dnsScheduler < 0 then
            local scheduler = cc.Director:getInstance():getScheduler()
            self.dnsScheduler = scheduler:scheduleScriptFunc(handler(self, self.onHttpDnsTime), 2, false)
        end
        -- http dns
        local HttpDNSManager = require "app.models.module.HttpDNSManager"
        HttpDNSManager:getInstance():HttpDnsEncrypt(szAddr, handler(self, self.onGetHttpDNSBack))
    elseif self.iConnectType == 2 then
        od.OdNetworkImpl:getInstance():connect(szAddr, wPort)
    elseif self.iConnectType == 3 then
        od.OdNetworkImpl:getInstance():connect(szAddr, wPort, 1)
    end
end


function SocketManager:onHttpDnsTime(dt)
    print("onCkTime___GetDNS_ER")
    MobClickForLua.event("GetDNS_ER") --记录dns解析
    if self.dnsScheduler ~= nil and self.dnsScheduler > 0 then
        local HttpDNSManager = require "app.models.module.HttpDNSManager"
        HttpDNSManager:getInstance():removeCallBack("onConnectHttpDNS")
        local scheduler = cc.Director:getInstance():getScheduler()
        scheduler:unscheduleScriptEntry(self.dnsScheduler)
        self.dnsScheduler = -1
    end
    self.iConnectType = 2
    od.OdNetworkImpl:getInstance():connect(self.sCurIp, self.iCurPort)
end


function SocketManager:onGetHttpDNSBack(status, data)

    if self.dnsScheduler ~= nil and self.dnsScheduler > 0 then
        local scheduler = cc.Director:getInstance():getScheduler()
        scheduler:unscheduleScriptEntry(self.dnsScheduler)
        self.dnsScheduler = -1
    end

    if status == 200 and self.iConnectType == 1 and data ~= nil and data ~= "" and data ~= " " then
        -- 连接 httpdns ipv4
       -- printInfo("status %d, iConnectType %d, dnsip %s", status, self.iConnectType, data)
        if self.isLobbySocket == 1 then
            LoginManager:getInstance().sLobbyIP = data
        end
        printInfo("dataIP: %s",data)
        od.OdNetworkImpl:getInstance():connect(data, self.iCurPort, 1)
        
        -- 做个定时器防止链接过慢(第三方dns被攻击 返回错误ip 情况下)
        if  self.connectScheduler < 0 then 
            local scheduler = cc.Director:getInstance():getScheduler()
            self.connectScheduler = scheduler:scheduleScriptFunc(handler(self, self.onConnectTime), 3, false)
        end

    else
       -- printInfo("status %d, iConnectType %d curIp %s", status, self.iConnectType, self.sCurIp)
       od.OdNetworkImpl:getInstance():connect(self.sCurIp, self.iCurPort)
    end
end

--如果连接不上httpdns的ip再连接自己的
function SocketManager:onConnectTime(dt)
    
    if self.connectScheduler ~= nil and self.connectScheduler > 0 then
        local scheduler = cc.Director:getInstance():getScheduler()
        scheduler:unscheduleScriptEntry(self.dnsScheduler)
        self.connectScheduler = -1
    end
     if self.iConnectType == 1 then
         print("连接不上httpdns的ip再连接自己的...")
         od.OdNetworkImpl:getInstance():close()
         LoginManager:getInstance().sLobbyIP = nil
         od.OdNetworkImpl:getInstance():connect(self.sCurIp, self.iCurPort) 
         self.iConnectType = 2
     end
end

function SocketManager:send2Peer(buf, len)
    printInfo("send2Peer.........")
    od.OdNetworkImpl:getInstance():send2Peer(buf, len)
end  

function SocketManager:sendWithCallBack(gamedata, keystr, cb)
    local data = {
        protocol_key = keystr,
        game_data = gamedata
    }

    dump(data, "sendWithCallBack:")
    if self:haveKey(keystr, 1) == false then
        local cbtable = { funckey = keystr, funccb = cb }
        table.insert(self.functable, cbtable)
        local seedstr = json.encode(data)

        local len = string.len(seedstr)
        self:send2Peer(seedstr, len)
    end
end


function SocketManager:sendWithOutCallBack(gamedata)
    local data = {
        game_data = gamedata
    }
    
    local seedstr = json.encode(data)
    local len = string.len(seedstr)
    self:send2Peer(seedstr, len)
end

function SocketManager:setMessageType(msgKey, msgType)
    od.NetMessageManager:getInstance():setMessageType(msgKey, msgType)
end    

function SocketManager:addListenerSocket(keystr, cb)
    if self:haveKey(keystr, 2) == false then
        local cbtable = { funckey = keystr, funccb = cb }
        table.insert(self.listenerFuncTable, cbtable)
    end
end    


function SocketManager:destroyInstance()
    od.OdNetworkImpl:destroyInstance()
end 

function SocketManager:resetData()
    self.functable = { }
    self.listenerFuncTable = { }
    self.fConnectedcb = nil
    self.fErrorcb = nil
    self.fSocketStatecb = nil
    self.iScheduler = -1
    self.iDt = 0
    self.sCurIp = ""
    self.iCurPort = 0
    self.iRepeatTime = 0
    self.iKeepAlive = 10
    self.iConnectType = 2
    self.isLobbySocket = 0
    if self.dnsScheduler ~= nil and self.dnsScheduler > 0 then
        local HttpDNSManager = require "app.models.module.HttpDNSManager"
        HttpDNSManager:getInstance():removeCallBack("onConnectHttpDNS")
        local scheduler = cc.Director:getInstance():getScheduler()
        scheduler:unscheduleScriptEntry(self.dnsScheduler)
        self.dnsScheduler = -1
    end

    if self.connectScheduler ~= nil and self.connectScheduler > 0 then 
        local scheduler = cc.Director:getInstance():getScheduler()
        scheduler:unscheduleScriptEntry(self.connectScheduler)
        self.connectScheduler = -1
    end     
end    

function SocketManager:close()
    if self.iScheduler ~= nil and self.iScheduler > 0 then
        local scheduler = cc.Director:getInstance():getScheduler()
        scheduler:unscheduleScriptEntry(self.iScheduler)
    end 
    od.OdNetworkImpl:getInstance():close()

    self:resetData()
end 

function SocketManager:onReceivedData(datastr)

    printInfo("onReceivedData.........%d", string.len(datastr))

    local data = json.decode(datastr)

    dump(data, "onReceivedData:")
    local msgHead = data.HEADER
    local protocol_key = string.format("0x%02X",(msgHead.cMsgType))
    printInfo("protocol_key %s", protocol_key)

    -- local  errorCode = data.cErrorCode
    -- printInfo("protocol_key.........%s",protocol_key)
    -- if errorCode ~= 0 then
    --    local errorTipsKey = string.format("error_server_%d",errorCode)
    --    FlyTipsManager:getInstance():showTipsByKey(errorTipsKey)
    -- end

    local func = self:findFunc(protocol_key, 2)
    if func ~= nil then
        func(data)
        self:removeFunc(protocol_key)
    else

        -- WaitingManager:getInstance():hideWaiting()

        func = self:findFunc(protocol_key, 1)
        if func ~= nil then

            func(data)
            self:removeFunc(protocol_key)
        end
    end
end    

function SocketManager:onConnected()
    printInfo("onConnected iConnectType : " .. self.iConnectType)

    if self.dnsScheduler ~= nil and self.dnsScheduler > 0 then
        local HttpDNSManager = require "app.models.module.HttpDNSManager"
        HttpDNSManager:getInstance():removeCallBack("onConnectHttpDNS")
        local scheduler = cc.Director:getInstance():getScheduler()
        scheduler:unscheduleScriptEntry(self.dnsScheduler)
        self.dnsScheduler = -1
    end

    self.iConnectType = 2

    if self.fConnectedcb ~= nil then
        self.fConnectedcb()
    end

end


function SocketManager:onDisconnected()
    printInfo("onDisconnected.........")
    self.iConnectType = 2
end

function SocketManager:onError()
    printInfo("onError.........")

    if self.fErrorcb ~= nil then
        local iSocketError = self:getSocketError()
        self.fErrorcb(iSocketError)
    end

end

-- enum TcpSocketState
-- {
--     kState_UnconnectedState = 0,
--     kState_ConnectingState,
--     kState_ConnectedState,
--     kState_ClosingState
-- };

function SocketManager:getSocketState()
    local state = -1
    state = od.OdNetworkImpl:getInstance():getSocketState()
    return state
end    

-- enum GTcpSocketError
-- {
--     kGTcpSocketError_NoError = 0,
--     kGTcpSocketError_StateIsNot_UnconnectedState,
--     kGTcpSocketError_StateIsNot_ConnectingState,
--     kGTcpSocketError_StateIsNot_ConnectedState,
--     kGTcpSocketError_StateIsNot_ClosingState,

--     kGTcpSocketError_CreateSocketFailed,
--     kGTcpSocketError_GetHostByNameFailed,
--     kGTcpSocketError_CreateConnectThreadFailed,
--     kGTcpSocketError_SetConnectThreadAttrFailed,
--     kGTcpSocketError_ConnectFailed,

--     kGTcpSocketError_CheckSocketSendError,
--     kGTcpSocketError_CheckSocketReceiveError,
--     kGTcpSocketError_ShutDownSocketError,
--     kGTcpSocketError_CloseSocketError,
-- };

function SocketManager:getSocketError()
    local socketError = -1
     socketError = od.OdNetworkImpl:getInstance():getSocketError()
     printInfo("getSocketError :%d",socketError)
    return socketError
end    

function SocketManager:haveKey(protocol_key, iType)
    local have = false
    local functable = self.functable
    if iType == 2 then
        functable = self.listenerFuncTable
    end

    for key, value in pairs(functable) do
        if protocol_key == value.funckey then
            have = true
            break
        end
    end
    return have
end   

--- 1是主动请求   2是推送
function SocketManager:findFunc(protocol_key, iType)

    local func = nil

    local functable = self.functable
    if iType == 2 then
        functable = self.listenerFuncTable
    end

    for key, value in pairs(functable) do
        printInfo("findFunc.........%s", value.funckey)
        if protocol_key == value.funckey then
            func = value.funccb
            break
        end
    end
    return func
end

function SocketManager:removeCallBack(protocol_key)

    local func = self:findFunc(protocol_key, 2)
    if func ~= nil then
        self:removeFunc(protocol_key, 2)
    else
        func = self:findFunc(protocol_key, 1)
        if func ~= nil then
            self:removeFunc(protocol_key, 1)
        end
    end

end    

function SocketManager:removeFunc(protocol_key, iType)

    local functable = self.functable
    if iType == 2 then
        functable = self.listenerFuncTable
    end

    for key, value in pairs(functable) do
        if protocol_key == value.funckey then

            table.remove(functable, key)
            break
        end
    end
end


function SocketManager:onSocketTime(dt)

    local state = self:getSocketState()
  --  printInfo("sockeState :%d", state)
    if state == 2 then
        -- 连上了
        if self.fSocketStatecb ~= nil then
            self.fSocketStatecb(1)
        end
        self.iRepeatTime = 0

        self.iDt = self.iDt + 1
        if self.iDt == self.iKeepAlive then
            self.iDt = 0
            self:sendKeepAlive()
        end
    elseif state == 0 or state == 3 then
        -- 断线重连
        printInfo("sockeState2 :%d  self.iRepeatTime :%d", state, self.iRepeatTime)
        local iSocketError = self:getSocketError() 
        if iSocketError > 0 then 
            -- 如果连接不上httpdns的ip再连接自己的(这里要超时才执行里面，在前面做了个3秒定时器,所以这里基本上不会进去)
            -- 某种情况下会马上执行这里 (传的ip参数 是个数字 之类的)
            if iSocketError == 9 and self.iConnectType == 1 then
               if self.connectScheduler ~= nil and self.dnsScheduler > 0 then 
                    local scheduler = cc.Director:getInstance():getScheduler()
                    scheduler:unscheduleScriptEntry(self.connectScheduler)
                    self.connectScheduler = -1
               end      
               LoginManager:getInstance().sLobbyIP = nil
               od.OdNetworkImpl:getInstance():connect(self.sCurIp, self.iCurPort) 
               self.iConnectType = 2
            else 
                
                if self.fSocketStatecb ~= nil then
                    self.fSocketStatecb(2)
                end
           end
        else 
            if state == 3 and self.fSocketStatecb ~= nil then
                self.fSocketStatecb(2)
            end
        end
    elseif state == 1 then
        -- 连接中
        if self.fSocketStatecb ~= nil then
            self.fSocketStatecb(4)
        end
        printInfo("sockeState4 :%d", state)
    end
end    

function SocketManager:sendKeepAlive()
    local tempdata = NetMessageManager.KEEPALIVED_REQ
    tempdata.HEADER = NetMessageManager.HEADER
    tempdata.HEADER.cMsgType = "0xF0F1"
    self:sendWithOutCallBack(tempdata)
end


return SocketManager
