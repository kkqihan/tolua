using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;

public class LuaBehaviour : MonoBehaviour
{
    private LuaFunction luaFunc_Awake;
    private LuaFunction luaFunc_FixedUpdate;
    private LuaFunction luaFunc_Update;

    void Awake()
    {
        //加载Lua脚本
        LuaState luaState = LuaStateManager.Instance.luaState;
        string luaFileName = string.Format("{0}", this.gameObject.name);
        luaState.Require(luaFileName);

        //绑定Lua方法
        luaFunc_Awake = GetFunction("Awake");
        luaFunc_FixedUpdate = GetFunction("FixedUpdate");

        //执行方法
        luaFunc_Awake.Call();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        luaFunc_FixedUpdate?.Call();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private LuaFunction GetFunction(string functionName)
    {
        return LuaStateManager.Instance.luaState.GetFunction(string.Format("{0}.{1}", this.gameObject.name, functionName));
    }
}
