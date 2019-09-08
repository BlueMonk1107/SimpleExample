using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMgr : IEntityMgr
{
    private Dictionary<int, IEntity> _entities = new Dictionary<int, IEntity>();
    private Action<IEntity> _onChangeComponent;
    
    public void Init()
    {
    }

    public void Update()
    {
    }

    public IEntity CreateEntity()
    {
        IEntity entity = new Entity(_onChangeComponent);
        _entities.Add(entity.ID,entity);
        return entity;
    }

    public void ChangeComponentListener(Action<IEntity> changeComponent)
    {
        _onChangeComponent += changeComponent;
    }
}
