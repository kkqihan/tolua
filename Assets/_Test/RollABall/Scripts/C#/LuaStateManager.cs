using UnityEngine;
using LuaInterface;

public class LuaStateManager
{
    private static LuaStateManager m_Instance;
    public static LuaStateManager Instance
    {
        get
        {
            //进行实例化
            if (m_Instance == null)
            {
                m_Instance = new LuaStateManager();
            }

            return m_Instance;
        }
    }

    /// <summary>
    /// 状态机
    /// </summary> <summary>
    /// 
    /// </summary>
    public LuaState luaState;

    private LuaLooper luaLooper = null;

    public LuaStateManager()
    {
        //创建虚拟机
        luaState = new LuaState();
        luaState.Start();
        //类绑定
        LuaBinder.Bind(luaState);
        //添加LuaLooper组件
        luaLooper = GameObject.Find("Manager").AddComponent<LuaLooper>();
        luaLooper.luaState = luaState;
        //添加Lua文件的搜索路径
        string luaDir = string.Format("{0}/_Test/RollABall/Scripts/Lua", Application.dataPath);
        luaState.AddSearchPath(luaDir);
    }
}
