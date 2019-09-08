using System.Collections;
using System.Collections.Generic;
using SocketIO;
using UnityEngine;

public class Move : ViewBase
{
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
        string id = obj.data["id"].ToString();
        if (id != GetComponent<NetworkEntity>().id)
            return;

        var position = Util.GetVectorFromJson(obj);
        Debug.Log("OnMove  " + position);
        var _navigator = transform.GetComponent<Navigator>();
        _navigator.NavigateTo(position);
    }
}
