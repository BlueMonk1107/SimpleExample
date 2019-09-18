using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicSystemMgr : ILogicSystemMgr
{

	private HashSet<ILogicSystem> _systems = new HashSet<ILogicSystem>();

	public void UpdateFun()
	{
		foreach (ILogicSystem system in _systems)
		{
			system.Execute();
		}
	}

	public void AddEntity(IEntity entity)
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
