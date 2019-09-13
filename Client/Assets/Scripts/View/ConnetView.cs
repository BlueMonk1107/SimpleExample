using System;
using System.Collections;
using System.Collections.Generic;
using SocketIO;
using UnityEngine;

public class ConnetView : MonoBehaviour {

	// Use this for initialization
	void Start () {
		NetworkMgr.Instance.AddListener(Keys.Connection, Connet);
		NetworkMgr.Instance.AddListener(Keys.Disconnect, Disconnect);
		NetworkMgr.Instance.AddListener(Keys.OtherDisconnect, OtherDisconnect);
	}

	private void OnDestroy()
	{
		NetworkMgr.Instance.RemoveListener(Keys.Connection, Connet);
		NetworkMgr.Instance.RemoveListener(Keys.Disconnect, Disconnect);
		NetworkMgr.Instance.AddListener(Keys.OtherDisconnect, OtherDisconnect);
	}

	private void Connet(SocketIOEvent data)
	{
		Debug.Log("Connect Server Success");
	}
	
	private void Disconnect(SocketIOEvent data)
	{
		Debug.Log("Disconnect Server");
		PlayerSpawner.Instance.RemovePlayer(PlayerData.ID);
	}

	private void OtherDisconnect(SocketIOEvent data)
	{
		string id = Util.GetId(data);
		PlayerSpawner.Instance.RemovePlayer(id);
	}
}
