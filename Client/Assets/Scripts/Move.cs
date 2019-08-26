using System.Collections;
using System.Collections.Generic;
using SocketIO;
using UnityEngine;

public class Move : ViewBase {
    protected override void AddEventListener()
    {
        NetworkMgr.Instance.AddListener(Keys.Move,OnMove);
    }

    protected override void RemoveEventListener()
    {
        NetworkMgr.Instance.RemoveListener(Keys.Move,OnMove);
    }

	private void OnMove(SocketIOEvent obj)
    {
        var position = Util.GetVectorFromJson(obj);
        Debug.Log("OnMove  "+position);
        var player = PlayerSpawner.Instance.GetPlayer(obj.data["id"].ToString());
        if(player == null)
        {
            Debug.LogError("player id:"+obj.data["id"].ToString()+" is null ");
            return;
        }
        var navPos = player.GetComponent<Navigator>();
        navPos.NavigateTo(position);
    }
}
