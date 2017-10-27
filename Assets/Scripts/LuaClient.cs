using System;

using UnityEngine;
using UnityEngine.SceneManagement;
using XLua;

namespace Bean.Hall
{
    [Serializable]
    public class Injection
    {
        public string name;
        public GameObject value;
    }

    public class LuaClient : MonoBehaviour
    {
        private string mainPath = "Assets/Lua/Client.lua.txt";
        public TextAsset luaScript;
        public Injection[] injections;

        internal static LuaEnv luaEnv = new LuaEnv(); //all lua behaviour shared one luaenv only!
        internal static float lastGCTime = 0;
        internal const float GCInterval = 1;//1 second 

        private Action luaStart;
        private Action luaUpdate;
        private Action luaOnDestroy;

        private LuaTable scriptTable;

        byte[] Require(ref string filepath)
        {
            filepath = "Assets/Lua/" + filepath;
            filepath = filepath.Replace('.', '/') + ".lua.txt";
            TextAsset file = ResourcesManager.Load<TextAsset>(filepath);
            if (file != null)
            {
                return file.bytes;
            }
            else
            {
                return null;
            }

        }

        void Awake()
        {
            Debug.LogMsg("LuaClient Awake");

            LuaEnv.CustomLoader custom = Require;
            luaEnv.AddLoader(custom);

            scriptTable = luaEnv.NewTable();

            LuaTable meta = luaEnv.NewTable();
            meta.Set("__index", luaEnv.Global);
            scriptTable.SetMetaTable(meta);
            meta.Dispose();

            scriptTable.Set("self", this);
            //foreach (var injection in injections)
            //{
            //    scriptTable.Set(injection.name, injection.value);
            //}

            luaScript = ResourcesManager.Load<TextAsset>(mainPath);
            luaEnv.DoString(luaScript.text, "Client", scriptTable);

            Action luaAwake = scriptTable.Get<Action>("Awake");
            scriptTable.Get("Start", out luaStart);
            scriptTable.Get("Update", out luaUpdate);
            scriptTable.Get("OnDestroy", out luaOnDestroy);

            Callback.RegistLuaAction(scriptTable);

            if (luaAwake != null)
            {
                luaAwake();
            }

        }

        // Use this for initialization
        void Start()
        {
            //UnityEngine.SceneManagement.SceneManager.
            if (luaStart != null)
            {
                luaStart();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (luaUpdate != null)
            {
                luaUpdate();
            }

            if (Time.time - LuaBehaviour.lastGCTime > GCInterval)
            {
                luaEnv.Tick();
                LuaBehaviour.lastGCTime = Time.time;
            }

        }

        void OnDestroy()
        {
            if (luaOnDestroy != null)
            {
                luaOnDestroy();
            }
            luaOnDestroy = null;
            luaUpdate = null;
            luaStart = null;

            scriptTable.Dispose();
            injections = null;
        }

    }

}