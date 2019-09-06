﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RootManagerBase : IManager
{
    private INormalSystemMgr _normalSystemMgr;
    private ILogicSystemMgr _logicSystemMgr;
    private IEntityMgr _entityMgr;
    
    public void Init()
    {
        _normalSystemMgr = new NormalSystemMgr();
        _normalSystemMgr.Init();
        _logicSystemMgr = new LogicSystemMgr();
        _logicSystemMgr.Init();
        _entityMgr =new EntityMgr();
        _entityMgr.Init();
        
        _entityMgr.AddCreateEntityListener(_logicSystemMgr.CreateEntity);
        InitSystems();
    }

    public void Update()
    {
        _normalSystemMgr.Update();
        _logicSystemMgr.Update();
        _entityMgr.Update();
    }

    public IEntity CreateEntity()
    {
        return _entityMgr.CreateEntity();
    }

    protected abstract void InitSystems();
    
    protected virtual void AddInitSystem(IInitSystem system)
    {
        _normalSystemMgr.AddInitSystem(system);
    }

    protected virtual void AddLogicSystem(ILogicSystem system)
    {
        _logicSystemMgr.AddLogicSystem(system);
    }

    protected virtual void AddUpdateSystem(IUpdateSystem system)
    {
        _normalSystemMgr.AddUpdateSystem(system);
    }
}
