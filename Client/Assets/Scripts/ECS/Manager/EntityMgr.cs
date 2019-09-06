using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMgr : IEntityMgr
{
    private Dictionary<int, IEntity> _entities = new Dictionary<int, IEntity>();
    private Action<IEntity> _onAddEntity;
    
    public void Init()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }

    public IEntity CreateEntity()
    {
        IEntity entity = new Entity();
        _entities.Add(entity.ID,entity);
        if (_onAddEntity != null)
            _onAddEntity(entity);
        return entity;
    }

    public void AddCreateEntityListener(Action<IEntity> onAddEntity)
    {
        _onAddEntity += onAddEntity;
    }
}
