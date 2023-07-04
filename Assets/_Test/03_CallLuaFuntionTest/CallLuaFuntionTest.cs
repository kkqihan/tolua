using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;
using System;

public class CallLuaFuntionTest : MonoBehaviour
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
    /// 求和的Lua方法
    /// </summary>
    private LuaFunction addLuaFunc;

    // Start is called before the first frame update
    void Start()
    {
        //监听日志事件
        Application.logMessageReceived += EventHandle_LogMessage;

        //创建虚拟机
        luaState = new LuaState();
        luaState.Start();
        //添加Lua文件的搜索路径
        string addPath = string.Format("{0}/_Test/03_CallLuaFuntionTest", Application.dataPath);
        luaState.AddSearchPath(addPath);
        //执行lua文件
        luaState.Require("tools");

        //测试函数调用 LuaState.Invoke
        #region LuaState.Invoke调用
        int numA = 123;
        int numB = 456;
        int total = luaState.Invoke<int, int, int>("tools.add", numA, numB, true);
        Debug.LogFormat("luaState.Invoke. result={0}", total);
        #endregion

        //测试函数调用 LuaFunction.Invoke
        #region LuaFunction.Invoke
        addLuaFunc = luaState.GetFunction("tools.add");
        total = addLuaFunc.Invoke<int, int, int>(numA, numB);
        Debug.LogFormat("LuaFunction.Invoke. result={0}", total);
        #endregion

        //测试函数调用 LuaFunction.ToDelegate
        #region LuaFunction.Invoke
        //初始化委托工厂
        DelegateFactory.Init();
        //LuaFunction转化为委托并调用
        Func<int, int, int> addDel = addLuaFunc.ToDelegate<Func<int, int, int>>();
        total = addDel(numA, numB);
        Debug.LogFormat("LuaFunction.ToDelegate. result={0}", total);
        #endregion

        #region luaFunc.PCall
        //函数入栈
        addLuaFunc.BeginPCall();
        //压入参数
        addLuaFunc.Push(numA);
        addLuaFunc.Push(numB);
        //函数调用
        addLuaFunc.PCall();
        //返回值出栈
        numA = (int)addLuaFunc.CheckNumber();
        //函数出栈
        addLuaFunc.EndPCall();
        Debug.LogFormat("luaFunc.PCall. result={0}", total);
        #endregion
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
        addLuaFunc.Dispose();
        addLuaFunc = null;

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
