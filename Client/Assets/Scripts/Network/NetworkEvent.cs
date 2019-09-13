using System;
using System.Collections;
using System.Collections.Generic;
using SocketIO;
using UnityEngine;

public class NetworkEvent {

    public string ID { get; private set; }
    private Action<SocketIOEvent> _action;

    public NetworkEvent(string id,SocketIOComponent socket)
    {
        ID = id;
        socket.On(id,Excute);
    }

    public void AddListener(Action<SocketIOEvent> callBack)
    {
        _action += callBack;
    }

    public void RemoveListener(Action<SocketIOEvent> callBack)
    {
        _action -= callBack;
    }

    private void Excute(SocketIOEvent data)
    {
        if (_action != null)
        {
            _action(data);
        }
    }

    public void Clear()
    {
        _action = null;
    }
}
