using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicSystemMgr : ILogicSystemMgr
{

    private HashSet<ILogicSystem> _systems = new HashSet<ILogicSystem>();
    
    public void Init()
    {
    }

    public void Update()
    {
        foreach (ILogicSystem system in _systems)
        {
            if (system.ExecuteCondition())
            {
                system.Execute();
            }
        }
    }

    public void CreateEntity(IEntity entity)
    {
        foreach (ILogicSystem system in _systems)
        {
            system.AddEntity(entity);
        }
    }
    
    public void AddLogicSystem(ILogicSystem system)
    {
        _systems.Add(system);
    }
}
