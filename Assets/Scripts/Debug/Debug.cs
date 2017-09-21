using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bean.Hall
{

    public class Debug 
    {
        public static void LogMsg(object msg)
        {
            if(true)
                UnityEngine.Debug.Log(msg);
        }

    }

}