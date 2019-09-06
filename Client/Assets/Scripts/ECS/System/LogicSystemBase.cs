using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LogicSystemBase : ILogicSystem
{

	private HashSet<IEntity> _entities;
	
	public void AddEntity(IEntity entity)
	{
		if (JudgeListener(entity))
		{
			_entities.Add(entity);
		}
	}

	protected abstract Type[] Listener();

	public abstract bool ExecuteCondition();

	public abstract void Execute();

	private bool JudgeListener(IEntity entity)
	{
		foreach (Type type in Listener())
		{
			if (!entity.HasComponent(type))
				return false;
		}

		return true;
	}
}
