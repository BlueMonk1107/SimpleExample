using System.Collections;
using System.Collections.Generic;
using SocketIO;
using UnityEngine;
using UnityEngine.AI;

public class PlayerView : ViewBase
{
    private IEntity _entity;

    // Use this for initialization
    public void Init(string id)
    {
        _entity = RootMgr.Instance.CreateEntity();
        PlayerComponent playerCom = _entity.AddComponent<PlayerComponent>();
        playerCom.Agent = GetComponent<NavMeshAgent>();
        playerCom.Player = transform;
        playerCom.ID = id;
        playerCom.Animator = GetComponent<Animator>();
        _entity.AddComponent<MoveComponent>();
    }

    protected override void AddEventListener()
    {
        NetworkMgr.Instance.AddListener(Keys.Move, OnMove);
    }

    protected override void RemoveEventListener()
    {
        NetworkMgr.Instance.RemoveListener(Keys.Move, OnMove);
    }

    private void OnMove(SocketIOEvent data)
    {
        var move = _entity.GetComponent<MoveComponent>();
        move.ID = Util.GetId(data);
        move.StartPos = Util.JsonToVector(data.data["startPos"]);
        move.TargetPos = Util.JsonToVector(data.data["targetPos"]);
    }
}