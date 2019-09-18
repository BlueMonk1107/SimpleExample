using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RootManagerBase : IManager
{

	private INormalSystemMgr _normalSystemMgr;
	private ILogicSystemMgr _logicSystemMgr;
	private IEntityMgr _entityMgr;
	
	// Use this for initialization
	public void Init () {
		_normalSystemMgr = new NormalSystemMgr();
		_normalSystemMgr.Init();
		_logicSystemMgr = new LogicSystemMgr();
		_entityMgr = new EntityMgr(_logicSystemMgr.AddEntity);
		InitSystems();
	}

	protected abstract void InitSystems();
	
	// Update is called once per frame
	public void Update () {
		_normalSystemMgr.Update();
		_logicSystemMgr.UpdateFun();
	}

	public IEntity CreateEntity()
	{
		return _entityMgr.CreateEntity();
	}

	protected void AddInitSystem(IInitSystem system)
	{
		_normalSystemMgr.AddInitSystem(system);
	}
	
	protected void AddLogicSystem(ILogicSystem system)
	{
		_logicSystemMgr.AddLogicSystem(system);
	}

	protected void AddUpdateSystem(IUpdateSystem system)
	{
		_normalSystemMgr.AddUpdateSystem(system);
	}
}
