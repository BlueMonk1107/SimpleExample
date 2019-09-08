using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalSystemMgr : INormalSystemMgr
{
    private HashSet<IInitSystem> _initSystems = new HashSet<IInitSystem>();
    private HashSet<IUpdateSystem> _updateSystems = new HashSet<IUpdateSystem>();

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

    public void AddInitSystem(IInitSystem system)
    {
        _initSystems.Add(system);
    }
    
    public void AddUpdateSystem(IUpdateSystem system)
    {
        _updateSystems.Add(system);
    }
}
