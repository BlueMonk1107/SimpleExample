using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SystemMgrBase : IManager
{
    private HashSet<IInitSystem> _initSystems;
    private HashSet<IUpdateSystem> _updateSystems;

    public void Init()
    {
        foreach (var sys in _initSystems)
        {
            sys.Init();
        }
    }

    public void Update()
    {
        foreach (var sys in _updateSystems)
        {
            sys.Update();
        }
    }

    protected virtual void AddInitSystem(IInitSystem system)
    {
        _initSystems.Add(system);
    }

    protected virtual void AddLogicSystem(ILogicSystem system)
    {
        throw new System.NotImplementedException();
    }

    protected virtual void AddUpdateSystem(IUpdateSystem system)
    {
        _updateSystems.Add(system);
    }


}
