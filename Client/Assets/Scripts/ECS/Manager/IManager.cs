using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IManager  {
   void Init();
   void Update();
}

public interface IEntityMgr : IManager
 {
    IEntity CreateEntity();
    void AddCreateEntityListener(Action<IEntity> onAddEntity);
 }

public interface INormalSystemMgr : IManager
{
    void AddInitSystem(IInitSystem system);
    void AddUpdateSystem(IUpdateSystem system);
}

public interface ILogicSystemMgr : IManager
{
    void CreateEntity(IEntity entity);
    void AddLogicSystem(ILogicSystem system);
}
