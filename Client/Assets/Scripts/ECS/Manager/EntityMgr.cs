using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMgr : IEntityMgr
{
    private Action<IEntity> _onChangeComponent;
    private Dictionary<int,IEntity> _entities = new Dictionary<int, IEntity>();
    
    public EntityMgr(Action<IEntity> onChangeComponent)
    {
        _onChangeComponent = onChangeComponent;
    }
    
    public IEntity CreateEntity()
    {
        IEntity entity = new Entity(_onChangeComponent);
        _entities.Add(entity.ID,entity);
        return entity;
    }
}