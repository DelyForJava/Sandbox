﻿using UnityEngine;

namespace Bean.Hall
{

    public class DebugConsole : MonoBehaviour
    {
        string Str;
        Vector2 v2;
        bool IsShow;

        void Start()
        {
            IsShow = true;
            Str = "";
            v2 = Vector2.zero;
        }

        void Update()
        {
            //当按下退格键时显示或隐藏控制台
            if (Input.GetKey(KeyCode.Backspace))
                IsShow = !IsShow;
        }

        //当脚本启用时注册控制台信息输出的委托
        void OnEnable()
        {
            Application.logMessageReceived += Application_logMessageReceived;
        }

        //当脚本禁用时取消控制台信息输出的委托
        void OnDisable()
        {
            Application.logMessageReceived -= Application_logMessageReceived;
        }

        private void Application_logMessageReceived(string condition, string stackTrace, LogType type)
        {
            //输入控制台的信息
            Str += condition + "\n" + stackTrace + "\n---------------------------------------------------------------\n";
        }

        void OnGUI()
        {
            //绘制控制台窗口
            if (IsShow)
            {
                v2 = GUILayout.BeginScrollView(v2, GUILayout.Width(1024), GUILayout.Height(640));
                GUILayout.TextArea(Str, GUILayout.Width(960), GUILayout.Height(60));
                //GUILayout.TextArea(Str, GUILayout.Width(600), GUILayout.Height(60));
                GUILayout.EndScrollView();
            }

        }

    }

}