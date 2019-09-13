using System.Collections;
using System.Collections.Generic;
using SocketIO;
using UnityEngine;
using UnityEngine.AI;

public class Move : ViewBase
{

	private NavMeshAgent _agent;

	// Use this for initialization
	void Start ()
	{
		_agent = GetComponent<NavMeshAgent>();
	}


	protected override void AddEventListener()
	{
		NetworkMgr.Instance.AddListener(Keys.Move,OnMove);
	}

	protected override void RemoveEventListener()
	{
		NetworkMgr.Instance.RemoveListener(Keys.Move,OnMove);
	}

	private void OnMove(SocketIOEvent data)
	{
		string id = Util.GetId(data);
		var ownData = GetComponent<Data>();
		if (id == ownData.ID)
		{
			transform.position = Util.JsonToVector(data.data["startPos"]);
			_agent.SetDestination(Util.JsonToVector(data.data["targetPos"]));
		}
	}
}
