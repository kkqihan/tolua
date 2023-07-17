using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;
using System;

public class EnumTest : MonoBehaviour
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
    /// 灯光
    /// </summary>
    [SerializeField]
    private Light light;

    /// <summary>
    /// 当前灯光
    /// </summary>
    private int currLightIdx;


    /// <summary>
    /// 灯光强度
    /// </summary>
    private float lightIntensity;

    // Start is called before the first frame update
    void Start()
    {
        //监听日志事件
        Application.logMessageReceived += EventHandle_LogMessage;

        //创建虚拟机
        luaState = new LuaState();
        luaState.Start();
        //类绑定
        LuaBinder.Bind(luaState);
        //添加Lua文件的搜索路径
        string addPath = string.Format("{0}/_Test/10_EnumTest", Application.dataPath);
        luaState.AddSearchPath(addPath);
        //执行lua文件
        luaState.Require("enumtest");

        //定义Lua虚拟机中的space变量
        luaState["space"] = Space.World;
        //执行TestEnum方法
        LuaFunction func = luaState.GetFunction("TestEnum");
        func.BeginPCall();
        func.Push(Space.World);
        func.PCall();
        func.EndPCall();
        func.Dispose();
        func = null;
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


        if (GUILayout.Button("ChangeType"))
        {

            LuaFunction func = luaState.GetFunction("ChangeLightType");
            func.BeginPCall();
            func.Push(light);
            LightType type = (LightType)(currLightIdx++ % 5);
            func.Push(type);
            func.PCall();
            func.EndPCall();
            func.Dispose();
        }

        lightIntensity = GUILayout.HorizontalSlider(lightIntensity, 0, 10);
        luaState.Call<Light, float>("SetLightIntensity", light, lightIntensity, true);

    }

    void OnApplicationQuit()
    {

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
