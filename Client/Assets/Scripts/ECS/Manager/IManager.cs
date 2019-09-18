using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IManager
{
}

public interface IEntityMgr : IManager
{
    IEntity CreateEntity();
    void ChangeComponentListener(Action<IEntity> onAddEntity);
}

public interface INormalSystemMgr : IManager
{
    void Init();
    void Update();
    void AddInitSystem(IInitSystem system);
    void AddUpdateSystem(IUpdateSystem system);
}

public interface ILogicSystemMgr : IManager
{
    void Update();
    void AddEntity(IEntity entity);
    void AddLogicSystem(ILogicSystem system);
}