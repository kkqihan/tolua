using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;
using System;

public class LuaCoroutineTest : MonoBehaviour
{
    /// <summary>
    /// 当前屏幕显示的日志字符串
    /// </summary>
    private string logStr = string.Empty;

    /// <summary>
    /// Lua虚拟机
    /// </summary>
    private LuaState luaState;


    /// <summary>
    /// Lua循环气
    /// </summary>
    private LuaLooper luaLooper = null;


    // Start is called before the first frame update
    void Start()
    {
        //监听日志事件
        Application.logMessageReceived += EventHandle_LogMessage;

        //创建虚拟机
        luaState = new LuaState();
        luaState.Start();
        //添加Lua文件的搜索路径
        string addPath = string.Format("{0}/_Test/05_LuaCoroutineTest", Application.dataPath);
        luaState.AddSearchPath(addPath);
        //添加LuaLooper的组件
        luaLooper = gameObject.AddComponent<LuaLooper>();
        luaLooper.luaState = luaState;
        //执行lua文件
        luaState.Require("coroutinetest");

    }

    // Update is called once per frame
    void Update()
    {
        luaState.CheckTop();
    }

    void OnGUI()
    {
        //显示日志
        GUILayout.Label(logStr);
    }

    void OnApplicationQuit()
    {
        //Lua求和方法卸载
        luaLooper.Destroy();
        luaLooper = null;

        //Lua虚拟机卸载
        luaState.Dispose();
        luaState = null;

        //卸载事件
        Application.logMessageReceived -= EventHandle_LogMessage;
    }


    /// <summary>
    /// 事件处理 打印日志
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="stackTrace"></param>
    /// <param name="type"></param>
    private void EventHandle_LogMessage(string condition, string stackTrace, LogType type)
    {
        logStr = string.Format("{0}{1}\n", logStr, condition);
    }
}
