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

    private bool JudgeListener(IEntity entity)
    {
        Type[] types = Listener();
        for (int i = 0; i < types.Length; i++)
        {
            if (!entity.HasComponent(types[i]))
                return false;
        }

        return true;
    }

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
}