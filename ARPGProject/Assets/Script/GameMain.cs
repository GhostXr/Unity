using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class GameMain : MonoBehaviour {

    public static LuaEnv _luaEnv = new LuaEnv();

    object[] mainLua;

    void Start () {
        _luaEnv.DoString("require 'main'");
        int i = _luaEnv.Global.Get<int>("index");
        string x = _luaEnv.Global.Get<string>("string");
        float y = _luaEnv.Global.Get<float>("float");

        float y = _luaEnv.Global.Get<float>("float");
    }

void Destory () {
        _luaEnv.Dispose();
    }
}
