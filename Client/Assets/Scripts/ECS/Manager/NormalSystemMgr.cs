using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalSystemMgr : INormalSystemMgr {
	
	private HashSet<IInitSystem> _initSystems = new HashSet<IInitSystem>();
	private HashSet<IUpdateSystem> _updateSystems = new HashSet<IUpdateSystem>();

	// Update is called once per frame
	public void Init()
	{
		foreach (IInitSystem initSystem in _initSystems)
		{
			initSystem.Init();
		}
	}

	public void Update()
	{
		foreach (IUpdateSystem system in _updateSystems)
		{
			system.Update();
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
