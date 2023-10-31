using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;

public class LuaBehaviour : MonoBehaviour
{
    private LuaFunction luaFunc_Awake;
    private LuaFunction luaFunc_Start;
    private LuaFunction luaFunc_FixedUpdate;
    private LuaFunction luaFunc_Update;
    private LuaFunction luaFunc_LateUpdate;

    void Awake()
    {
        //加载Lua脚本
        LuaState luaState = LuaStateManager.Instance.luaState;
        string luaFileName = string.Format("{0}", this.gameObject.name);
        luaState.Require(luaFileName);

        //绑定Lua方法
        luaFunc_Awake = GetFunction("Awake");
        luaFunc_Start = GetFunction("Start");
        luaFunc_FixedUpdate = GetFunction("FixedUpdate");
        luaFunc_Update = GetFunction("LateUpdate");

        //执行方法
        luaFunc_Awake.Call(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        luaFunc_Start?.Call();
    }

    void FixedUpdate()
    {
        luaFunc_FixedUpdate?.Call();
    }

    // Update is called once per frame
    void Update()
    {
        luaFunc_Update?.Call();
    }

    void LateUpdate()
    {
        luaFunc_Update?.Call();
    }

    private LuaFunction GetFunction(string functionName)
    {
        return LuaStateManager.Instance.luaState.GetFunction(string.Format("{0}.{1}", this.gameObject.name, functionName));
    }
}
