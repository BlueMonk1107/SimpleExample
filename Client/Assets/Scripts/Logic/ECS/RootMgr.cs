using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMgr : RootManagerBase
{

    private static RootMgr _instance;

    public static RootMgr Instance
    {
        get
        {
            if(_instance == null)
                _instance = new RootMgr();

            return _instance;
        }
    }
    
    protected override void InitSystems()
    {
        AddLogicSystem(new IdleSystem());
        AddLogicSystem(new MoveSystem());
        AddLogicSystem(new MoveAniSystem());
    }
}
