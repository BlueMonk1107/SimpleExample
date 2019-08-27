using System.Collections;
using System.Collections.Generic;
using SocketIO;
using UnityEngine;

public class UpdatePosition : ViewBase {
        private Navigator _navigator;
    private string _selfId;
 	// Use this for initialization
    void Start () {
           _selfId = GetComponent<NetworkEntity>().id;
         _navigator = transform.GetComponent<Navigator>();
	}

    private void Update()
    {
        // if(_navigator.GetRemainingDistance()>0.01f)
        // {
        //     OnRequestPosition();
        // }
    }
    protected override void AddEventListener()
    {
        NetworkMgr.Instance.AddListener(Keys.UpdatePosition,OnUpdatePosition);
    }

    protected override void RemoveEventListener()
    {
        NetworkMgr.Instance.RemoveListener(Keys.UpdatePosition,OnUpdatePosition);
    }

    private void OnUpdatePosition(SocketIOEvent obj)
    {
        Debug.Log("OnUpdatePosition  "+obj.data["id"]);
        if(_selfId == obj.data["id"].ToString())
            return;

        var position = Util.GetVectorFromJson(obj);
        var player = PlayerSpawner.Instance.GetPlayer(obj.data["id"].ToString());
        player.transform.position = position;
    }

    private void OnRequestPosition()
    {
        NetworkMgr.Instance.Emit(Keys.UpdatePosition, Util.VectorToJson(transform.position));
    }
}
