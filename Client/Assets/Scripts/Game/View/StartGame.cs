using System;
using System.Collections;
using System.Collections.Generic;
using SocketIO;
using UnityEngine;

public class StartGame : ViewBase
{
    // Use this for initialization
    void Start()
    {
        GameObject Prefab = Resources.Load<GameObject>("Prefab/ChatView");
        Instantiate(Prefab, transform);
		NetworkMgr.Instance.Emit(Keys.InitGameComplete);
        RootMgr.Instance.Init();
    }

    private void Update()
    {
        RootMgr.Instance.Update();
    }

    private void Spawn(SocketIOEvent obj)
    {
        Debug.Log("Spawn " + obj.data);
        var player = PlayerSpawner.Instance.SpawnPlayer(obj.data["id"].ToString());
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
