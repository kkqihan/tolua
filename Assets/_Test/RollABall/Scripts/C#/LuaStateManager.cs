using UnityEngine;
using LuaInterface;

public class LuaStateManager
{
    private static LuaStateManager m_Instance;
    public static LuaStateManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new LuaStateManager();
            }

            return m_Instance;
        }
    }

    public LuaState luaState;

    public LuaStateManager()
    {
        //创建虚拟机
        luaState = new LuaState();
        luaState.Start();
        //类绑定
        LuaBinder.Bind(luaState);
        //添加Lua文件的搜索路径
        string luaDir = string.Format("{0}/_Test/RollABall/Scripts/Lua", Application.dataPath);
        luaState.AddSearchPath(luaDir);
    }
}
