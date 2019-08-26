using System;
using SocketIO;

public class NetworkEvent
{
    public string ID { get; private set; }
    private Action<SocketIOEvent> _action;

    public NetworkEvent(string id,SocketIOComponent socket)
    {
        ID = id;
        socket.On(id, Excute);
    }

    public void AddListener(Action<SocketIOEvent> action)
    {
        _action += action;
    }

    public void RemoveListener(Action<SocketIOEvent> action)
    {
        _action -= action;
    }

    private void Excute(SocketIOEvent data)
    {
        if(_action != null)
        {
            _action(data);
        }
    }

    public void Clear()
    {
        _action = null;
    }

}