using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using SocketIO;
using UnityEngine;

public class StartGame : ViewBase
{
    // Use this for initialization
    void Start()
    {
        RootMgr.Instance.Init();
        GameObject Prefab = Resources.Load<GameObject>("Prefab/ChatView");
        Instantiate(Prefab, transform);
		NetworkMgr.Instance.Emit(Keys.InitGameComplete);
    }

    private void Update()
    {
        RootMgr.Instance.Update();
    }

    private void Spawn(SocketIOEvent obj)
    {
        Debug.Log("Spawn " + obj.data);
        var player = PlayerSpawner.Instance.SpawnPlayer(Util.GetId(obj));
        if (obj.data["x"])
        {
            var movePosition = Util.GetVectorFromJson(obj);
            var navPos = player.GetComponent<Navigator>();
            navPos.NavigateTo(movePosition);
        }
    }

    protected override void AddEventListener()
    {
        NetworkMgr.Instance.AddListener(Keys.Spawn,Spawn);
    }

    protected override void RemoveEventListener()
    {
        NetworkMgr.Instance.RemoveListener(Keys.Spawn,Spawn);
    }
}
