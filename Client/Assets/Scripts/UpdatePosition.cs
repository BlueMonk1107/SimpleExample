using System.Collections;
using System.Collections.Generic;
using SocketIO;
using UnityEngine;

public class UpdatePosition : ViewBase {
	private NetworkEntity _entity;
 	// Use this for initialization
    void Start () {
		 _entity = GetComponent<NetworkEntity>();
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
        var position = Util.GetVectorFromJson(obj);
        var player = PlayerSpawner.Instance.GetPlayer(obj.data["id"].ToString());
        player.transform.position = position;
    }

    private void OnRequestPosition(SocketIOEvent obj)
    {
		GameObject player = PlayerSpawner.Instance.GetPlayer(_entity.id);
        NetworkMgr.Instance.Emit("updatePosition", Util.VectorToJson(player.transform.position));
    }
}
