using System;
using System.Collections;
using System.Collections.Generic;
using SocketIO;
using UnityEngine;

public class NetworkMgr : MonoBehaviour
{
	public static NetworkMgr Instance { get; private set; }
	private SocketIOComponent _socket;
	private Dictionary<string, NetworkEvent> _eventsDic;
	
	// Use this for initialization
	void Awake ()
	{
		InitInstance();
		_eventsDic = new Dictionary<string, NetworkEvent>();
		_socket = gameObject.AddComponent<SocketIOComponent>();
	}

	private void InitInstance()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
		
	}

	public void AddListener(string id,Action<SocketIOEvent> callBack)
	{
		if (!_eventsDic.ContainsKey(id))
		{
			_eventsDic.Add(id,new NetworkEvent(id,_socket));
		}
		_eventsDic[id].AddListener(callBack);
	}

	public void RemoveListener(string id,Action<SocketIOEvent> callBack)
	{
		if (_eventsDic.ContainsKey(id))
		{
			_eventsDic[id].RemoveListener(callBack);
		}
	}

	public void Emit(string id,JSONObject json = null)
	{
		_socket.Emit(id,json);
	}
}
