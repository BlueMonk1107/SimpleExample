using System;
using System.Collections;
using System.Collections.Generic;
using SocketIO;
using UnityEngine;

public class NetworkMgr : MonoBehaviour {
	public static NetworkMgr Instance { get; private set;}
	private Dictionary<string,NetworkEvent> _eventsDic;
	private SocketIOComponent _socket;

	private void Awake() 
	{
		_eventsDic = new Dictionary<string, NetworkEvent>();
		InitInstance();
		_socket = gameObject.AddComponent<SocketIOComponent>();
		AddEventListener();

	}

	private void Destroy()
	{
		RemoveEventListener();
	}

	private void AddEventListener()
	{
		AddListener(Keys.Connection,Connect);
		AddListener(Keys.Disconnect,OnDisconnected);
	}

	private void RemoveEventListener()
	{
		RemoveListener(Keys.Connection,Connect);
		RemoveListener(Keys.Disconnect,OnDisconnected);
	}

	private void Connect(SocketIOEvent data)
	{
		Debug.Log("Connect success");
	}

    private void OnDisconnected(SocketIOEvent obj)
    {
		Debug.Log("Disconnected");
        var disconnectedId = obj.data["id"].ToString();
        PlayerSpawner.Instance.RemovePlayer(disconnectedId);
    }

	private void InitInstance()
	{
		if(Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private NetworkEvent GetNetworkEvent(string id,SocketIOComponent socket)
	{
		return new NetworkEvent(id,socket);
	}
	
	public void AddListener(string id,Action<SocketIOEvent> callBack)
	{
		if(!_eventsDic.ContainsKey(id))
		{
			_eventsDic.Add(id,GetNetworkEvent(id,_socket));
		}

		_eventsDic[id].AddListener(callBack);
	}

	public void Emit(string id,JSONObject json = null)
	{
		_socket.Emit(id,json);
	}

	public void RemoveListener(string id,Action<SocketIOEvent> callBack)
	{
		if(_eventsDic.ContainsKey(id))
		{
			_eventsDic[id].RemoveListener(callBack);
		}
	}
}
