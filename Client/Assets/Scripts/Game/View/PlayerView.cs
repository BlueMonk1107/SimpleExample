using System;
using System.Collections;
using System.Collections.Generic;
using SocketIO;
using UnityEngine;

public class PlayerView : ViewBase
{
    private IEntity _entity;

    public void Init(string id)
    {
        _entity = RootMgr.Instance.CreateEntity();
        PlayerComponent Player = _entity.AddComponent<PlayerComponent>();
        Player.ID = id;
        Player.Player = transform;
        Player.Animator = GetComponent<Animator>();
        Player.Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _entity.AddComponent<MoveComponent>();
        _entity.AddComponent<AniComponent>();
    }

    protected override void AddEventListener()
    {
        NetworkMgr.Instance.AddListener(Keys.Move, OnMove);
    }

    protected override void RemoveEventListener()
    {
        NetworkMgr.Instance.RemoveListener(Keys.Move, OnMove);
    }

    private void OnMove(SocketIOEvent obj)
    {
        var position = Util.GetVectorFromJson(obj);
        var move = _entity.GetComponent<MoveComponent>();
        move.ID = Util.GetId(obj);
        move.Target = position;
        move.ValueChanged = true;
    }
}