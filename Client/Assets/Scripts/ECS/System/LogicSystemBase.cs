using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LogicSystemBase : ILogicSystem
{

	private HashSet<IEntity> _entities = new HashSet<IEntity>();
	
	public void AddEntity(IEntity entity)
	{
		if (JudgeListener(entity))
		{
			_entities.Add(entity);
		}
		else
		{
			if (_entities.Contains(entity))
			{
				_entities.Remove(entity);
			}
		}
	}

	protected abstract Type[] Listener();

	protected abstract bool ExecuteCondition(IEntity entity);
	protected abstract void Execute(IEntity entity);

	public void Execute()
	{
		foreach (IEntity entity in _entities)
		{
			if (ExecuteCondition(entity))
			{
				Execute(entity);
			}
		}
	}

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
